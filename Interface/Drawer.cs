using static Triangle.Window;

namespace Triangle.Interface
{
    public class Drawer
    {
        private readonly Context c;
        private float scale;
        private PointF[] points;
        private PointF p1, p2, p3;

        public Drawer(Context context)
        {
            this.c = context;
        }

        public void DrawTriangle(Graphics graphics)
        {
            var triangle = c.Triangle;
            if (triangle == null)
            {
                return;
            }

            graphics.Clear(Color.White);
            DrawGrid(graphics);
            CalculateTrianglePoints(triangle);
            DrawTriangleShape(graphics);
            DrawSideSigns(graphics);
        }

        private void CalculateTrianglePoints(TriangleObject.Triangle triangle)
        {
            float a = triangle.SideA, b = triangle.SideB, c = triangle.SideC;
            float angleAB = triangle.AngleAB;

            scale = CalculateMarginScale();

            p1 = new PointF(0, 0); 
            p2 = new PointF(a * scale, 0);

            float angle = (float)Math.Acos(
                (Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2)) / (2 * a * b)
            );

            p3 = new PointF(
                b * scale * (float)Math.Cos(angle),
                b * scale * (float)Math.Sin(angle)
            );

            CenterTriangleOnPanel();
        }

        private void CenterTriangleOnPanel()
        {
            float panelCenterX = c.UI.TrianglePanel.Width / 2;
            float panelCenterY = c.UI.TrianglePanel.Height / 2;

            float triangleCenterX = (p1.X + p2.X + p3.X) / 3;
            float triangleCenterY = (p1.Y + p2.Y + p3.Y) / 3;

            float offsetX = panelCenterX - triangleCenterX;
            float offsetY = panelCenterY - triangleCenterY;

            p1 = new PointF(p1.X + offsetX, p1.Y + offsetY);
            p2 = new PointF(p2.X + offsetX, p2.Y + offsetY);
            p3 = new PointF(p3.X + offsetX, p3.Y + offsetY);
        }

        private float CalculateMarginScale()
        {
            float margin = 0.2f * Math.Min(c.UI.TrianglePanel.Width, c.UI.TrianglePanel.Height);
            float maxWidth = c.UI.TrianglePanel.Width - 2 * margin;
            float maxHeight = c.UI.TrianglePanel.Height - 2 * margin;

            float maxSide = Math.Max(c.Triangle.SideA, Math.Max(c.Triangle.SideB, c.Triangle.SideC));
            return Math.Min(maxWidth / maxSide, maxHeight / maxSide);
        }

        private void DrawTriangleShape(Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, 2);

            graphics.DrawLine(pen, p1, p2);
            graphics.DrawLine(pen, p2, p3);
            graphics.DrawLine(pen, p3, p1);
        }

        private void DrawSideSigns(Graphics graphics)
        {
            Font font = new Font("Arial", 12, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);

            graphics.DrawString("A", font, brush, (p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            graphics.DrawString("B", font, brush, (p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2);
            graphics.DrawString("C", font, brush, (p3.X + p1.X) / 2, (p3.Y + p1.Y) / 2);
        }

        private void DrawGrid(Graphics graphics)
        {
            int cellSize = 20;
            Pen gridPen = new Pen(Color.Gray, 1);

            for (int x = 0; x < c.UI.TrianglePanel.ClientSize.Width; x += cellSize)
            {
                graphics.DrawLine(gridPen, x, 0, x, c.UI.TrianglePanel.ClientSize.Height);
            }

            for (int y = 0; y < c.UI.TrianglePanel.ClientSize.Height; y += cellSize)
            {
                graphics.DrawLine(gridPen, 0, y, c.UI.TrianglePanel.ClientSize.Width, y);
            }
        }
    }
}
