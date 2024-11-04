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
            Surface.Text = "Surface: ";
            Perimeter.Font = new Font("Arial", 14); 
            Perimeter.ForeColor = Color.Black; 
            Perimeter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; 
            Surface.Font = new Font("Arial", 14); 
            Surface.ForeColor = Color.Black; 
            Surface.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; 
            Perimeter.AutoSize = true;
            Surface.AutoSize = true;


            // labels to align with input fields
            a_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            b_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            c_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;

            a_input.Minimum = int.MinValue;
            a_input.Maximum = int.MaxValue;
            b_input.Minimum = int.MinValue;
            b_input.Maximum = int.MaxValue;
            c_input.Minimum = int.MinValue;
            c_input.Maximum = int.MaxValue;

            // controls

            mainPanel.Controls.Add(trianglePanel, 1, 0);

            var inputPanel = new TableLayoutPanel();
            inputPanel.Dock = DockStyle.Fill;
            inputPanel.ColumnCount = 2;
            inputPanel.RowCount = 4; // 3 rows for inputs, 1 for button

            inputPanel.Controls.Add(a_label, 0, 0);
            inputPanel.Controls.Add(a_input, 1, 0);
            inputPanel.Controls.Add(b_label, 0, 1);
            inputPanel.Controls.Add(b_input, 1, 1);
            inputPanel.Controls.Add(c_label, 0, 2);
            inputPanel.Controls.Add(c_input, 1, 2);

            inputPanel.Controls.Add(createTriangle, 0, 3);
            inputPanel.SetColumnSpan(createTriangle, 2);
            createTriangle.Anchor = AnchorStyles.None | AnchorStyles.Top;
            createTriangle.AutoSize = true;

            inputPanel.Controls.Add(Perimeter, 0, 4);
            inputPanel.SetColumnSpan(Perimeter, 2);
            inputPanel.Controls.Add(Surface, 0, 5);
            inputPanel.SetColumnSpan(Surface, 2);

            // Центрируем лейблы
            Perimeter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Surface.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            mainPanel.Controls.Add(trianglePanel, 1, 0);
            mainPanel.Controls.Add(inputPanel, 0, 0);

            this.Controls.Add(mainPanel);
        }

        private void CreateTriangle_Click(object sender, EventArgs e)
        {
            DeleteOldFiles();

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

            // setting panel size
            float panelWidth = trianglePanel.ClientSize.Width;
            float panelHeight = trianglePanel.ClientSize.Height;

            // indents from panel edges
            float margin = 0.35f * Math.Min(panelWidth, panelHeight);

            // calculating max triangle size for panel size
            float maxWidth = panelWidth - 2 * margin;
            float maxHeight = panelHeight - 2 * margin;

            // calculating surface size scale to fit triangle
            float scale = Math.Min(maxWidth / (float)triangle.a, maxHeight / (float)triangle.b);

            // finding triangle points to fit current size with panel
            PointF p1 = new PointF(margin, panelHeight - margin); // lower left 
            PointF p2 = new PointF(margin + (float)triangle.a * scale, panelHeight - margin); //  lower right
            // finding 3rd point of triangle with current scale
            float angle = (float)Math.Acos((Math.Pow(triangle.a, 2) + Math.Pow(triangle.b, 2) - Math.Pow(triangle.c, 2)) / (2 * triangle.a * triangle.b));
            PointF p3 = new PointF(
                p1.X + (float)(triangle.b * scale * Math.Cos(angle)),
                p1.Y - (float)(triangle.b * scale * Math.Sin(angle))
            );

            // drawing
            g.DrawLine(Pens.Black, p1, p2);
            g.DrawLine(Pens.Black, p2, p3);
            g.DrawLine(Pens.Black, p3, p1);
        }
        private void DeleteOldFiles()
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
