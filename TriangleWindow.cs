using System.Windows.Forms.VisualStyles;
using System;
using System.Windows.Forms;

namespace Triangle
{
    public partial class TriangleWindow : Form
    {
        private Button createTriangle;
        private Graphics triangleImage;
        private NumericUpDown a_input, b_input, c_input;
        private Label a_label, b_label, c_label, Perimeter, Surface;
        private TableLayoutPanel trianglePanel = new TableLayoutPanel();
        private TableLayoutPanel mainPanel = new TableLayoutPanel();
        private Triangle currentTriangle;
        private TableLayoutPanel inputPanel = new TableLayoutPanel();
        public TriangleWindow()
        {
            this.Width = 800;
            this.Height = 500;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.BackColor = Color.LightGray;

            createTriangle = new Button();
            createTriangle.Text = "Make triangle";
            createTriangle.UseVisualStyleBackColor = true;
            createTriangle.Click += new EventHandler(CreateTriangle_Click);

            var inputPanel = new TableLayoutPanel();
            Perimeter = new Label();
            Surface = new Label();
            a_input = new NumericUpDown();
            b_input = new NumericUpDown();
            c_input = new NumericUpDown();
            a_label = new Label();
            b_label = new Label();
            c_label = new Label();

            InitialSetup();

            trianglePanel.Paint += TrianglePanel_Paint;
        }

        private void InitialSetup()
        {
            //params
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.ColumnCount = 2;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            trianglePanel.Dock = DockStyle.Fill;
            trianglePanel.BackColor = Color.White;

            inputPanel.Dock = DockStyle.Fill;
            inputPanel.SetColumnSpan(createTriangle, 2);
            inputPanel.ColumnCount = 2;
            inputPanel.RowCount = 4; // 3 rows for inputs, 1 for button

            createTriangle.Anchor = AnchorStyles.None | AnchorStyles.Top;
            createTriangle.AutoSize = true;

            a_label.Text = "Side A:";
            b_label.Text = "Side B:";
            c_label.Text = "Side C:";
            a_label.Font = new Font("Arial", 12);
            b_label.Font = new Font("Arial", 12);
            c_label.Font = new Font("Arial", 12);

            a_input.Font = new Font("Arial", 11);
            b_input.Font = new Font("Arial", 11);
            c_input.Font = new Font("Arial", 11);

            // perimeter and surface
            Perimeter.Text = "Perimeter: ";
            Perimeter.Font = new Font("Arial", 14); 
            Perimeter.ForeColor = Color.Black; 
            Perimeter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Perimeter.AutoSize = true;
            Perimeter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Surface.Text = "Surface: ";
            Surface.Font = new Font("Arial", 14); 
            Surface.ForeColor = Color.Black; 
            Surface.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Surface.AutoSize = true;
            Surface.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // labels to align with input fields
            a_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            b_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            c_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;

            a_input.Maximum = int.MaxValue;
            b_input.Maximum = int.MaxValue;
            c_input.Maximum = int.MaxValue;

            // controls
            inputPanel.Controls.Add(a_label, 0, 0);
            inputPanel.Controls.Add(a_input, 1, 0);
            inputPanel.Controls.Add(b_label, 0, 1);
            inputPanel.Controls.Add(b_input, 1, 1);
            inputPanel.Controls.Add(c_label, 0, 2);
            inputPanel.Controls.Add(c_input, 1, 2);

            inputPanel.Controls.Add(Perimeter, 0, 4);
            inputPanel.SetColumnSpan(Perimeter, 2);
            inputPanel.Controls.Add(Surface, 0, 5);
            inputPanel.SetColumnSpan(Surface, 2);

            inputPanel.Controls.Add(createTriangle, 0, 3);

            mainPanel.Controls.Add(trianglePanel, 1, 0);
            mainPanel.Controls.Add(inputPanel, 0, 0);

            this.Controls.Add(mainPanel);
        }

