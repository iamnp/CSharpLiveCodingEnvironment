﻿namespace CSharpLiveCodingEnvironment
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
    }
}