[MainClass]
class Game
{
  [StateField]
  double sunX, ballX, ballY, ballSpdX, ballSpdY;
  [StateField]
  int[,] blocks;

  int ballSize = 8;
  double g = 0.004;
  int blockSize = 24;
  int blockCount = 40;

  [InitMethod]
  void Init() {
    ballX = 50.0;
    ballY = -50.0;

    blocks = new int[blockCount, blockCount];
    for (int i = 0; i < 12; ++i) {
      blocks[i, 18] = 1;
    }
    for (int i = 18; i < 21; ++i) {
      blocks[i, 18] = 1;
    }
    blocks[11, 18] = 2;
    for (int i = 18; i < 21; ++i) {
      blocks[i, 13] = 1;
    }
    for (int i = 18; i < 21; ++i) {
      blocks[i, 10] = 1;
    }
    for (int i = 13; i < 18; ++i) {
      blocks[18, i] = 1;
    }
  }

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    double spdX = 0.3;
    if (input['A']) ballSpdX = -spdX;
    else if (input['D']) ballSpdX = spdX;
    else ballSpdX = 0.0;

    double dY = (ballSpdY + g * dt) * dt / 2.0;
    double dX = ballSpdX * dt;
    double d = Math.Sqrt(dX * dX + dY * dY);

    if (d > 5) {
      Tick(dt / 2.0, input);
      Tick(dt / 2.0, input);
      return;
    }

    ballSpdY += g * dt;
    ballY += ballSpdY * dt / 2.0;
    ballX += ballSpdX * dt;

    int si = Bound((int)ballX / blockSize - 1, 0, blockCount - 1);
    int sj = Bound((int)ballY / blockSize - 1, 0, blockCount - 1);
    int ei = Bound((int)(ballX + ballSize) / blockSize + 1, 0, blockCount - 1);
    int ej = Bound((int)(ballY + ballSize) / blockSize + 1, 0, blockCount - 1);

    for (int i = si; i < ei; ++i) {
      for (int j = sj; j < ej; ++j) {
        if (blocks[i, j] == 0) continue;
        var b = new Rect(i * blockSize, j * blockSize, blockSize, blockSize);
        int c = Collision(b);

        if (c == 1) {
          if (ballSpdX > 0.0) ballSpdX = 0.0;
          ballX = b.X - ballSize;
        }
        if (c == 2) {
          if (ballSpdX < 0.0) ballSpdX = 0.0;
          ballX = b.X + b.Width + ballSize;
        }
        if (c == 3 && (j - 1 < 0 || blocks[i, j-1] == 0)) {
          ballY = b.Y - ballSize;
          ballSpdY = 0;
          if (input['W']) ballSpdY = -1.2;
          if (blocks[i,j] == 2) ballSpdY = -1.2;
        }
        if (c == 4 && (j + 1 > blockCount - 1 || blocks[i, j+1] == 0)) {
          ballY = b.Y + b.Height + 1 + ballSize;
          if (ballSpdY < 0.0) ballSpdY = 0;
        }
      }
    }

    sunX += 0.02 * dt;
    if (sunX > 500.0) sunX = -100;

    if (ballY > 800.0) {
      ballX = 50.0;
      ballY = -50.0;
      ballSpdY = 0.0;
    }
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.Rect(Color.FromArgb(70, 0, 255, 255), 0, 0, 500, 500);
    DrawSun(dc);
    DrawBall(dc);
    DrawBlocks(dc);
  }

  [DrawTrackMethod]
  void DrawBall(DrawingContext dc) {
    dc.Ellipse(Colors.Black, ballX, ballY, ballSize, ballSize);
  }

  void DrawSun(DrawingContext dc) {
    dc.Ellipse(Colors.Yellow, sunX, 50, 40, 40);
  }

  void DrawBlocks(DrawingContext dc) {
    for (int i = 0; i < blockCount; ++i) {
      for (int j = 0; j < blockCount; ++j) {
        if (blocks[i, j] == 1)
          dc.Rect(Colors.Green, i * blockSize, j * blockSize, blockSize, blockSize);
        if (blocks[i, j] == 2)
          dc.Rect(Colors.Blue, i * blockSize, j * blockSize, blockSize, blockSize);
      }
    }
  }

  int Collision(Rect b) {
    var playerRect = new Rect((int)ballX - ballSize, (int)ballY - ballSize, ballSize*2, ballSize*2);

    var leftRect = new Rect(b.X - 1, b.Y, 1, b.Height);
    var rightRect = new Rect(b.X + b.Width - 1, b.Y, 1, b.Height);
    var topRect = new Rect(b.X, b.Y - 1, b.Width, 1);
    var bottomRect = new Rect(b.X, b.Y + b.Height - 1, b.Width, 1);

    bool left = playerRect.IntersectsWith(leftRect);
    bool right = playerRect.IntersectsWith(rightRect);
    bool top = playerRect.IntersectsWith(topRect);
    bool bottom = playerRect.IntersectsWith(bottomRect);

    if (left && !right && !top && !bottom) return 1;
    if (!left && right && !top && !bottom) return 2;
    if (!left && !right && top && !bottom) return 3;
    if (!left && !right && !top && bottom) return 4;

    if (left && top) {
      var r1 = Rect.Intersect(playerRect, leftRect);
      var r2 = Rect.Intersect(playerRect, topRect);
      return r1.Height > r2.Width ? 1 : 3;
    }
    if (left && bottom) {
      var r1 = Rect.Intersect(playerRect, leftRect);
      var r2 = Rect.Intersect(playerRect, bottomRect);
      return r1.Height > r2.Width ? 1 : 4;
    }
    if (right && top) {
      var r1 = Rect.Intersect(playerRect, rightRect);
      var r2 = Rect.Intersect(playerRect, topRect);
      return r1.Height > r2.Width ? 2 : 3;
    }
    if (right && bottom) {
      var r1 = Rect.Intersect(playerRect, rightRect);
      var r2 = Rect.Intersect(playerRect, bottomRect);
      return r1.Height > r2.Width ? 2 : 4;
    }
    return 0;
  }

  int Bound(int a, int min, int max) {
    if (a < min) return min;
    if (a > max) return max;
    return a;
  }

}