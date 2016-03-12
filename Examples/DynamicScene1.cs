[MainClass]
class Game
{
  [StateField]
  double angle;

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    angle += 0.005 * dt;
    if (angle >= 360) angle = 0;
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.Rect(Color.FromArgb(255, 135, 206, 235), 0, 0, 500, 500);

    int sunX = 436, sunY = 43, sunR = 60;
    dc.Ellipse(Colors.Yellow, sunX, sunY, sunR, sunR);
    int innerW = 10, outerW = 80;
    var beam = GetBeam(sunX - innerW/2, sunY, innerW, outerW, 216);
    var beams = 10;
    for (int i = 0; i < beams; ++i) {
        dc.PushTransform(new RotateTransform(angle + i * 360 / beams, sunX, sunY));
        dc.DrawGeometry(new LinearGradientBrush(Color.FromArgb(0, 255, 255, 0), Color.FromArgb(255, 255, 255, 0), 90), null, beam);
        dc.Pop();
    }
  }
  
  Geometry GetBeam(int x, int y, int innerW, int outerW, int h) {
    var g = new PathGeometry();
    var f = new PathFigure { StartPoint = new Point(x, y) };
    f.Segments.Add(new LineSegment(new Point(x - (outerW - innerW)/2, y - h), true));    
    f.Segments.Add(new LineSegment(new Point(x + (outerW - innerW)/2 + innerW, y - h), true));
    f.Segments.Add(new LineSegment(new Point(x + innerW, y), true));
    g.Figures.Add(f);
    return g;
  }
}