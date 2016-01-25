[MainClass]
class Game
{
  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.DrawEllipse(new SolidColorBrush(Color.FromArgb(255, 200, 200, 200)), null, new Point(50, 18), 12, 12);
    dc.DrawEllipse(new SolidColorBrush(Color.FromArgb(255, 200, 200, 200)), new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), 3), new Point(50, 51), 12, 12);
    dc.DrawEllipse(null, new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), 3), new Point(50, 87), 12, 12);
    
    dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 200, 200, 200)), null, new Rect(25, 111, 24, 24));
    dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 200, 200, 200)), new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), 3), new Rect(25, 147, 24, 24));
    dc.DrawRectangle(null, new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), 3), new Rect(25, 184, 24, 24));
    
    dc.DrawRoundedRectangle(new SolidColorBrush(Color.FromArgb(255, 200, 200, 200)), null, new Rect(25, 237, 24, 24), 5, 5);
    dc.DrawRoundedRectangle(new SolidColorBrush(Color.FromArgb(255, 200, 200, 200)), new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), 3), new Rect(25, 270, 24, 24), 5, 5);
    dc.DrawRoundedRectangle(null, new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), 3), new Rect(25, 307, 24, 24), 5, 5);
    
    dc.DrawLine(new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), 3), new Point(50, 340), new Point (70, 360));
  }
}