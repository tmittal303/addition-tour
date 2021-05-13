/* 
        Group-4
        Mittal, Tanya
        Kurian, Eldho
        Pou, Aileen
        Yu, Katey  
*/
using System.Windows.Controls;

namespace Addition_Tour
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Page
    {
        public Results()
        {
            InitializeComponent();
            ReadFromFile();
        }
        #region Read Data from Saved File
        public void ReadFromFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\AdditionTour\TestYourKnowledge\Data.txt");
            for (int i = 0; i < lines.Length; i += 4)
            {
                UserAnswer.Content += lines[i].Substring(15) + "\n"; // Writes answer that user gave
                RightAnswer.Content += lines[i + 1].Substring(16) + "\n"; // Writes correct answer
                Remark.Content += lines[i + 2] + "\n"; // Writes remark
            }
        }
        #endregion

        #region New Game Button
        private void StartNewGame_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
        #endregion
    }
}
