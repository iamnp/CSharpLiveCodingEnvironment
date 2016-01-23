[MainClass]
class Game
{
  [StateField]
  double marsAngle, anotherPlanetAngle;

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    marsAngle += 0.03 * dt; 
    anotherPlanetAngle += 0.05 * dt;
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 0, 0, 30)), null, new Rect(0, 0, 500, 500));
    dc.DrawEllipse(Brushes.Yellow, null, new Point(250, 250), 30, 30);
    
    d(dc);
    h(dc);
    
    var r = new Random(99);
    for (int i = 0; i < 50; ++i) {
      dc.DrawEllipse(Brushes.White, null, new Point(r.Next(0, 501), r.Next(0, 501)), 1, 1);
    }
  }

  [DrawTrackMethod]
  void d(DrawingContext dc) {
      dc.DrawEllipse(Brushes.Red, null,
      new Point(250 + 100*Math.Cos(marsAngle*0.05),
                250 + 96*Math.Sin(marsAngle*0.05)), 10, 10);
  }
  
  [DrawTrackMethod]
  void h(DrawingContext dc) {
    dc.PushTransform(new RotateTransform(anotherPlanetAngle, 250, 250));
    dc.DrawEllipse(Brushes.Blue, null, new Point(400, 400), 10, 10);
    dc.Pop();
  }
}