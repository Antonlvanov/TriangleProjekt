
using Triangle.Interface;
using _triangle = Triangle.TriangleObject.Triangle;

namespace Triangle
{
    public class Context
    {
        public _triangle Triangle { get; set; }
        public UI UI { get; }
        public Drawer Drawer { get; }
        public DataManager DataManager { get; }
        public Init_UI InitInterface { get; }

        public Context()
        {
            UI = new UI();
            DataManager = new DataManager(this);
            Drawer = new Drawer(this);
            InitInterface = new Init_UI(this);
        }
    }
}
