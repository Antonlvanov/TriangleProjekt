using _triangle = Triangle.TriangleObject.Triangle;

namespace Triangle
{
    public partial class Window
    {
        public void ConfigureWindow(Window window)
        {
            window.Width = 800;
            window.Height = 500;
            window.FormBorderStyle = FormBorderStyle.Fixed3D;
            window.BackColor = Color.LightGray;
            window.Text = "Triangle";
        }

        private void InitializeUI()
        {
            c.InitInterface.SetupUI();
            Controls.Add(c.UI.MainPanel);
        }

        private void InitializeEventHandlers()
        {
            c.UI.CreateTriangle.Click += CreateTriangle_Click;
            c.UI.TrianglePanel.Paint += TrianglePanel_Paint;
        }

        private void CreateTriangle_Click(object sender, EventArgs e)
        {
            float a = (float)this.c.UI.A_input.Value;
            float b = (float)this.c.UI.B_input.Value;
            float c = (float)this.c.UI.C_input.Value;

            this.c.Triangle = new _triangle(a, b, c);

            if (this.c.Triangle.ExistTriangle)
            {
                this.c.DataManager.DisplayTriangleInfo();
                this.c.UI.TrianglePanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Треугольник с такими сторонами не может существовать", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.c.UI.TrianglePanel.Invalidate();
            }
        }

        private void TrianglePanel_Paint(object sender, PaintEventArgs e)
        {
            if (c.Triangle != null)
            {
                c.Drawer.DrawTriangle(e.Graphics);
            }
        }
    }
}
