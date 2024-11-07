using System.Windows.Forms.VisualStyles;
using System;
using System.Windows.Forms;

namespace Triangle
{
    public partial class TriangleWindow : Form
    {
        private Button createTriangle;
        private NumericUpDown a_input, b_input, c_input;
        private Label a_label, b_label, c_label, titleLabel;
        private TableLayoutPanel trianglePanel = new TableLayoutPanel();
        private TableLayoutPanel mainPanel = new TableLayoutPanel();
        private Triangle currentTriangle;
        private TableLayoutPanel inputPanel = new TableLayoutPanel();
        private ListView triangleInfoView;
        public TriangleWindow()
        {
            this.Width = 800;
            this.Height = 500;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.BackColor = Color.LightGray;
            this.Text = "Triangle";

            triangleInfoView = new ListView();
            var inputPanel = new TableLayoutPanel();
            a_input = new NumericUpDown();
            b_input = new NumericUpDown();
            c_input = new NumericUpDown();
            titleLabel = new Label();
            a_label = new Label();
            b_label = new Label();
            c_label = new Label();
            createTriangle = new Button();

            createTriangle.Click += new EventHandler(CreateTriangle_Click);
            trianglePanel.Paint += TrianglePanel_Paint;

            InitialSetup();
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
            inputPanel.ColumnCount = 2;
            inputPanel.RowCount = 4;
            inputPanel.SetColumnSpan(createTriangle, 2);
            inputPanel.SetColumnSpan(triangleInfoView, 2);

            titleLabel.Text = "Input";
            titleLabel.Font = new Font("Arial", 12);
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Fill;

            createTriangle.Text = "Make triangle";
            createTriangle.UseVisualStyleBackColor = true;

            triangleInfoView.View = View.Details;
            triangleInfoView.FullRowSelect = true;
            triangleInfoView.Columns.Add("Parameter", 150);
            triangleInfoView.Columns.Add("Value", 100);
            triangleInfoView.Dock = DockStyle.Fill;

            a_label.Text = "a:";
            b_label.Text = "b:";
            c_label.Text = "c:";
            a_label.Font = new Font("Arial", 12);
            b_label.Font = new Font("Arial", 12);
            c_label.Font = new Font("Arial", 12);

            a_input.Font = new Font("Arial", 11);
            b_input.Font = new Font("Arial", 11);
            c_input.Font = new Font("Arial", 11);

            titleLabel.Anchor = AnchorStyles.None | AnchorStyles.Top;
            titleLabel.AutoSize = true;
            createTriangle.Anchor = AnchorStyles.None | AnchorStyles.Top;
            createTriangle.AutoSize = true;

            // labels to align with input fields
            a_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            b_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            c_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;

            a_input.Maximum = int.MaxValue;
            b_input.Maximum = int.MaxValue;
            c_input.Maximum = int.MaxValue;

            // controls
            inputPanel.Controls.Add(titleLabel, 0, 0);  // Вставляем в первую строку и колонку
            inputPanel.SetColumnSpan(titleLabel, 2);

            inputPanel.Controls.Add(a_label, 0, 1);
            inputPanel.Controls.Add(a_input, 1, 1);
            inputPanel.Controls.Add(b_label, 0, 2);
            inputPanel.Controls.Add(b_input, 1, 2);
            inputPanel.Controls.Add(c_label, 0, 3);
            inputPanel.Controls.Add(c_input, 1, 3);

            inputPanel.Controls.Add(createTriangle, 0, 4);
            inputPanel.Controls.Add(triangleInfoView, 0, 5);

            mainPanel.Controls.Add(trianglePanel, 1, 0);
            mainPanel.Controls.Add(inputPanel, 0, 0);

            this.Controls.Add(mainPanel);
        }

