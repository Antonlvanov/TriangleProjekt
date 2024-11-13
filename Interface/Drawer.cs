using _triangle = Triangle.TriangleObject.Triangle;

namespace Triangle.Interface
{
    public class Drawer
    {
        private float scale;
        private PointF p1, p2, p3;

        public void DrawTriangle(_triangle triangle, UI ui, Graphics graphics)
        {
            if (triangle == null || ui == null || graphics == null)
            {
                return;
            }

            graphics.Clear(Color.White);
            DrawGrid(graphics, ui);
            CalculateTrianglePoints(triangle, ui);
            DrawTriangleShape(graphics);
            DrawSideSigns(graphics);
        }

        private void CalculateTrianglePoints(_triangle triangle, UI ui)
        {
            float a = triangle.SideA, b = triangle.SideB, c = triangle.SideC;
            float AB_angle = (float)Math.Acos(
                (Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2)) / (2 * a * b)
            );

            scale = CalculateMarginScale(triangle, ui);

            p1 = new PointF(0, 0);
            p2 = new PointF(a * scale, 0);
            p3 = new PointF(
                p1.X + b * scale * (float)Math.Cos(AB_angle),
                p1.Y - b * scale * (float)Math.Sin(AB_angle)
            );

            CenterTriangleOnPanel(ui);
        }

        private void CenterTriangleOnPanel(UI ui)
        {
            float panelCenterX = ui.TrianglePanel.Width / 2;
            float panelCenterY = ui.TrianglePanel.Height / 2;

            float triangleCenterX = (p1.X + p2.X + p3.X) / 3;
            float triangleCenterY = (p1.Y + p2.Y + p3.Y) / 3;

            float offsetX = panelCenterX - triangleCenterX;
            float offsetY = panelCenterY - triangleCenterY;

            p1 = new PointF(p1.X + offsetX, p1.Y + offsetY);
            p2 = new PointF(p2.X + offsetX, p2.Y + offsetY);
            p3 = new PointF(p3.X + offsetX, p3.Y + offsetY);
        }

        private float CalculateMarginScale(_triangle triangle, UI ui)
        {
            float margin = 0.2f * Math.Min(ui.TrianglePanel.Width, ui.TrianglePanel.Height);
            float maxWidth = ui.TrianglePanel.Width - 2 * margin;
            float maxHeight = ui.TrianglePanel.Height - 2 * margin;

            float maxSide = Math.Max(triangle.SideA, Math.Max(triangle.SideB, triangle.SideC));
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

            graphics.DrawString("A", font, brush, (p1.X + p2.X) / 2 - 20, (p1.Y + p2.Y) / 2);
            graphics.DrawString("B", font, brush, (p3.X + p1.X) / 2 - 20, (p3.Y + p1.Y) / 2);
            graphics.DrawString("C", font, brush, (p2.X + p3.X) / 2 + 10, (p2.Y + p3.Y) / 2);
        }

        private void DrawGrid(Graphics graphics, UI ui)
        {
            int cellSize = 20;
            Pen gridPen = new Pen(Color.Gray, 1);

            for (int x = 0; x < ui.TrianglePanel.ClientSize.Width; x += cellSize)
            {
                graphics.DrawLine(gridPen, x, 0, x, ui.TrianglePanel.ClientSize.Height);
            }

            for (int y = 0; y < ui.TrianglePanel.ClientSize.Height; y += cellSize)
            {
                graphics.DrawLine(gridPen, 0, y, ui.TrianglePanel.ClientSize.Width, y);
            }
        }
    }
}
