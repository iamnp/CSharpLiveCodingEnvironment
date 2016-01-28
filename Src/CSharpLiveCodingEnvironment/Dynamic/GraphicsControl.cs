using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    internal class GraphicsControl : Control
    {
        public Action<DrawingContext> DrawingFunc;
        public bool IsRendering { get; private set; }

        protected override void OnRender(DrawingContext dc)
        {
            IsRendering = true;
            DrawingFunc?.Invoke(dc);
            IsRendering = false;
        }
    }
}