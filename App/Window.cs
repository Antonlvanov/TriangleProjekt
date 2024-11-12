namespace Triangle
{
    public partial class Window : Form
    {
        private Context c;
        public Window()
        {
            c = new Context();
            ConfigureWindow(this);
            InitializeUI();
            InitializeEventHandlers();
        }
    }
}
