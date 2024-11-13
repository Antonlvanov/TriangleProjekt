namespace Triangle.Interface
{
    public class UI
    {
        public TableLayoutPanel MainPanel { get; set; }
        public TableLayoutPanel InputPanel { get; set; }
        public TableLayoutPanel TrianglePanel { get; set; }
        public ListView TriangleInfoView { get; set; }
        public NumericUpDown A_input { get; set; }
        public NumericUpDown B_input { get; set; }
        public NumericUpDown C_input { get; set; }
        public Label A_label { get; set; }
        public Label B_label { get; set; }
        public Label C_label { get; set; }
        public Label TitleLabel { get; set; }
        public Button CreateTriangle { get; set; }

        public virtual TableLayoutPanel GetMainPanel() => MainPanel;

        public UI()
        {
            MainPanel = new TableLayoutPanel();
            InputPanel = new TableLayoutPanel();
            TrianglePanel = new TableLayoutPanel();

            TriangleInfoView = new ListView();

            A_input = new NumericUpDown();
            B_input = new NumericUpDown();
            C_input = new NumericUpDown();

            TitleLabel = new Label();
            A_label = new Label();
            B_label = new Label();
            C_label = new Label();

            CreateTriangle = new Button();
        }
    }
}
