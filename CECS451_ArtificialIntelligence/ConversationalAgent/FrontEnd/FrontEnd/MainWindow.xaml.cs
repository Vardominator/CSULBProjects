using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string intellia = "Intellia";
        string user = "RandomUser";

        // Calling R for backend NLP
        string rScriptExecutablePath = @"C:\Program Files\Microsoft\MRO\R-3.2.5\bin\Rscript.exe";
        string rScriptPath = @"F:\GitHub\CSULBProjects\CECS451_ArtificialIntelligence\ConversationalAgent\openNLPTest\openNLPTest\entityTest.R";
        string rScriptResult = string.Empty;

        public MainWindow()
        {

            InitializeComponent();
            conversationBox.Items.Add($"{intellia}: Welcome! My name Intellia.\nTell me a little about yourself :).");

        }

        private void userInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {

                conversationBox.Items.Add($"{user}: {userInputTextBox.Text}");
               
                rScriptResult = RScriptHandler.RunCommand(rScriptPath, rScriptExecutablePath, userInputTextBox.Text);
                conversationBox.Items.Add(rScriptResult);

                userInputTextBox.Clear();

            }
        }

    }
}
