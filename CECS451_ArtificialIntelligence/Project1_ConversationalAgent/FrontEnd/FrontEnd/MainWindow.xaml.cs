using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for IntelAgent
    /// </summary>
    public partial class MainWindow : Window
    {

        string intellia = "Intellia";
        string user = "RandomUser";

        // Calling R for backend NLP
        string rScriptExecutablePath = @"C:\Program Files\Microsoft\MRO\R-3.2.5\bin\Rscript.exe";
        string rScriptPath = @"F:\GitHub\CSULBProjects\CECS451_ArtificialIntelligence\ConversationalAgent\openNLPTest\openNLPTest\entityTest.R";
        string rScriptResult = string.Empty;

        string[] speechTolkens = new string[4];

        bool gotName = false;
        bool gotPlaceOfOrigin = false;
        bool gotDestination = false;
        bool gotDate = false;

        string placeOfOrigin = string.Empty;
        string destination = string.Empty;
        string date = string.Empty;
        string flightSearch = string.Empty;

        public MainWindow()
        {

            InitializeComponent();
            conversationBox.Items.Add($"{intellia}: Welcome! My name Intellia.\nTell me a little about yourself :).");

        }
        private void userInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                rScriptResult = RScriptHandler.RunCommand(rScriptPath, rScriptExecutablePath, userInputTextBox.Text);

                Console.WriteLine(rScriptResult);

                speechTolkens = rScriptResult.Split('\r');
                speechTolkens.Select(tolken => tolken.Trim(new char[] { '\r', '\n' }));

                if (gotName == false)
                {
                    if (speechTolkens[0] != "character(0)")
                    {
                        user = speechTolkens[0];
                        conversationBox.Items.Add($"{user}: {userInputTextBox.Text}");
                        gotName = true;
                        conversationBox.Items.Add($"{intellia}: Hey {speechTolkens[0]}, welcome! Where are you from?");
                    }
                    else
                    {
                        conversationBox.Items.Add($"{intellia}: I'm sorry, I did not quite catch that.\nDo you have a name?");
                    }

                    userInputTextBox.Clear();
                    return;

                }

                if (gotName == true && gotPlaceOfOrigin == false)
                {
                    if (speechTolkens[1] != "character(0)")
                    {
                        conversationBox.Items.Add($"{user}: {userInputTextBox.Text}");
                        conversationBox.Items.Add($"{intellia}: {speechTolkens[1].Trim('\n')} is nice, but everyone needs to\ntravel every now and then.");
                        conversationBox.Items.Add($"{intellia}: Is there somewhere you would like to visit?");
                        placeOfOrigin = speechTolkens[1];
                    }
                    else
                    {
                        conversationBox.Items.Add($"{intellia}: I don't know where that is actually,\nbut I bet you need a serious getaway!");
                    }

                    gotPlaceOfOrigin = true;

                    userInputTextBox.Clear();
                    return;

                }
                

                if(gotPlaceOfOrigin == true && gotDestination == false)
                {

                    if(speechTolkens[1] != "character(0)")
                    {
                        conversationBox.Items.Add($"{user}: {userInputTextBox.Text}");
                        conversationBox.Items.Add($"{intellia}: {speechTolkens[1].Trim('\n')} it is. I'll see what I could find!");
                        conversationBox.Items.Add($"{intellia}: Do you have a date for me?");
                        gotDestination = true;
                        destination = speechTolkens[1];
                    }
                    else
                    {
                        conversationBox.Items.Add($"{intellia}: Try again :(. I don't think that place exists.");
                    }

                    userInputTextBox.Clear();
                    return;

                }


                if(gotDestination == true && gotDate == false)
                {
                    if(speechTolkens[3] != "character(0)")
                    {
                        conversationBox.Items.Add($"{user}: {userInputTextBox.Text}");
                        conversationBox.Items.Add($"{intellia}: Give me a sec. I am searching some flights!");
                        gotDate = true;
                        date = speechTolkens[3];

                        SetUpFlight(placeOfOrigin, destination, date);

                    }
                    else
                    {
                        conversationBox.Items.Add($"{intellia}: Okay stop messing around. Can you give me a real date?");
                    }

                    userInputTextBox.Clear();
                    return;

                }

            }
        }

        public void SetUpFlight(string placeOfOrigin, string destination, string date)
        {

            string[] splitDate = date.Split(' ');
            string finalDate = string.Empty;

            for (int i = 0; i < splitDate.Length; i++)
            {
                finalDate += splitDate[i] + "+";
            }

            finalDate.Trim('+');

            string googleSearch = $"{placeOfOrigin}+{destination}+{finalDate}";

            System.Diagnostics.Process.Start("http://google.com/search?q=" + googleSearch + "&ie=utf-8&oe=utf-8");

        }

    }
}
