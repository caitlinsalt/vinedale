using Logo.Core.Interpretation;
using Logo.Windows;
using System.IO;
using System.Windows.Forms;
using Vinedale.Shell;
using Vinedale.WfTurtle;

namespace Vinedale
{
    /// <summary>
    /// The window containing the main Vinedale front end.
    /// </summary>
    public partial class FrontEnd : Form
    {
        private StreamWriter _debugOutput;
        private StreamWriter _standardOutput;
        private Interpretor _interp;
        private TurtleContext _turtle;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FrontEnd()
        {
            InitializeComponent();
            _turtle = new TurtleContext();
            turtleWindow1.TurtleContext = _turtle;
            _debugOutput = new StreamWriter(new ShellOutputStream(debugShell)) { AutoFlush = true };
            //_debugOutput = new StreamWriter(new ShellOutputStream(null));
            _standardOutput = new StreamWriter(new ShellOutputStream(interpShell)) { AutoFlush = true };
            _interp = new Interpretor(_standardOutput, _debugOutput, DebugMessageLevel.Logorrheic);
            _interp.LoadModule(new SystemCommands());
            _interp.LoadModule(new ShellCommands());
            _interp.LoadModule(new TurtleCommands(_turtle));
            _interp.StartInteractiveInterpretor();
            interpShell.Enabled = true;
            debugShell.Enabled = true;
        }

        private void interpShell_CommandEntered(object sender, ShellControl.CommandEnteredEventArgs e)
        {
            if (_interp != null)
            {
                InterpretationResult result = _interp.Interpret(e.Command);
                if (result == InterpretationResult.SuccessIncomplete)
                {
                    interpShell.Prompt = "> ";
                }
                else
                {
                    interpShell.Prompt = "? ";
                }
            }
        }
    }
}
