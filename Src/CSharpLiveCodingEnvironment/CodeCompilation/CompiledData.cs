using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Media;

namespace CSharpLiveCodingEnvironment.CodeCompilation
{
    internal class CompiledData
    {
        public Action<DrawingContext> DrawDelegate;
        public List<Action<DrawingContext>> DrawTrackDelegates;

        public FieldInfo[] Fields;
        public Action InitDelegate;
        public object Instance;
        public bool NeedToSaveState;
        public Action<double, Dictionary<char, bool>> TickDelegate;

        public void TryInvokeTickDelegate(double dt, Dictionary<char, bool> input)
        {
            if (TickDelegate == null) return;
            try
            {
                TickDelegate(dt, input);
            }
            catch
            {
            }
        }

        public void TryInvokeDrawDelegate(DrawingContext dc)
        {
            if (DrawDelegate == null) return;
            try
            {
                DrawDelegate(dc);
            }
            catch
            {
            }
        }

        public void TryInvokeInitDelegate()
        {
            if (InitDelegate == null) return;
            try
            {
                InitDelegate();
            }
            catch
            {
            }
        }

        public void TryInvokeDrawTrackDelegates(DrawingContext dc)
        {
            for (var i = 0; i < DrawTrackDelegates.Count; ++i)
            {
                try
                {
                    DrawTrackDelegates[i](dc);
                }
                catch
                {
                }
            }
        }

        public void SetGameState(Dictionary<string, object> state)
        {
            for (var index = 0; index < Fields.Length; index++)
            {
                var f = Fields[index];
                if (state.ContainsKey(f.Name))
                {
                    f.SetValue(Instance, state[f.Name]);
                }
            }
        }

        public Dictionary<string, object> DumpGameState()
        {
            var d = new Dictionary<string, object>();
            foreach (var f in Fields)
            {
                foreach (var attr in f.CustomAttributes)
                {
                    if (attr.AttributeType.Name == "StateField")
                    {
                        d[f.Name] = f.GetValue(Instance);
                        break;
                    }
                }
            }
            return d;
        }

        public bool CheckWhileTrue(double dt, Dictionary<char, bool> input)
        {
            var res = true;
            var t = new Thread(() =>
            {
                var drawingVisual = new DrawingVisual();
                using (var dc = drawingVisual.RenderOpen())
                {
                    try
                    {
                        InitDelegate?.Invoke();
                        TickDelegate?.Invoke(dt, input);
                        DrawDelegate?.Invoke(dc);
                    }
                    catch
                    {
                    }
                }
            });
            t.Start();
            if (!t.Join(SettingsForm.Instance.InfiniteLoopsTimeout))
            {
                t.Abort();
                res = false;
            }
            return res;
        }
    }
}