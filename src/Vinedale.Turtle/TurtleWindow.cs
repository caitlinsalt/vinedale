using System.Windows.Forms;

namespace Vinedale.Turtle
{
    public partial class TurtleWindow : UserControl
    {
        public TurtleContext TurtleContext { get; set; }

        public TurtleWindow()
        {
            InitializeComponent();
        }
    }
}
