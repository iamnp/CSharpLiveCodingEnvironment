namespace CSharpLiveCodingEnvironment
{
    internal static class CodeSnippets
    {
        private const string DarkColor = @"Color.FromArgb(255, 100, 100, 100)";
        private const string DarkBrush = @"new SolidColorBrush(" + DarkColor + @")";
        private const string DarkPen = @"new Pen(" + DarkBrush + @", 3)";

        private const string LightColor = @"Color.FromArgb(255, 200, 200, 200)";
        private const string LightBrush = @"new SolidColorBrush(" + LightColor + @")";

        public static string HelloWorld = @"[MainClass]
class Game
{
  [StateField]
  double x;

  [InitMethod]
  void Init() {
    x = 0.0;
  }

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    x += 0.05 * dt;
    if (x > 450) x = 0;
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.DrawRectangle(Brushes.White, null, new Rect(0, 0, 500, 500));
    
    DrawCircle(dc);
    
    dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 100, 100, 250)),
      null, new Rect(x - 25, 300, 50, 50));
  }

  void DrawCircle(DrawingContext dc) {
    dc.DrawEllipse(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
      null, new Point(x, 200), 10, 10);
  }
}";

        public static string Ellipse =>
            @"DrawEllipse(" + LightBrush + @", null, new Point(50, 50), 20, 20);";

        public static string EllipseWithStroke =>
            @"DrawEllipse(" + LightBrush + @", " + DarkPen + @", new Point(50, 50), 20, 20);";

        public static string EllipseStroke =>
            @"DrawEllipse(null, " + DarkPen + @", new Point(50, 50), 20, 20);";

        public static string Rectangle =>
            @"DrawRectangle(" + LightBrush + @", null, new Rect(25, 25, 40, 40));";

        public static string RectangleWithStroke =>
            @"DrawRectangle(" + LightBrush + @", " + DarkPen + @", new Rect(25, 25, 40, 40));";

        public static string RectangleStroke =>
            @"DrawRectangle(null, " + DarkPen + @", new Rect(25, 25, 40, 40));";

        public static string Line =>
            @"DrawLine(" + DarkPen + @", new Point(50, 50), new Point (100, 100));";

        public static string RoundedRectangle =>
            @"DrawRoundedRectangle(" + LightBrush + @", null, new Rect(25, 25, 40, 40), 5, 5);";

        public static string RoundedRectangleWithStroke =>
            @"DrawRoundedRectangle(" + LightBrush + @", " + DarkPen + @", new Rect(25, 25, 40, 40), 5, 5);";

        public static string RoundedRectangleStroke =>
            @"DrawRoundedRectangle(null, " + DarkPen + @", new Rect(25, 25, 40, 40), 5, 5);";

        public static string Text =>
            @"DrawText(new FormattedText(""Hello world!"", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(""Arial""), 24, " +
            DarkBrush + @"), new Point(25, 25));";

        public static string GetStandaloneExecutableCode(int desiredDt, bool waitAfterEachTick, int waitAfterEachTickMs)
        {
            return string.Format(@"using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Threading;
using Application = System.Windows.Forms.Application;
using Brushes = System.Windows.Media.Brushes;
using Control = System.Windows.Controls.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace WindowsFormsApplication1
{{
    internal static class Program
    {{
        [STAThread]
        private static void Main()
        {{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }}
    }}

    internal class GraphicsControl : Control
    {{
        public Action<DrawingContext> DrawingFunc;
        public bool IsRendering {{ get; private set; }}

        protected override void OnRender(DrawingContext dc)
        {{
            IsRendering = true;
            if (DrawingFunc != null) DrawingFunc(dc);
            IsRendering = false;
        }}
    }}

    internal class CompiledData
    {{
        public Action<DrawingContext> DrawDelegate;
        public List<Action<DrawingContext>> DrawTrackDelegates;

        public FieldInfo[] Fields;
        public Action InitDelegate;
        public object Instance;
        public bool NeedToSaveState;
        public Action<double, Dictionary<char, bool>> TickDelegate;

        public void TryInvokeTickDelegate(double dt, Dictionary<char, bool> input)
        {{
            if (TickDelegate == null) return;
            try
            {{
                TickDelegate(dt, input);
            }}
            catch
            {{
            }}
        }}

        public void TryInvokeDrawDelegate(DrawingContext dc)
        {{
            if (DrawDelegate == null) return;
            try
            {{
                DrawDelegate(dc);
            }}
            catch
            {{
            }}
        }}

        public void TryInvokeInitDelegate()
        {{
            if (InitDelegate == null) return;
            try
            {{
                InitDelegate();
            }}
            catch
            {{
            }}
        }}

        public void TryInvokeDrawTrackDelegates(DrawingContext dc)
        {{
            for (var i = 0; i < DrawTrackDelegates.Count; ++i)
            {{
                try
                {{
                    DrawTrackDelegates[i](dc);
                }}
                catch
                {{
                }}
            }}
        }}

        public void SetGameState(Dictionary<string, object> state)
        {{
            for (var index = 0; index < Fields.Length; index++)
            {{
                var f = Fields[index];
                if (state.ContainsKey(f.Name))
                {{
                    f.SetValue(Instance, state[f.Name]);
                }}
            }}
        }}

        public Dictionary<string, object> DumpGameState()
        {{
            var d = new Dictionary<string, object>();
            foreach (var f in Fields)
            {{
                foreach (var attr in f.CustomAttributes)
                {{
                    if (attr.AttributeType.Name == ""StateField"")
                    {{
                        d[f.Name] = f.GetValue(Instance);
                        break;
                    }}
                }}
            }}
            return d;
        }}
    }}

    public class Form1 : Form
    {{
        private readonly CompiledData _compiledData;
        private readonly ManualResetEvent _evExit = new ManualResetEvent(false);
        private readonly GraphicsControl _graphics;
        private readonly Dictionary<char, bool> _input = new Dictionary<char, bool>();
        private readonly IContainer components = null;

        private Task _sceneLoopTask;
        private ElementHost elementHost1;

        public Form1()
        {{
            InitializeComponent();
            TimeBeginPeriod(1);
            Load += OnLoad;
            Closing += OnClosing;
            _graphics = new GraphicsControl {{DrawingFunc = DrawScene}};
            elementHost1.Child = _graphics;
            _graphics.KeyDown += GraphicsOnKeyDown;
            _graphics.KeyUp += GraphicsOnKeyUp;
            for (int i = 'A'; i <= 'Z'; i += 1)
            {{
                _input[(char) i] = false;
            }}

            var types = Assembly.GetExecutingAssembly().GetTypes();
            Type type = null;
            for (var i = 0; i < types.Length; ++i)
            {{
                var attrs = types[i].CustomAttributes;
                foreach (var attr in attrs)
                {{
                    if (attr.AttributeType.Name == ""MainClass"")
                    {{
                        type = types[i];
                        break;
                    }}
                }}
            }}

            if (type != null)
            {{
                var inst = Activator.CreateInstance(type);
                var fs = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                _compiledData = new CompiledData
                {{
                    Instance = inst,
                    Fields = fs,
                    NeedToSaveState = false,
                    DrawTrackDelegates = new List<Action<DrawingContext>>()
                }};

                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                for (var i = 0; i < methods.Length; ++i)
                {{
                    foreach (var attr in methods[i].CustomAttributes)
                    {{
                        if (attr.AttributeType.Name == ""InitMethod"" && IsInitMethodSignature(methods[i]))
                            _compiledData.InitDelegate =
                                (Action) Delegate.CreateDelegate(typeof (Action), inst, methods[i]);
                        if (attr.AttributeType.Name == ""DrawMethod"" && IsDrawMethodSignature(methods[i]))
                            _compiledData.DrawDelegate =
                                (Action<DrawingContext>)
                                    Delegate.CreateDelegate(typeof (Action<DrawingContext>), inst, methods[i]);
                        if (attr.AttributeType.Name == ""DrawTrackMethod"" && IsDrawTrackMethodSignature(methods[i]))
                            _compiledData.DrawTrackDelegates.Add((Action<DrawingContext>)
                                Delegate.CreateDelegate(typeof (Action<DrawingContext>), inst, methods[i]));
                        if (attr.AttributeType.Name == ""TickMethod"" && IsTickMethodSignature(methods[i]))
                            _compiledData.TickDelegate =
                                (Action<double, Dictionary<char, bool>>)
                                    Delegate.CreateDelegate(typeof (Action<double, Dictionary<char, bool>>), inst,
                                        methods[i]);
                    }}
                }}
            }}
            _compiledData.TryInvokeInitDelegate();
        }}

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {{
            _evExit.Set();
            _sceneLoopTask.Wait();
            _evExit.Close();
            TimeEndPeriod(1);
        }}

        protected override void Dispose(bool disposing)
        {{
            if (disposing && (components != null))
            {{
                components.Dispose();
            }}
            base.Dispose(disposing);
        }}

        private void InitializeComponent()
        {{
            elementHost1 = new ElementHost();
            SuspendLayout();
            // 
            // elementHost1
            // 
            elementHost1.Dock = DockStyle.Fill;
            elementHost1.Location = new Point(0, 0);
            elementHost1.Name = ""elementHost1"";
            elementHost1.Size = new Size(515, 407);
            elementHost1.TabIndex = 0;
            elementHost1.Text = ""elementHost1"";
            elementHost1.Child = null;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(500, 500);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Controls.Add(elementHost1);
            Name = ""Form1"";
            Text = ""Game"";
            ResumeLayout(false);
        }}

        private bool IsInitMethodSignature(MethodInfo m)
        {{
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 0;
        }}

        private bool IsDrawMethodSignature(MethodInfo m)
        {{
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 1 && p[0].ParameterType == typeof (DrawingContext);
        }}

        private bool IsDrawTrackMethodSignature(MethodInfo m)
        {{
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 1 && p[0].ParameterType == typeof (DrawingContext);
        }}

        private bool IsTickMethodSignature(MethodInfo m)
        {{
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 2 && p[0].ParameterType == typeof (double)
                   && p[1].ParameterType == typeof (Dictionary<char, bool>);
        }}

        private void GraphicsOnKeyUp(object sender, KeyEventArgs e)
        {{
            _input[e.Key.ToString()[0]] = false;
        }}

        private void DrawScene(DrawingContext dc)
        {{
            dc.DrawRectangle(Brushes.White, null,
                new Rect(0, 0, _graphics.ActualWidth, _graphics.ActualHeight));
            if (_compiledData != null)
            {{
                _compiledData.TryInvokeDrawDelegate(dc);
            }}
        }}

        private void GraphicsOnKeyDown(object sender, KeyEventArgs e)
        {{
            _input[e.Key.ToString()[0]] = true;
        }}

        private void OnLoad(object sender, EventArgs eventArgs)
        {{
            _sceneLoopTask = new Task(GameTick, TaskCreationOptions.LongRunning);
            _sceneLoopTask.Start();
        }}

        [DllImport(""winmm.dll"", EntryPoint = ""timeBeginPeriod"", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport(""winmm.dll"", EntryPoint = ""timeEndPeriod"", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);

        private void GameTick()
        {{
            var dtStopwatch = new Stopwatch();

            while (!_evExit.WaitOne({0}))
            {{
                // call move delegate
                dtStopwatch.Stop();
                var dt = 1000.0*dtStopwatch.ElapsedTicks/Stopwatch.Frequency;
                if (_compiledData != null) _compiledData.TryInvokeTickDelegate(dt, _input);
                dtStopwatch.Restart();

                // call draw delegate
                if (!_graphics.IsRendering)
                {{
                    _graphics.Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (MethodInvoker) _graphics.InvalidateVisual);
                }}

                // wait
                if ({1})
                {{
                    dtStopwatch.Stop();
                    Thread.Sleep({2});
                    dtStopwatch.Start();
                }}
            }}
        }}
    }}
}}", desiredDt, waitAfterEachTick ? "true" : "false", waitAfterEachTickMs);
        }
    }
}