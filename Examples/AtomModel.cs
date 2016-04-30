[MainClass]
class Game
{
  [StateField]
  double[] angle;
  
  [InitMethod]
  void Init() {
    angle = new double[5];
    var r = new Random(99);
    for (int i = 0; i < 5; ++i) {
      angle[i] = r.Next(0, 1000) / 10.0;
    }
  }

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    for (int i = 0; i < 5; ++i) {
        angle[i] += 0.03*dt;
    }
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.Rect(Color.FromArgb(0, 0, 0, 0), 0, 0, 500, 500);
    dc.Ellipse(Colors.Red, 250, 250, 20, 20);   

    for (int i = 0; i < 5; ++i) {
       dc.PushTransform(new RotateTransform(36*i, 250, 250));
       dc.Ellipse(Colors.Gray, 3, 250, 250, 40, 150);
       dc.Ellipse(Colors.Black,
          250 + 40*Math.Cos(angle[i]*0.05),
          250 + 150*Math.Sin(angle[i]*0.05),
          8, 8);
       dc.Pop();
    }
    
  }
}