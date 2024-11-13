using _triangle = Triangle.TriangleObject.Triangle;

namespace Triangle
{
    public partial class Form1 : Form
    {
        Button btn;
        TextBox txtA, txtB, txtC;
        ListView listView1;
        Label labelA, labelB, labelC;
        PictureBox pictureBox;
        _triangle Triangle;

        public Form1()
        {
            this.Height = 420;
            this.Width = 800;
            this.Text = "Töötamine kolmnurgaga";
            //Button - btn
            btn = new Button();
            btn.Text = "Käivitamine";
            btn.Font = new Font("Arial", 28, FontStyle.Italic);
            btn.AutoSize = true;
            btn.Height = 80;
            btn.Width = 50;
            btn.FlatAppearance.BorderSize = 6;
            btn.FlatAppearance.BorderColor = Color.LightSeaGreen;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Location = new Point(460, 80);
            btn.BackColor = Color.Khaki;
            Controls.Add(btn);
            btn.Click += Btn_Click;

            //TextBox - txtA
            txtA = new TextBox();
            txtA.Location = new Point(270, 270);
            txtA.Font = new Font("Arial", 13);
            txtA.Width = 100;
            txtA.Height = 80;
            Controls.Add(txtA);

            //Label - labelA
            labelA = new Label();
            labelA.Text = "Külg A";
            labelA.Font = new Font("Arial", 13, FontStyle.Bold | FontStyle.Underline);
            labelA.ForeColor = Color.RoyalBlue;
            labelA.AutoSize = true;
            labelA.Location = new Point(200, 270);
            Controls.Add(labelA);

            //TextBox - txtB
            txtB = new TextBox();
            txtB.Location = new Point(270, 300);
            txtB.Font = new Font("Arial", 13);
            txtB.Width = 100;
            txtB.Height = 80;
            Controls.Add(txtB);

            //Label - labelB
            labelB = new Label();
            labelB.Text = "Külg B";
            labelB.Font = new Font("Arial", 13, FontStyle.Bold | FontStyle.Underline);
            labelB.ForeColor = Color.RoyalBlue;
            labelB.AutoSize = true;
            labelB.Location = new Point(200, 300);
            Controls.Add(labelB);

            //TextBox - txtC
            txtC = new TextBox();
            txtC.Location = new Point(270, 330);
            txtC.Font = new Font("Arial", 13);
            txtC.Width = 100;
            txtC.Height = 80;
            Controls.Add(txtC);

            //Label - labelC
            labelC = new Label();
            labelC.Text = "Külg C";
            labelC.Font = new Font("Arial", 13, FontStyle.Bold | FontStyle.Underline);
            labelC.ForeColor = Color.RoyalBlue;
            labelC.AutoSize = true;
            labelC.Location = new Point(200, 330);
            Controls.Add(labelC);

            //listView1
            listView1 = new ListView();
            listView1.Location = new Point(10, 10);
            listView1.Font = new Font("Arial", 10);
            listView1.Width = 360;
            listView1.Height = 250;
            listView1.View = View.Details;
            listView1.Columns.Add("Väli", 165);
            listView1.Columns.Add("Väärtused", 190);
            listView1.BackColor = Color.Silver;
            listView1.ForeColor = Color.DarkBlue;
            Controls.Add(listView1);

            //pictureBox
            pictureBox = new PictureBox();
            pictureBox.Location = new Point(500, 200);
            pictureBox.Size = new Size(150, 150);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = Image.FromFile(@"..\..\..\Equilateral.png");
            Controls.Add(pictureBox);
        }
        int clicks = 0;
        private void Btn_Click(object? sender, EventArgs e)
        {
            clicks++;
            if (clicks % 5 == 0)
            {
                Window window = new();
                window.Show();
            }
            if (txtA.Text == "" || txtB.Text == "" || txtC.Text == "") return;
            var triangle = new _triangle(double.Parse(txtA.Text), double.Parse(txtB.Text), double.Parse(txtC.Text));
            listView1.Items.Clear();
            listView1.Items.Add("Külg A");
            listView1.Items.Add("Külg B");
            listView1.Items.Add("Külg C");
            listView1.Items.Add("Perimeeter");
            listView1.Items.Add("Piirkond");
            listView1.Items.Add("Kas see on olemas?");
            listView1.Items.Add("Täpsustaja");

            listView1.Items[0].SubItems.Add(triangle.outputA());
            listView1.Items[1].SubItems.Add(triangle.outputB());
            listView1.Items[2].SubItems.Add(triangle.outputC());
            listView1.Items[5].SubItems.Add(Convert.ToString(triangle.KasOnOlemas));
            if (triangle.ExistTriangle)
            {
                listView1.Items[3].SubItems.Add(Convert.ToString(triangle.Perimeter.ToString()));
                listView1.Items[4].SubItems.Add(Convert.ToString(triangle.Surface.ToString()));
                listView1.Items[6].SubItems.Add(Convert.ToString(triangle.TypeEst()));
                
                switch (triangle.Type)
                {
                    case _triangle.TriangleType.Equilateral:
                        pictureBox.Image = Image.FromFile(@"..\..\..\Equilateral.png");
                        break;
                    case _triangle.TriangleType.Isosceles:
                        pictureBox.Image = Image.FromFile(@"..\..\..\Isosceles.png");
                        break;
                    default:
                        pictureBox.Image = Image.FromFile(@"..\..\..\Scalene.png");
                        break;
                }
            }
            else
            {
                listView1.Items[3].SubItems.Add("");
                listView1.Items[4].SubItems.Add("");
                listView1.Items[5].SubItems.Add("");
            }
        }
    }
}
