// Основной класс, помечен атрибутом MainClass.
[MainClass]
class Game
{
  // Поля любых типов.
  int a;
  int y = 200;

  // Поля любых типов, помеченные атрибутом StateField,
  // однозначно определяют состояние игрового мира
  // и сохраняют свои значения при редактировании кода.
  [StateField]
  double x;

  // Метод инициализации, помечен атрибутом InitMethod,
  // вызывается средой один раз перед первым игровым тиком.
  [InitMethod]
  void Init() {
    a = 1;
    x = 1;
  }

  // Метод обработки игрового тика, помечен атрибутом TickMethod,
  // вызывается средой для обработки каждого кадра.
  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    if (++a > 500) a = 0;
    if (++x > 500) x = 0;
  }

  // Метод отрисовки кадра, помечен атрибутом DrawMethod,
  // вызывается средой для отрисовки каждого кадра.
  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.DrawRectangle(Brushes.White, null, new Rect(0, 0, 500, 500));
    DrawBall(dc);
  }

  // Метод отрисовки ""следов"" от объектов, помечен атрибутом DrawTrackMethod,
  // вызывается средой для рендеринга состояний игрового мира во время паузы.
  [DrawTrackMethod]
  void DrawBall(DrawingContext dc)
  {
    dc.DrawEllipse(Brushes.Black, null, new Point(x, y), 10, 10);
    dc.DrawEllipse(Brushes.Black, null, new Point(a, y + 50), 10, 10);
  }

  // Любые методы.
  int DoSomething(int someParam)
  {
      someParam += 1;
      return someParam;
  }
}

// Любые классы.
class SomeClass
{
}