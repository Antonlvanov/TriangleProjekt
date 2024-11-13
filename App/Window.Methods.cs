using _triangle = Triangle.TriangleObject.Triangle;
using Triangle.Interface;

namespace Triangle
{
    public partial class Window
    {
        private UI Interface = new Init_UI();
        private DataManager dataManager = new DataManager();
        private Drawer drawer = new Drawer();
        private _triangle triangle;

        public static void ConfigureWindow(Window window)
        {
            window.Width = 800;
            window.Height = 500;
            window.FormBorderStyle = FormBorderStyle.Fixed3D;
            window.BackColor = Color.LightGray;
            window.Text = "Triangle";
        }

        private void Initialize()
        {
            ((Init_UI)Interface).SetupUI();
            Interface.CreateTriangle.Click += CreateTriangle_Click;
            Interface.TrianglePanel.Paint += TrianglePanel_Paint;
            Controls.Add(Interface.GetMainPanel());
        }

        private void CreateTriangle_Click(object sender, EventArgs e)
        {
            float a = (float)Interface.A_input.Value;
            float b = (float)Interface.B_input.Value;
            float c = (float)Interface.C_input.Value;

            triangle = new _triangle(a, b, c);

            if (triangle.ExistTriangle)
            {
                dataManager.DisplayTriangleInfo(triangle, Interface.TriangleInfoView);
                dataManager.SaveDataXML(triangle);
                Interface.TrianglePanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Selliste külgedega kolmnurka ei saa eksisteerida", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TrianglePanel_Paint(object sender, PaintEventArgs e)
        {
            if (triangle != null)
            {
                drawer.DrawTriangle(triangle, Interface, e.Graphics);
            }
        }
    }
}