        private void CreateTriangle_Click(object sender, EventArgs e)
        {
            double a = (double)a_input.Value;
            double b = (double)b_input.Value;
            double c = (double)c_input.Value;

            Triangle triangle = new Triangle(a, b, c);
            currentTriangle = triangle; // saving current triangle

            // check if triangle exist
            if (currentTriangle.ExistTriangle)
            {
                DisplayTriangleInfo(triangle);
                trianglePanel.Invalidate(); // updating drawing
                triangle.SaveDataXML();
                DeleteTempFiles();
            }
            else
            {
                MessageBox.Show("Ошибка при создании треугольника. Треугольник с такими сторонами не может существовать.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                trianglePanel.Invalidate();
            }
        }

        private void TrianglePanel_Paint(object sender, PaintEventArgs e)
        {
            if (currentTriangle != null)
            {
                DrawGrid(e.Graphics, trianglePanel.ClientSize.Width, trianglePanel.ClientSize.Height);
                DrawTriangle(e.Graphics, currentTriangle);
            }
        }

        private void DrawTriangle(Graphics g, Triangle triangle)
        {
            g.Clear(Color.White);

            DrawGrid(g, trianglePanel.ClientSize.Width, trianglePanel.ClientSize.Height);

            // getting panel size
            float panelWidth = trianglePanel.ClientSize.Width;
            float panelHeight = trianglePanel.ClientSize.Height;

            // indents from panel edges
            float margin = 0.2f * Math.Min(panelWidth, panelHeight);
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

            // draw signs
            DrawSideSigns(p1, p2, p3, g);
            triangle.SaveDataXML();
        }


        public void DrawSideSigns(PointF p1, PointF p2, PointF p3, Graphics g)
        {
            Font font = new Font("Arial", 12, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);

            // finding points for signs
            PointF midAB = new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            PointF midBC = new PointF((p2.X + p3.X) / 2 + 20, (p2.Y + p3.Y) / 2);
            PointF midCA = new PointF((p3.X + p1.X) / 2 - 30, (p3.Y + p1.Y) / 2);

            PointF arcA = new PointF(p1.X - 20, p1.Y);
            PointF arcB = new PointF(p2.X, p2.Y);
            PointF arcC = new PointF(p3.X - 10, p3.Y - 20);

            // draw signs for sides
            g.DrawString("a", font, brush, midAB.X, midAB.Y); // a
            g.DrawString("b", font, brush, midCA.X, midCA.Y); // b
            g.DrawString("c", font, brush, midBC.X, midBC.Y); // c

            g.DrawString("A", font, brush, arcA.X, arcA.Y);
            g.DrawString("B", font, brush, arcB.X, arcB.Y);
            g.DrawString("C", font, brush, arcC.X, arcC.Y);
        }

        private void DrawGrid(Graphics g, int width, int height)
        {
            int cellSize = 20; 
            Pen gridPen = new Pen(Color.Gray, 1);

            for (int x = 0; x < width; x += cellSize)
            {
                g.DrawLine(gridPen, x, 0, x, height);
            }

            for (int y = 0; y < height; y += cellSize)
            {
                g.DrawLine(gridPen, 0, y, width, y);
            }
        }
        private float CalculateScale(Triangle triangle, float panelWidth, float panelHeight, float margin)
        {
            float maxWidth = panelWidth - 2 * margin;
            float maxHeight = panelHeight - 2 * margin;

            float maxSide = (float)Math.Max(triangle.GetSetA, Math.Max(triangle.GetSetB, triangle.GetSetC));

            return Math.Min(maxWidth / maxSide, maxHeight / maxSide);
        }

        private void DisplayTriangleInfo(Triangle triangle)
        {
            triangleInfoView.Items.Clear();

            triangleInfoView.Items.Add(new ListViewItem(["Side a", triangle.GetSetA.ToString()]));
            triangleInfoView.Items.Add(new ListViewItem(["Side b", triangle.GetSetB.ToString()]));
            triangleInfoView.Items.Add(new ListViewItem(["Side c", triangle.GetSetC.ToString()]));

            triangleInfoView.Items.Add(new ListViewItem(["Angle A", triangle.AngleA.ToString("F2") + "°"]));
            triangleInfoView.Items.Add(new ListViewItem(["Angle B", triangle.AngleB.ToString("F2") + "°"]));
            triangleInfoView.Items.Add(new ListViewItem(["Angle C", triangle.AngleC.ToString("F2") + "°"]));

            triangleInfoView.Items.Add(new ListViewItem(["Perimeter", triangle._perimeter.ToString("F2")]));
            triangleInfoView.Items.Add(new ListViewItem(["Surface", triangle._surface.ToString("F2")]));

            triangleInfoView.Items.Add(new ListViewItem(["Type", triangle.TypeRusky()]));

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
