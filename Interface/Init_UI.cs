using static Triangle.Window;

namespace Triangle.Interface
{
    public class Init_UI : UI
    {
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
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.ColumnCount = 2;
            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            
            MainPanel.Controls.Add(TrianglePanel, 1, 0);
            MainPanel.Controls.Add(InputPanel, 0, 0);
        }

        private void ConfigureTrianglePanel()
        {
            TrianglePanel.Dock = DockStyle.Fill;
            TrianglePanel.BackColor = Color.White;
        }

        private void ConfigureInputPanel()
        {
            InputPanel.Dock = DockStyle.Fill;
            InputPanel.ColumnCount = 2;
            InputPanel.RowCount = 4;
            InputPanel.SetColumnSpan(CreateTriangle, 2);
            InputPanel.SetColumnSpan(TriangleInfoView, 2);
            
            InputPanel.Controls.Add(TitleLabel, 0, 0);
            InputPanel.SetColumnSpan(TitleLabel, 2);
            
            InputPanel.Controls.Add(A_label, 0, 1);
            InputPanel.Controls.Add(A_input, 1, 1);
            InputPanel.Controls.Add(B_label, 0, 2);
            InputPanel.Controls.Add(B_input, 1, 2);
            InputPanel.Controls.Add(C_label, 0, 3);
            InputPanel.Controls.Add(C_input, 1, 3);
            
            InputPanel.Controls.Add(CreateTriangle, 0, 4);
            InputPanel.Controls.Add(TriangleInfoView, 0, 5);
        }

        private void ConfigureLabelsAndInputs()
        {
            // labels / inputs
            TitleLabel.Text = "Sisend";
            TitleLabel.Font = new Font("Arial", 12);
            TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            TitleLabel.Dock = DockStyle.Fill;
            TitleLabel.Anchor = AnchorStyles.None | AnchorStyles.Top;
            TitleLabel.AutoSize = true;

            A_label.Text = "A:";
            B_label.Text = "B:";
            C_label.Text = "C:";
            A_label.Font = new Font("Arial", 12);
            B_label.Font = new Font("Arial", 12);
            C_label.Font = new Font("Arial", 12);
            A_label.TextAlign = ContentAlignment.BottomRight;
            B_label.TextAlign = ContentAlignment.BottomRight;
            C_label.TextAlign = ContentAlignment.BottomRight;

            A_input.Font = new Font("Arial", 11);
            B_input.Font = new Font("Arial", 11);
            C_input.Font = new Font("Arial", 11);
            A_input.Maximum = int.MaxValue;
            B_input.Maximum = int.MaxValue;
            C_input.Maximum = int.MaxValue;
        }

        private void ConfigureTriangleInfoView()
        {
            TriangleInfoView.View = View.Details;
            TriangleInfoView.FullRowSelect = true;
            TriangleInfoView.Columns.Add("Väli", 150);
            TriangleInfoView.Columns.Add("Väärtused", 100);
            TriangleInfoView.Dock = DockStyle.Fill;
        }

        private void ConfigureCreateButton()
        {
            CreateTriangle.Text = "Tee kolmnurk";
            CreateTriangle.UseVisualStyleBackColor = true;
            CreateTriangle.Anchor = AnchorStyles.None | AnchorStyles.Top;
            CreateTriangle.AutoSize = true;
        }
    }
}
