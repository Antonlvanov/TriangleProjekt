namespace Triangle
{
    public partial class TriangleWindow : Form
    {
        private Button createTriangle;
        private Graphics triangleImage;
        private NumericUpDown a_input, b_input, c_input;
        private Label Perimeter, Surface;
        private TableLayoutPanel trianglePanel = new TableLayoutPanel();
        private TableLayoutPanel mainPanel = new TableLayoutPanel();
        public TriangleWindow()
        {
            this.Width = 800;
            this.Height = 500;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.BackColor = Color.LightGray;


            createTriangle = new Button();
            Perimeter = new Label();
            Surface = new Label();
            a_input = new NumericUpDown();
            b_input = new NumericUpDown();
            c_input = new NumericUpDown();

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

            createTriangle.Text = "Make triangle";
            createTriangle.UseVisualStyleBackColor = true;
            // controls
            
            mainPanel.Controls.Add(trianglePanel, 1, 0);

            var inputPanel = new FlowLayoutPanel();
            inputPanel.Dock = DockStyle.Fill;
            inputPanel.Controls.Add(a_input);
            inputPanel.Controls.Add(b_input);
            inputPanel.Controls.Add(c_input);
            inputPanel.Controls.Add(createTriangle);
            mainPanel.Controls.Add(inputPanel, 0, 0);

            this.Controls.Add(mainPanel);

        }

    }
}
