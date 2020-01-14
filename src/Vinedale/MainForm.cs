using Logo.Interpretation;
using Logo.Os;
using System.IO;
using System.Windows.Forms;
using Vinedale.Shell;
using Vinedale.Turtle;

namespace Vinedale
{
    /// <summary>
    /// The main window of Vinedale.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly StreamWriter _debugOutput;
        private readonly StreamWriter _standardOutput;
        private readonly Interpretor _interp;
        private readonly TurtleContext _turtle;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _turtle = new TurtleContext();
            turtleWindow1.TurtleContext = _turtle;
            _debugOutput = new StreamWriter(new ShellOutputStream(debugShell)) { AutoFlush = true };
            _standardOutput = new StreamWriter(new ShellOutputStream(interpShell)) { AutoFlush = true };
            _interp = new Interpretor(_standardOutput, _debugOutput, DebugMessageLevel.Logorrheic);
            _interp.LoadModule(new SystemCommands());
            _interp.LoadModule(new ShellCommands());
            _interp.LoadModule(new TurtleCommands(_turtle));
            _interp.StartInteractiveInterpretor();
            interpShell.Enabled = true;
            debugShell.Enabled = true;
        }

        private void InterpShell_CommandEntered(object sender, CommandEnteredEventArgs e)
        {
            ProcessCommand(e.Command);
        }

        private void ProcessCommand(string command)
        {
            if (_interp != null)
            {
                InterpretationResultType result = _interp.Interpret(command);
                if (result == InterpretationResultType.SuccessIncomplete)
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
