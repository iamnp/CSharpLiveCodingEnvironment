[MainClass]
class Game
{
  [StateField]
  double planet1Angle, planet2Angle;

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    planet1Angle += 0.03 * dt; 
    planet2Angle += 0.05 * dt;
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.Rect(Color.FromArgb(255, 0, 0, 30), 0, 0, 500, 500);
    dc.Ellipse(Colors.Yellow, 250, 250, 30, 30);
    
    DrawPlanet1(dc);
    DrawPlanet2(dc);
    
    var r = new Random(99);
    for (int i = 0; i < 50; ++i) {
      dc.Ellipse(Colors.White, r.Next(0, 501), r.Next(0, 501), 1, 1);
    }
  }

  [DrawTrackMethod]
  void DrawPlanet1(DrawingContext dc) {
      dc.Ellipse(Colors.Red, 250 + 100*Math.Cos(planet1Angle*0.05),
                250 + 96*Math.Sin(planet1Angle*0.05), 10, 10);
  }
  
  [DrawTrackMethod]
  void DrawPlanet2(DrawingContext dc) {
    dc.PushTransform(new RotateTransform(planet2Angle, 250, 250));
    dc.Ellipse(Colors.Blue, 400, 400, 10, 10);
    dc.Pop();
  }
}