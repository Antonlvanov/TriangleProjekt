using static Triangle.Window;

namespace Triangle.Interface
{
    public class Init_UI
    {
        private readonly Context c;

        public Init_UI(Context context)
        {
            this.c = context;
        }

        public void SetupUI()
        {
            ConfigureMainPanel();
            ConfigureTrianglePanel();
            ConfigureInputPanel();
            ConfigureLabelsAndInputs();
            ConfigureTriangleInfoView();
            ConfigureCreateButton();
        }

        private void ConfigureMainPanel()
        {
            var mainPanel = c.UI.MainPanel;

            mainPanel.Dock = DockStyle.Fill;
            mainPanel.ColumnCount = 2;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            mainPanel.Controls.Add(c.UI.TrianglePanel, 1, 0);
            mainPanel.Controls.Add(c.UI.InputPanel, 0, 0);
        }

        private void ConfigureTrianglePanel()
        {
            var trianglePanel = c.UI.TrianglePanel;

            trianglePanel.Dock = DockStyle.Fill;
            trianglePanel.BackColor = Color.White;
        }

        private void ConfigureInputPanel()
        {
            var inputPanel = c.UI.InputPanel;

            inputPanel.Dock = DockStyle.Fill;
            inputPanel.ColumnCount = 2;
            inputPanel.RowCount = 4;
            inputPanel.SetColumnSpan(c.UI.CreateTriangle, 2);
            inputPanel.SetColumnSpan(c.UI.TriangleInfoView, 2);

            inputPanel.Controls.Add(c.UI.TitleLabel, 0, 0);
            inputPanel.SetColumnSpan(c.UI.TitleLabel, 2);

            inputPanel.Controls.Add(c.UI.A_label, 0, 1);
            inputPanel.Controls.Add(c.UI.A_input, 1, 1);
            inputPanel.Controls.Add(c.UI.B_label, 0, 2);
            inputPanel.Controls.Add(c.UI.B_input, 1, 2);
            inputPanel.Controls.Add(c.UI.C_label, 0, 3);
            inputPanel.Controls.Add(c.UI.C_input, 1, 3);

            inputPanel.Controls.Add(c.UI.CreateTriangle, 0, 4);
            inputPanel.Controls.Add(c.UI.TriangleInfoView, 0, 5);
        }

        private void ConfigureLabelsAndInputs()
        {
            var uiElements = c.UI;

            // labels / inputs
            uiElements.TitleLabel.Text = "Input";
            uiElements.TitleLabel.Font = new Font("Arial", 12);
            uiElements.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            uiElements.TitleLabel.Dock = DockStyle.Fill;
            uiElements.TitleLabel.Anchor = AnchorStyles.None | AnchorStyles.Top;
            uiElements.TitleLabel.AutoSize = true;

            uiElements.A_label.Text = "a:";
            uiElements.B_label.Text = "b:";
            uiElements.C_label.Text = "c:";
            uiElements.A_label.Font = new Font("Arial", 12);
            uiElements.B_label.Font = new Font("Arial", 12);
            uiElements.C_label.Font = new Font("Arial", 12);
            uiElements.A_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            uiElements.B_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            uiElements.C_label.TextAlign = System.Drawing.ContentAlignment.BottomRight;

            uiElements.A_input.Font = new Font("Arial", 11);
            uiElements.B_input.Font = new Font("Arial", 11);
            uiElements.C_input.Font = new Font("Arial", 11);
            uiElements.A_input.Maximum = int.MaxValue;
            uiElements.B_input.Maximum = int.MaxValue;
            uiElements.C_input.Maximum = int.MaxValue;
        }

        private void ConfigureTriangleInfoView()
        {
            var triangleInfoView = c.UI.TriangleInfoView;

            triangleInfoView.View = View.Details;
            triangleInfoView.FullRowSelect = true;
            triangleInfoView.Columns.Add("Parameter", 150);
            triangleInfoView.Columns.Add("Value", 100);
            triangleInfoView.Dock = DockStyle.Fill;
        }

        private void ConfigureCreateButton()
        {
            var createButton = c.UI.CreateTriangle;

            createButton.Text = "Make triangle";
            createButton.UseVisualStyleBackColor = true;
            createButton.Anchor = AnchorStyles.None | AnchorStyles.Top;
            createButton.AutoSize = true;
        }
    }
}
