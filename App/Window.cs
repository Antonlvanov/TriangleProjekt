namespace Triangle
{
    public partial class Window : Form
    {
        private readonly Context c;
        public Window()
        {
            c = new Context();
            ConfigureWindow(this);
            InitializeUI();
            InitializeEventHandlers();
        }
    }
}
