[MainClass]
class Game
{
  [StateField]
  double angle1;

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    angle1 += 0.03 * dt;
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.Rect(Color.FromArgb(255, 0, 0, 30), 0, 0, 500, 500);
    // sun
    dc.Ellipse(Colors.Yellow, 250, 250, 30, 30);
    
    Planet(dc);
    
    Stars(dc);
  }

  [DrawTrackMethod]
  void Planet(DrawingContext dc) {
      dc.Ellipse(Colors.Red,
          250 + 100*Math.Cos(angle1*0.05),
          250 + 100*Math.Sin(angle1*0.05),
          10, 10);
  }
  
  void Stars(DrawingContext dc) {
    var r = new Random(99);
    for (int i = 0; i < 50; ++i) {
      dc.Ellipse(Colors.White, r.Next(0, 501), r.Next(0, 501), 1, 1);
    }
  }
}