        private void CreateTriangle_Click(object sender, EventArgs e)
        {
            DeleteTempFiles();

            double a = (double)a_input.Value;
            double b = (double)b_input.Value;
            double c = (double)c_input.Value;

            Triangle triangle = new Triangle(a, b, c);
            currentTriangle = triangle; // saving current triangle

            // check for unequal sides
            if (currentTriangle.ExistTriangle)
            {
                CalculateAndDisplayTriangleData(triangle);
                trianglePanel.Invalidate(); // updating drawing
            }
            else
            {
                MessageBox.Show("Треугольник с такими сторонами не может существовать. Сумма длин любых двух сторон треугольника должна быть больше длины третьей стороны.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                currentTriangle = null;
                trianglePanel.Invalidate();
            }
        }

        private void CalculateAndDisplayTriangleData(Triangle triangle)
        {
            // showing perimeter and surface
            Perimeter.Text = $"Perimeter: {triangle.Perimeter():F2}";
            Surface.Text = $"Surface: {triangle.Surface():F2}";
        }

        private void TrianglePanel_Paint(object sender, PaintEventArgs e)
        {
            if (currentTriangle != null)
            {
                DrawTriangle(e.Graphics, currentTriangle);
            }
        }

        private void DrawTriangle(Graphics g, Triangle triangle)
        {
            g.Clear(Color.White);

            // getting panel size
            float panelWidth = trianglePanel.ClientSize.Width;
            float panelHeight = trianglePanel.ClientSize.Height;

            // indents from panel edges
            float margin = 0.1f * Math.Min(panelWidth, panelHeight);
            float centerX = panelWidth / 2;
            float centerY = panelHeight / 2;

            // calculating surface size scale to fit panel
            float scale = CalculateScale(triangle, panelWidth, panelHeight, margin);

            // finding new adjested triangle drawing points
            PointF p1 = new PointF(0, 0); //  lower left 
            PointF p2 = new PointF((float)triangle.a * scale, 0); // lower right

            // finding 3rd point of triangle with current scale and angle
            float angle = (float)Math.Acos((Math.Pow(triangle.a, 2) + Math.Pow(triangle.b, 2) - Math.Pow(triangle.c, 2)) / (2 * triangle.a * triangle.b));
            PointF p3 = new PointF(
                (float)(triangle.b * scale * Math.Cos(angle)),
                -(float)(triangle.b * scale * Math.Sin(angle))
            );

            // finding panel center
            float triangleCenterX = (p1.X + p2.X + p3.X) / 3;
            float triangleCenterY = (p1.Y + p2.Y + p3.Y) / 3;

            // adjusting offset to fit panel center
            float offsetX = centerX - triangleCenterX;
            float offsetY = centerY - triangleCenterY;

            // adjusting triangle points by offset
            p1 = new PointF(p1.X + offsetX, p1.Y + offsetY);
            p2 = new PointF(p2.X + offsetX, p2.Y + offsetY);
            p3 = new PointF(p3.X + offsetX, p3.Y + offsetY);

            // draw
            Pen pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, p1, p2);
            g.DrawLine(pen, p2, p3);
            g.DrawLine(pen, p3, p1);

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);

            // finding points for signs
            PointF midAB = new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2 + 10); 
            PointF midBC = new PointF((p2.X + p3.X) / 2 + 20, (p2.Y + p3.Y) / 2); 
            PointF midCA = new PointF((p3.X + p1.X) / 2 - 30, (p3.Y + p1.Y) / 2);

            // draw signs for sides
            g.DrawString("A", font, brush, midBC.X, midBC.Y); // A
            g.DrawString("B", font, brush, midCA.X, midCA.Y); // B
            g.DrawString("C", font, brush, midAB.X, midAB.Y); // C
        }


        private float CalculateScale(Triangle triangle, float panelWidth, float panelHeight, float margin)
        {
            float maxWidth = panelWidth - 2 * margin;
            float maxHeight = panelHeight - 2 * margin;

            float maxSide = (float)Math.Max(triangle.GetSetA, Math.Max(triangle.GetSetB, triangle.GetSetC));

            return Math.Min(maxWidth / maxSide, maxHeight / maxSide);
        }

        private PointF CalculateThirdPoint(Triangle triangle, float scale)
        {
            float a_scaled = (float)triangle.GetSetA * scale;
            float b_scaled = (float)triangle.GetSetB * scale;

            float p1X = 0;
            float p1Y = 0;

            float p2X = a_scaled;
            float p2Y = 0;

            float p3X = b_scaled * (float)Math.Cos(triangle.AngleC);
            float p3Y = b_scaled * (float)Math.Sin(triangle.AngleC);

            return new PointF(p3X, p3Y);
        }

        public void DrawSideSigns()
        {

        }

        private void DeleteTempFiles()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory; 
            string[] oldFiles = Directory.GetFiles(path, "*.TMP");

            foreach (string file in oldFiles)
            {
                try
                {
                    File.Delete(file); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить файл: {file}\nОшибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
