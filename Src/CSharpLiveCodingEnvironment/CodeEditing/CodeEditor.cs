using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CSharpLiveCodingEnvironment.CodeEditing
{
    internal partial class CodeEditor : UserControl
    {
        private const int DockWidth = 20;
        private const int OffsetFromDock = 1;

        private const int WheelDelta = 120;
        private const int LinesPerScroll = 3;

        private const int ManipulatorHeight = 15;
        private const int ManipulatorWidth = 200;
        private const int MarkerSize = 14;

        private readonly Font _font = new Font("Consolas", 10f, FontStyle.Regular, GraphicsUnit.Point);

        private readonly HighlightingRule[] _highlightingRules =
        {
            new HighlightingRule
            {
                Pattern = new Regex(@"(\/\/.*)", RegexOptions.Compiled),
                Color = Color.Green
            },
            new HighlightingRule
            {
                Pattern = new Regex(@"(""(?:\\""|[^""])*"")", RegexOptions.Compiled),
                Color = Color.Green
            },
            new HighlightingRule
            {
                Pattern =
                    new Regex(
                        @"(?:[^a-zA-Z0-9_]|^)(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|while)(?=(?:[^a-zA-Z0-9_]|$))",
                        RegexOptions.Compiled),
                Color = Color.FromArgb(255, 110, 110, 255)
            },
            new HighlightingRule
            {
                Pattern = new Regex(@"(?:[^a-zA-Z0-9_]|^)(-*[0-9\.]+)(?=(?:[^a-zA-Z0-9_]|$))", RegexOptions.Compiled),
                Color = Color.FromArgb(255, 0, 0, 255)
            }
        };

        private readonly List<LineMarker> _markers = new List<LineMarker>();

        private readonly Selection _selection = new Selection();

        private readonly ToolTip _toolTip = new ToolTip();
        private bool _caretVisible;
        private float _charWidth = -1;
        private int _decimalPlacesAfterPoint;
        private int _initialMouseXPos;
        private double _initialValue;
        private Keys _lastModifiers;
        private double _lastNum = double.NaN;

        private string[] _lines;

        private bool _manipulating;
        private int _manipulatingSelectionStart;
        private int _manipulatingSelectionStop;
        private int _manipulatorOriginX;
        private int _manipulatorOriginY;
        private int _manipulatorValue;
        private bool _needToSetLayoutSize;
        private bool _selecting;
        private int _showingToolTipLine = -1;
        private string _text;

        public CodeEditor()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            SetNewText("");
            VerticalScroll.SmallChange = _font.Height;
        }

        public string SelectedText
        {
            get
            {
                var sel = _selection.Sorted();
                var a = CharPosToIndex(sel.Start);
                var b = CharPosToIndex(sel.End);
                return _text.Substring(a, b - a);
            }
            set
            {
                ReplaceSelectionWith(value);
                SmoothRefresh();
            }
        }

        public override string Text
        {
            get { return _text; }
            set { SetNewText(value); }
        }

        public void ReplaceSelectedTextWith(string s)
        {
            var sel = _selection.Sorted();
            SelectedText = s;
            _selection.Start = sel.Start;
            _selection.End = IndexToCharPos(CharPosToIndex(_selection.Start) + s.Length);
        }

        public new event EventHandler TextChanged;

        protected override void OnTextChanged(EventArgs args)
        {
            TextChanged?.Invoke(this, args);
        }

        private void SetNewText(string text)
        {
            _text = text;
            _lines = _text.Split('\n');
            _needToSetLayoutSize = true;

            if (_selection.Start.Line >= _lines.Length)
                _selection.Start.Line = _lines.Length - 1;
            if (_selection.Start.Column >= _lines[_selection.Start.Line].Length)
                _selection.Start.Column = _lines[_selection.Start.Line].Length;

            if (_selection.End.Line >= _lines.Length)
                _selection.End.Line = _lines.Length - 1;
            if (_selection.End.Column >= _lines[_selection.Start.Line].Length)
                _selection.End.Column = _lines[_selection.Start.Line].Length;
            PosCaret();
            ScrollToCaret();
            OnTextChanged(EventArgs.Empty);
            Invalidate();
        }

        private void SetLayoutSize()
        {
            var max = 0;
            for (var i = 0; i < _lines.Length; ++i)
            {
                if (_lines[i].Length > max) max = _lines[i].Length;
            }
            var h = _lines.Length*_font.Height;
            var add = Width - ClientRectangle.Width;
            AutoScrollMinSize = new Size((int) (max*_charWidth + add + DockWidth + OffsetFromDock), h);
            ScrollToCaret();
        }

        protected override bool ProcessMnemonic(char c)
        {
            if (!Focused) return false;
            if ((_lastModifiers & Keys.Control) == Keys.Control) return false;
            if (c == '\b')
            {
                BackspacePressed();
            }
            else if (c == '\n' || c == '\r')
            {
                EnterPressed();
            }
            else if (c == '\t')
            {
                ReplaceSelectionWith("    ");
            }
            else
            {
                ReplaceSelectionWith(c.ToString());
            }
            PosCaret();
            return true;
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            if (m.Msg == WinApi.WM_CHAR)
                ProcessMnemonic(Convert.ToChar(m.WParam.ToInt32()));

            return base.ProcessKeyMessage(ref m);
        }

        protected override bool IsInputKey(Keys key)
        {
            if ((key & Keys.Alt) != Keys.Alt && ((key & Keys.KeyCode) == Keys.Right))
                return true;
            if ((key & Keys.Alt) != Keys.Alt && ((key & Keys.KeyCode) == Keys.Left))
                return true;
            if ((key & Keys.Alt) != Keys.Alt && ((key & Keys.KeyCode) == Keys.Down))
                return true;
            if ((key & Keys.Alt) != Keys.Alt && ((key & Keys.KeyCode) == Keys.Up))
                return true;
            if ((key & Keys.Alt) != Keys.Alt && ((key & Keys.KeyCode) == Keys.End))
                return true;
            if ((key & Keys.Alt) != Keys.Alt && ((key & Keys.KeyCode) == Keys.Home))
                return true;
            if ((key & Keys.KeyCode) == Keys.Tab)
                return true;
            if ((key & Keys.KeyCode) == Keys.Delete)
                return true;

            return base.IsInputKey(key);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_selecting)
            {
                _selecting = false;
                SetCharPosToNearestToPoint(_selection.End, e.X, e.Y, AutoScrollPosition.X, AutoScrollPosition.Y);
                SmoothRefresh();
            }
            if (_manipulating)
            {
                _manipulatorValue = 0;
                _manipulating = false;
                SmoothRefresh();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Cursor = e.X < DockWidth || _manipulating ? Cursors.Arrow : Cursors.IBeam;
            CheckToShowToolTip(e.X, e.Y, AutoScrollPosition.Y);
            if (_manipulating)
            {
                ManipulateSelectedNumber(e.X);
                SmoothRefresh();
            }
            if (_selecting)
            {
                SetCharPosToNearestToPoint(_selection.End, e.X, e.Y, AutoScrollPosition.X, AutoScrollPosition.Y);
                SmoothRefresh();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.X < DockWidth) return;
            SetCharPosToNearestToPoint(_selection.End, e.X, e.Y, AutoScrollPosition.X, AutoScrollPosition.Y);

            if ((_lastModifiers & Keys.Control) == Keys.Control)
            {
                _manipulating = TrySelectNumberOnSelectionEnd(e.X, AutoScrollPosition.Y);
                _selecting = !_manipulating;
            }
            else
            {
                _selecting = true;
                if ((_lastModifiers & Keys.Shift) != Keys.Shift)
                {
                    _selection.Start = _selection.End.Copy();
                }
            }
            SmoothRefresh();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
                _lastModifiers &= ~Keys.Shift;
            if (e.KeyCode == Keys.Alt)
                _lastModifiers &= ~Keys.Alt;
            if (e.KeyCode == Keys.ControlKey)
                _lastModifiers &= ~Keys.Control;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!Focused) return;
            _lastModifiers = e.Modifiers;
            if (e.KeyData == Keys.Delete)
            {
                DeletePressed();
                PosCaret();
            }
            if (e.KeyCode == Keys.Right)
            {
                MoveSelectionToRight((e.Modifiers & Keys.Shift) == Keys.Shift);
            }
            if (e.KeyCode == Keys.Left)
            {
                MoveSelectionToLeft((e.Modifiers & Keys.Shift) == Keys.Shift);
            }
            if (e.KeyCode == Keys.Up)
            {
                if ((e.Modifiers & Keys.Control) == Keys.Control)
                {
                    ScrollUp(1);
                }
                else
                {
                    MoveSelectionUp((e.Modifiers & Keys.Shift) == Keys.Shift);
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if ((e.Modifiers & Keys.Control) == Keys.Control)
                {
                    ScrollDown(1);
                }
                else
                {
                    MoveSelectionDown((e.Modifiers & Keys.Shift) == Keys.Shift);
                }
            }
            if (e.KeyCode == Keys.A && (e.Modifiers & Keys.Control) == Keys.Control)
            {
                _selection.Start = new Position {Line = 0, Column = 0};
                _selection.End = new Position {Line = _lines.Length - 1, Column = _lines[_lines.Length - 1].Length};
                SmoothRefresh();
            }

            if (e.KeyCode == Keys.C && (e.Modifiers & Keys.Control) == Keys.Control)
            {
                CopyToClipboard();
            }

            if (e.KeyCode == Keys.X && (e.Modifiers & Keys.Control) == Keys.Control)
            {
                CutToClipboard();
            }

            if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control)
            {
                PasteFromClipboard();
            }

            if (e.KeyCode == Keys.Home)
            {
                MoveCharPosToLineStart(_selection.End);
                if ((e.Modifiers & Keys.Shift) != Keys.Shift)
                {
                    _selection.Start = _selection.End.Copy();
                }
                SmoothRefresh();
            }

            if (e.KeyCode == Keys.End)
            {
                MoveCharPosToLineEnd(_selection.End);
                if ((e.Modifiers & Keys.Shift) != Keys.Shift)
                {
                    _selection.Start = _selection.End.Copy();
                }
                SmoothRefresh();
            }
        }

        private void SmoothRefresh()
        {
            Invalidate();
            ScrollToCaret();
            PosCaret();
        }

        protected override void OnGotFocus(EventArgs eventArgs)
        {
            WinApi.CreateCaret(Handle, 0, 1, _font.Height);
            PosCaret();
            WinApi.ShowCaret(Handle);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs eventArgs)
        {
            WinApi.DestroyCaret();
            _selecting = false;
            _manipulating = false;
            _manipulatorValue = 0;
            _lastModifiers = Keys.None;
            _toolTip.RemoveAll();
            _showingToolTipLine = -1;
            Invalidate();
        }

        #region Manipulating routine

        private static bool IsNumberSymbol(char c)
        {
            return char.IsDigit(c) || c == '-' || c == '.';
        }

        private void ManipulateSelectedNumber(int x)
        {
            var delta = (x - _initialMouseXPos)/Math.Pow(10, _decimalPlacesAfterPoint);
            _manipulatorValue = x - _initialMouseXPos;
            var num = _initialValue + delta;
            var numStr = string.Format($"{{0:F{_decimalPlacesAfterPoint}}}", num).Replace(",", ".");
            if (Math.Abs(num - _lastNum) > double.Epsilon)
            {
                ReplaceSelectionWith(numStr);
            }
            _lastNum = num;

            _manipulatingSelectionStop = _manipulatingSelectionStart + numStr.Length - 1;

            _selection.Start = IndexToCharPos(_manipulatingSelectionStart);
            _selection.End = IndexToCharPos(_manipulatingSelectionStop + 1);
        }

        private bool TrySelectNumberOnSelectionEnd(int x, int shiftY)
        {
            _initialMouseXPos = x;
            var cursorPos = CharPosToIndex(_selection.End);

            if (!IsNumberSymbol(_text[cursorPos]))
            {
                if (cursorPos > 0 && IsNumberSymbol(_text[cursorPos - 1]))
                    cursorPos -= 1;
                else return false;
            }

            _manipulatingSelectionStart = cursorPos;
            while (_manipulatingSelectionStart > 0 && IsNumberSymbol(_text[_manipulatingSelectionStart - 1]))
                _manipulatingSelectionStart -= 1;

            _manipulatingSelectionStop = cursorPos;
            while (_manipulatingSelectionStop < _text.Length - 1 &&
                   IsNumberSymbol(_text[_manipulatingSelectionStop + 1]))
                _manipulatingSelectionStop += 1;

            if (_manipulatingSelectionStop - _manipulatingSelectionStart < 0) return false;

            var number = _text.Substring(_manipulatingSelectionStart,
                _manipulatingSelectionStop - _manipulatingSelectionStart + 1);

            var pointPos = number.IndexOf('.');
            _decimalPlacesAfterPoint = pointPos == -1 ? 0 : number.Length - pointPos - 1;

            if (!double.TryParse(number.Replace(".", ","), out _initialValue))
                return false;

            _selection.Start = IndexToCharPos(_manipulatingSelectionStart);
            _selection.End = IndexToCharPos(_manipulatingSelectionStop + 1);

            _manipulatorOriginX = _initialMouseXPos - ManipulatorWidth/2;
            _manipulatorOriginY = shiftY + _selection.End.Line*_font.Height - ManipulatorHeight - 4;
            Cursor = Cursors.Arrow;
            return true;
        }

        #endregion

        #region Markers routine

        public void AddMarker(LineMarker m)
        {
            _markers.Add(m);
            Invalidate();
        }

        public void RemoveAllMarkers()
        {
            _markers.Clear();
            Invalidate();
        }

        private void CheckToShowToolTip(int x, int y, int shiftY)
        {
            var firstLine = -shiftY/_font.Height;
            var lastLine = (-shiftY + ClientRectangle.Height)/_font.Height;

            var shown = false;
            for (var i = 0; i < _markers.Count; ++i)
            {
                if (_markers[i].Line >= firstLine && _markers[i].Line <= lastLine)
                {
                    var markerX = 2 + MarkerSize/2;
                    var markerY = (_markers[i].Line - 1 - firstLine)*_font.Height + MarkerSize/2;
                    var dx = markerX - x;
                    var dy = markerY - y;

                    if (dx*dx + dy*dy <= MarkerSize*MarkerSize/2)
                    {
                        if (_showingToolTipLine != _markers[i].Line)
                        {
                            _toolTip.RemoveAll();
                            _toolTip.Show(_markers[i].Text, this, DockWidth, markerY - MarkerSize/2);
                            _showingToolTipLine = _markers[i].Line;
                        }
                        shown = true;
                        break;
                    }
                }
            }
            if (!shown)
            {
                _toolTip.RemoveAll();
                _showingToolTipLine = -1;
            }
        }

        #endregion

        #region Scrolling routine

        private void ScrollToCaret()
        {
            var firstLine = -AutoScrollPosition.Y/_font.Height;
            var lastLine = (-AutoScrollPosition.Y + ClientRectangle.Height)/_font.Height;
            if (_selection.End.Line < firstLine)
            {
                ScrollUp(firstLine - _selection.End.Line);
            }
            if (_selection.End.Line > lastLine - 1)
            {
                ScrollDown(_selection.End.Line - lastLine + 1);
            }

            var firstColumn = (int) (-AutoScrollPosition.X/_charWidth) + 1;
            var lastColumn = (int) ((-AutoScrollPosition.X + ClientRectangle.Width)/_charWidth) - 1;
            var add = (int) (ClientRectangle.Width/_charWidth/3);
            if (_selection.End.Column < firstColumn)
            {
                ScrollLeft(firstColumn - _selection.End.Column + Math.Min(add, _selection.End.Column));
            }
            if (_selection.End.Column > lastColumn - 4)
            {
                ScrollRight(
                    (int)
                        (_selection.End.Column - lastColumn +
                         Math.Min(add, AutoScrollMinSize.Width/_charWidth - _selection.End.Column)));
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                var newValue = se.NewValue;
                newValue = (int) (Math.Ceiling(1d*newValue/_font.Height)*_font.Height);
                VerticalScroll.Value = Math.Max(VerticalScroll.Minimum, Math.Min(VerticalScroll.Maximum, newValue));
            }

            if (se.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                var newValue = se.NewValue;
                newValue = (int) (Math.Ceiling(1d*newValue/_charWidth)*_charWidth);
                HorizontalScroll.Value = Math.Max(HorizontalScroll.Minimum, Math.Min(HorizontalScroll.Maximum, newValue));
            }

            // aligns scrollbar if it is moved by mouse
            AutoScrollMinSize -= new Size(1, 0);
            AutoScrollMinSize += new Size(1, 0);

            PosCaret();
        }

        private void ScrollUp(int lines)
        {
            if (VerticalScroll.Visible)
            {
                var ea = new ScrollEventArgs(ScrollEventType.SmallDecrement,
                    VerticalScroll.Value,
                    VerticalScroll.Value - _font.Height*lines,
                    ScrollOrientation.VerticalScroll);
                OnScroll(ea);
            }
        }

        protected override void WndProc(ref Message m)
        {
            // prevents flickering while scrolling
            if ((m.Msg == WinApi.WM_HSCROLL || m.Msg == WinApi.WM_VSCROLL) && m.WParam.ToInt32() != WinApi.SB_ENDSCROLL)
            {
                Invalidate();
            }
            base.WndProc(ref m);
        }

        private void ScrollDown(int lines)
        {
            if (VerticalScroll.Visible)
            {
                var ea = new ScrollEventArgs(ScrollEventType.SmallIncrement,
                    VerticalScroll.Value,
                    VerticalScroll.Value + _font.Height*lines,
                    ScrollOrientation.VerticalScroll);
                OnScroll(ea);
            }
        }

        private void ScrollLeft(int chars)
        {
            if (HorizontalScroll.Visible)
            {
                var ea = new ScrollEventArgs(ScrollEventType.SmallDecrement,
                    HorizontalScroll.Value,
                    (int) (HorizontalScroll.Value - _charWidth*chars),
                    ScrollOrientation.HorizontalScroll);
                OnScroll(ea);
            }
        }

        private void ScrollRight(int chars)
        {
            if (HorizontalScroll.Visible)
            {
                var ea = new ScrollEventArgs(ScrollEventType.SmallIncrement,
                    HorizontalScroll.Value,
                    (int) (HorizontalScroll.Value + _charWidth*chars),
                    ScrollOrientation.HorizontalScroll);
                OnScroll(ea);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Invalidate();
            if (e.Delta > 0) ScrollUp(e.Delta/WheelDelta*LinesPerScroll);
            else ScrollDown(-e.Delta/WheelDelta*LinesPerScroll);
        }

        #endregion

        #region Selection managing routine

        private void MoveCharPosToLineStart(Position pos)
        {
            pos.Column = 0;
        }

        private void MoveCharPosToLineEnd(Position pos)
        {
            pos.Column = _lines[pos.Line].Length - 1;
            if (pos.Line == _lines.Length - 1) pos.Column += 1;
        }

        private void SetCharPosToNearestToPoint(Position pos, int x, int y, int shiftX, int shiftY)
        {
            var line = Math.Max((y - shiftY)/_font.Height, 0);
            var column = (int) Math.Max((x - DockWidth - shiftX + 3 - OffsetFromDock)/_charWidth, 0);
            if (line > _lines.Length - 1)
            {
                pos.Line = _lines.Length - 1;
            }
            else
            {
                pos.Line = line;
            }

            if (pos.Line < _lines.Length - 1)
            {
                if (column <= _lines[pos.Line].Length - 1)
                {
                    pos.Column = column;
                }
                else
                {
                    pos.Column = _lines[pos.Line].Length - 1;
                }
            }
            else
            {
                if (column <= _lines[pos.Line].Length - 1)
                {
                    pos.Column = column;
                }
                else
                {
                    pos.Column = _lines[pos.Line].Length;
                }
            }
        }

        private void PosCaret()
        {
            var caretX = (int) (OffsetFromDock + AutoScrollPosition.X + DockWidth + _selection.End.Column*_charWidth);
            if (caretX < DockWidth)
            {
                if (_caretVisible)
                {
                    WinApi.HideCaret(Handle);
                    _caretVisible = false;
                }
            }
            else
            {
                if (!_caretVisible)
                {
                    WinApi.ShowCaret(Handle);
                    _caretVisible = true;
                }
                WinApi.SetCaretPos(caretX, AutoScrollPosition.Y + _selection.End.Line*_font.Height);
            }
        }

        private void MoveCharPosToRight(Position pos)
        {
            if ((pos.Line < _lines.Length - 1 && pos.Column + 1 <= _lines[pos.Line].Length - 1)
                || (pos.Line == _lines.Length - 1 && pos.Column + 1 <= _lines[pos.Line].Length))
            {
                pos.Column += 1;
            }
            else if (pos.Line + 1 <= _lines.Length - 1)
            {
                pos.Column = 0;
                pos.Line += 1;
            }
        }

        private void MoveSelectionToRight(bool shiftPressed)
        {
            if (shiftPressed)
            {
                MoveCharPosToRight(_selection.End);
            }
            else
            {
                if (_selection.IsEmpty)
                {
                    MoveCharPosToRight(_selection.End);
                    _selection.Start = _selection.End.Copy();
                }
                else
                {
                    var sel = _selection.Sorted();
                    _selection.Start = sel.End;
                    _selection.End = _selection.Start.Copy();
                }
            }
            SmoothRefresh();
        }

        private void MoveCharPosToLeft(Position pos)
        {
            if (pos.Column - 1 >= 0)
            {
                pos.Column -= 1;
            }
            else if (pos.Line - 1 >= 0)
            {
                pos.Line -= 1;
                pos.Column = _lines[_selection.End.Line].Length - 1;
            }
        }

        private void MoveSelectionToLeft(bool shiftPressed)
        {
            if (shiftPressed)
            {
                MoveCharPosToLeft(_selection.End);
            }
            else
            {
                if (_selection.IsEmpty)
                {
                    MoveCharPosToLeft(_selection.End);
                    _selection.Start = _selection.End.Copy();
                }
                else
                {
                    var sel = _selection.Sorted();
                    _selection.Start = sel.Start;
                    _selection.End = _selection.Start.Copy();
                }
            }
            SmoothRefresh();
        }

        private void MoveCharPosUp(Position pos)
        {
            if (pos.Line == 0) return;
            pos.Line -= 1;
            if (_lines[pos.Line].Length - 1 < pos.Column)
            {
                pos.Column = _lines[pos.Line].Length - 1;
            }
        }

        private void MoveSelectionUp(bool shiftPressed)
        {
            if (shiftPressed)
            {
                MoveCharPosUp(_selection.End);
            }
            else
            {
                if (_selection.IsEmpty)
                {
                    MoveCharPosUp(_selection.End);
                    _selection.Start = _selection.End.Copy();
                }
                else
                {
                    if (_selection.Start.Line == _selection.End.Line)
                    {
                        MoveCharPosUp(_selection.End);
                    }
                    else
                    {
                        var sel = _selection.Sorted();
                        _selection.End = sel.Start;
                    }
                    _selection.Start = _selection.End.Copy();
                }
            }
            SmoothRefresh();
        }

        private void MoveCharPosDown(Position pos)
        {
            if (pos.Line == _lines.Length - 1) return;
            pos.Line += 1;
            if (_lines[pos.Line].Length - 1 < pos.Column)
            {
                pos.Column = _lines[pos.Line].Length - 1;
                if (pos.Column < 0) pos.Column = 0;
            }
        }

        private void MoveSelectionDown(bool shiftPressed)
        {
            if (shiftPressed)
            {
                MoveCharPosDown(_selection.End);
            }
            else
            {
                if (_selection.IsEmpty)
                {
                    MoveCharPosDown(_selection.End);
                    _selection.Start = _selection.End.Copy();
                }
                else
                {
                    if (_selection.Start.Line == _selection.End.Line)
                    {
                        MoveCharPosDown(_selection.End);
                    }
                    else
                    {
                        var sel = _selection.Sorted();
                        _selection.End = sel.End;
                    }
                    _selection.Start = _selection.End.Copy();
                }
            }
            SmoothRefresh();
        }

        #endregion

        #region Text editing routine

        private void CopyToClipboard()
        {
            if (!_selection.IsEmpty)
            {
                Clipboard.SetText(SelectedText);
            }
        }

        private void PasteFromClipboard()
        {
            ReplaceSelectionWith(Clipboard.GetText());
            SmoothRefresh();
        }

        private void CutToClipboard()
        {
            if (!_selection.IsEmpty)
            {
                Clipboard.SetText(SelectedText);
                ReplaceSelectionWith("");
                SmoothRefresh();
            }
        }

        private void ReplaceSelectionWith(string s)
        {
            var sel = _selection.Sorted();
            var p1 = CharPosToIndex(sel.Start);
            var p2 = CharPosToIndex(sel.End);

            var sb = new StringBuilder(Text);
            sb.Remove(p1, p2 - p1);
            sb.Insert(p1, s);
            Text = sb.ToString();

            _selection.Start = IndexToCharPos(p1 + s.Length);
            _selection.End = _selection.Start.Copy();
        }

        private void BackspacePressed()
        {
            if (_selection.IsEmpty)
            {
                if (_selection.End.Column == 0 && _selection.End.Line == 0) return;
                var pos = CharPosToIndex(_selection.End);
                if (_selection.End.Column == 0)
                {
                    var len = _lines[_selection.End.Line - 1].Length;
                    Text = _text.Remove(pos - 2, 2);
                    if (_selection.End.Line != _lines.Length - 1) _selection.End.Line -= 1;
                    _selection.End.Column = len - 1;
                }
                else
                {
                    Text = _text.Remove(pos - 1, 1);
                    if (_selection.End.Line != _lines.Length - 1) _selection.End.Column -= 1;
                }
                _selection.Start = _selection.End.Copy();
            }
            else
            {
                ReplaceSelectionWith("");
            }
        }

        private void DeletePressed()
        {
            if (_selection.IsEmpty)
            {
                if (_selection.End.Column == _lines[_selection.End.Line].Length &&
                    _selection.End.Line == _lines.Length - 1) return;

                if (_selection.End.Column == _lines[_selection.End.Line].Length - 1 &&
                    _selection.End.Line != _lines.Length - 1)
                {
                    Text = _text.Remove(CharPosToIndex(_selection.End), 2);
                }
                else
                {
                    Text = _text.Remove(CharPosToIndex(_selection.End), 1);
                }
            }
            else
            {
                ReplaceSelectionWith("");
            }
        }

        private string GetLeadingSpaces(string s)
        {
            var i = 0;
            while (i <= s.Length - 1 && s[i] == ' ') i += 1;
            return s.Substring(0, i);
        }

        private void EnterPressed()
        {
            if (_selection.IsEmpty)
            {
                var spaces = GetLeadingSpaces(_lines[_selection.End.Line]);
                var pos = CharPosToIndex(_selection.End);
                Text = _text.Insert(pos, "\r\n" + spaces);
                _selection.End.Line += 1;
                _selection.End.Column = spaces.Length;
                _selection.Start = _selection.End.Copy();
            }
            else
            {
                ReplaceSelectionWith("\r\n");
            }
        }

        private int CharPosToIndex(Position p)
        {
            var cnt = 0;
            for (var i = 0; i < p.Line; ++i)
            {
                cnt += _lines[i].Length + 1;
            }
            cnt += p.Column;
            return cnt;
        }

        private Position IndexToCharPos(int index)
        {
            var cnt = 0;
            for (var i = 0; i < _lines.Length; ++i)
            {
                if (cnt + _lines[i].Length + 1 > index)
                {
                    return new Position {Line = i, Column = index - cnt};
                }
                cnt += _lines[i].Length + 1;
            }
            return new Position {Line = _lines.Length - 1, Column = _lines[_lines.Length - 1].Length};
        }

        #endregion

        #region Drawing routine

        private readonly Brush _backBrush = Brushes.White;
        private readonly Brush _dockBrush = new SolidBrush(Color.FromArgb(255, 230, 231, 232));
        private readonly Pen _dockLinePen = new Pen(Color.FromArgb(255, 192, 192, 192));
        private readonly Pen _lineSelectorPen = new Pen(Color.FromArgb(200, 234, 234, 242), 2);
        private readonly Color _defaultTextColor = Color.Black;
        private readonly Brush _selectionBrush = new SolidBrush(Color.FromArgb(200, 173, 214, 255));
        private readonly Brush _manipulatorBack = new SolidBrush(Color.Silver);
        private readonly Brush _manipulatorFront = new SolidBrush(Color.FromArgb(255, 111, 153, 242));

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(_backBrush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_charWidth < 0)
            {
                _charWidth =
                    e.Graphics.MeasureString("a", _font, new PointF(0, 0), StringFormat.GenericTypographic).Width;
                HorizontalScroll.SmallChange = (int) _charWidth + 1;
            }
            if (_needToSetLayoutSize)
            {
                SetLayoutSize();
                _needToSetLayoutSize = false;
            }
            DrawVisibleSelection(e.Graphics, AutoScrollPosition.X + DockWidth + OffsetFromDock, AutoScrollPosition.Y);
            DrawVisibleText(e.Graphics, AutoScrollPosition.X + DockWidth + OffsetFromDock, AutoScrollPosition.Y);
            e.Graphics.FillRectangle(Brushes.White,
                new Rectangle(0, 0, DockWidth + OffsetFromDock, ClientRectangle.Height));
            e.Graphics.FillRectangle(_dockBrush, new Rectangle(0, 0, DockWidth, ClientRectangle.Height));
            e.Graphics.DrawLine(_dockLinePen, DockWidth - 1, 0, DockWidth - 1, ClientRectangle.Height);
            DrawVisibleMarkers(e.Graphics, AutoScrollPosition.Y);
            DrawLineSelector(e.Graphics, AutoScrollPosition.Y);
            DrawManipulator(e.Graphics);
        }

        private void DrawVisibleText(Graphics g, int shiftX, int shiftY)
        {
            var firstLine = -shiftY/_font.Height;
            var lastLine = (-shiftY + ClientRectangle.Height)/_font.Height;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            for (var i = firstLine; i <= lastLine && i < _lines.Length; ++i)
            {
                DrawLine(g, _lines[i], shiftX, shiftY, _font.Height*i);
            }
        }

        private bool Intersects(List<HighlightingRange> ranges, HighlightingRange r)
        {
            for (var i = 0; i < ranges.Count; ++i)
            {
                if (!(r.Start > ranges[i].Start + ranges[i].Length || r.Start + r.Length < ranges[i].Start))
                    return true;
            }
            return false;
        }

        private List<HighlightingRange> HighlightLine(string line)
        {
            var ranges = new List<HighlightingRange>();

            if (_highlightingRules != null)
            {
                for (var i = 0; i < _highlightingRules.Length; ++i)
                {
                    var matches = _highlightingRules[i].Pattern.Matches(line);
                    foreach (Match m in matches)
                    {
                        var cnd = new HighlightingRange
                        {
                            Color = _highlightingRules[i].Color,
                            Start = m.Groups[1].Index,
                            Length = m.Groups[1].Length
                        };
                        if (!Intersects(ranges, cnd))
                        {
                            ranges.Add(cnd);
                        }
                    }
                }
            }
            ranges.Sort((a, b) => a.Start.CompareTo(b.Start));
            return ranges;
        }

        private void DrawLine(Graphics g, string line, int shiftX, int shiftY, int y)
        {
            var ranges = HighlightLine(line);
            var last = 0;
            for (var i = 0; i < ranges.Count; ++i)
            {
                g.DrawString(line.Substring(last, ranges[i].Start - last), _font, new SolidBrush(_defaultTextColor),
                    shiftX + last*_charWidth, shiftY + y, StringFormat.GenericTypographic);

                g.DrawString(line.Substring(ranges[i].Start, ranges[i].Length), _font, new SolidBrush(ranges[i].Color),
                    shiftX + ranges[i].Start*_charWidth, shiftY + y, StringFormat.GenericTypographic);
                last = ranges[i].Start + ranges[i].Length;
            }
            g.DrawString(line.Substring(last, line.Length - last), _font, new SolidBrush(_defaultTextColor),
                shiftX + last*_charWidth, shiftY + y, StringFormat.GenericTypographic);
        }

        private void DrawVisibleSelection(Graphics g, int shiftX, int shiftY)
        {
            if (_selection.Start.Line == _selection.End.Line)
            {
                var firstLine = -shiftY/_font.Height;
                var lastLine = (-shiftY + ClientRectangle.Height)/_font.Height;
                if (_selection.Start.Column != _selection.End.Column && _selection.Start.Line >= firstLine &&
                    _selection.Start.Line <= lastLine)
                {
                    var sel = _selection.Sorted();
                    DrawSelectionOnLine(_selection.Start.Line, sel.Start.Column, sel.End.Column, g, shiftX, shiftY);
                }
            }
            else
            {
                var sel = _selection.Sorted();
                var firstLine = sel.Start.Line;
                var lastLine = sel.End.Line;
                firstLine = Math.Max(firstLine, -shiftY/_font.Height);
                lastLine = Math.Min(lastLine, (-shiftY + ClientRectangle.Height)/_font.Height);

                for (var i = firstLine; i <= lastLine; ++i)
                {
                    if (i == sel.Start.Line)
                    {
                        DrawSelectionOnLine(i, sel.Start.Column, _lines[i].Length - 1, g, shiftX, shiftY);
                    }
                    else if (i == sel.End.Line)
                    {
                        DrawSelectionOnLine(i, 0, sel.End.Column, g, shiftX, shiftY);
                    }
                    else
                    {
                        DrawSelectionOnLine(i, 0, _lines[i].Length - 1, g, shiftX, shiftY);
                    }
                }
            }
        }

        private void DrawSelectionOnLine(int line, int column1, int column2, Graphics g, int shiftX, int shiftY)
        {
            if (column1 == 0 && column2 == 0 && line != _selection.Start.Line && line != _selection.End.Line)
                column2 = 1;
            g.FillRectangle(_selectionBrush,
                shiftX + column1*_charWidth,
                shiftY + line*_font.Height,
                (column2 - column1)*_charWidth,
                _font.Height);
        }

        private void DrawVisibleMarkers(Graphics g, int shiftY)
        {
            var firstLine = -shiftY/_font.Height;
            var lastLine = (-shiftY + ClientRectangle.Height)/_font.Height;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            for (var i = 0; i < _markers.Count; ++i)
            {
                if (_markers[i].Line >= firstLine && _markers[i].Line <= lastLine)
                {
                    g.FillEllipse(new SolidBrush(_markers[i].Color), 2,
                        shiftY%_font.Height + (_markers[i].Line - 1 - firstLine)*_font.Height, MarkerSize,
                        MarkerSize);
                }
            }
            g.SmoothingMode = SmoothingMode.None;
        }

        private void DrawLineSelector(Graphics g, int shiftY)
        {
            if (!_selection.IsEmpty || !Focused) return;
            var firstLine = -shiftY/_font.Height;
            var lastLine = (-shiftY + ClientRectangle.Height)/_font.Height;
            if (_selection.End.Line >= firstLine && _selection.End.Line <= lastLine)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawRectangle(_lineSelectorPen, DockWidth + OffsetFromDock,
                    shiftY%_font.Height + (_selection.End.Line - firstLine)*_font.Height,
                    ClientRectangle.Width - DockWidth - 1 - OffsetFromDock, _font.Height - 1);
                g.SmoothingMode = SmoothingMode.None;
            }
        }

        private void DrawManipulator(Graphics g)
        {
            if (!_manipulating) return;
            g.FillRectangle(_manipulatorBack, _manipulatorOriginX, _manipulatorOriginY, ManipulatorWidth,
                ManipulatorHeight);
            var a = _manipulatorOriginX + ManipulatorWidth/2;
            var b = a + _manipulatorValue;
            if (a > b)
            {
                var t = a;
                a = b;
                b = t;
            }
            g.FillRectangle(_manipulatorFront, a, _manipulatorOriginY, b - a, ManipulatorHeight);
        }

        #endregion
    }
}