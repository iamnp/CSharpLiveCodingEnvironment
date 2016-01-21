using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    internal class GraphicsControl : Control
    {
        public Action<DrawingContext> DrawingFunc;

        protected override void OnRender(DrawingContext dc)
        {
            DrawingFunc?.Invoke(dc);
        }
    }
}