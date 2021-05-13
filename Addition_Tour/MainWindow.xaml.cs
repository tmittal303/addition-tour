/* 
        Group-4
        Mittal, Tanya
        Kurian, Eldho
        Pou, Aileen
        Yu, Katey  
*/
using System;
using System.IO;
using System.Windows;

namespace Addition_Tour
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileContent = string.Empty;
        decimal result = 0;
        int points = 0;
        private int operationType = 0;
        Random r = new Random();
        const string OUTPUT_FILE = "Data.txt";
        const string APPDATA_PATH = "TestYourKnowledge";
        const int RANDOM_NUMBER_RANGE_LOWEST = 100;
        const int RANDOM_NUMBER_RANGE_HIGHEST = 500;
        const int OPERATION_TYPE_RANGE_LOWEST = 1;
        const int OPERATION_TYPE_RANGE_HIGHEST = 5;
        const int POINTS_CHANGE = 1;
        const int POINTS_WIN = 10;
        const int POINTS_LOSE = -5;
        public MainWindow()
        {
            InitializeComponent();
            RandomNumber();
        }

        public enum Operation
        {
            Add = 1,
            Subtract = 2,
            Divide = 3,
            Multiply = 4,
            Modulo = 5
        }

        #region Random Number and Random operation generator
        private void RandomNumber()
        {
            AdditionFirst.Content = r.Next(RANDOM_NUMBER_RANGE_LOWEST, RANDOM_NUMBER_RANGE_HIGHEST);
            AdditionSecond.Content = r.Next(RANDOM_NUMBER_RANGE_LOWEST, RANDOM_NUMBER_RANGE_HIGHEST);
            operationType = r.Next(OPERATION_TYPE_RANGE_LOWEST, OPERATION_TYPE_RANGE_HIGHEST);
            decimal a = Convert.ToInt32(AdditionFirst.Content);
            decimal b = Convert.ToInt32(AdditionSecond.Content);
            switch (operationType)
            {
                case (int)Operation.Add: // 1 is for add
                    result = a + b;
                    OperationType.Content = "+";
                    break;
                case (int)Operation.Subtract: // 2 is for subtract
                    result = a - b;
                    OperationType.Content = "-";
                    UserAnswer.ToolTip = "If answer is negative put '-' sign.";
                    break;
                case (int)Operation.Multiply: // 3 is for multiply
                    result = a * b;
                    OperationType.Content = "*";
                    break;
                case (int)Operation.Divide: // 4 is for divide
                    result = Math.Round(a / b, 2);
                    OperationType.Content = "/";
                    UserAnswer.ToolTip = "Enter the answer upto two decimal places.";
                    break;
                case (int)Operation.Modulo: // 5 is for mod
                    result = a % b;
                    OperationType.Content = "%";
                    break;
            }
        }
        #endregion

        #region Save To File
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string path = @"c:\AdditionTour";
            string fullpath = Path.Combine(path, APPDATA_PATH);
            if (!Directory.Exists(fullpath))
                Directory.CreateDirectory(fullpath);
            fullpath = Path.Combine(fullpath, OUTPUT_FILE);
            try
            {
                if (Math.Round(Convert.ToDecimal(UserAnswer.Text), 2) == result)
                {
                    MessageBox.Show("Congratulations. " + result + " is a right answer. You have earned 1 point.", "Results");
                    points += POINTS_CHANGE; //points increase by 1 when you give correct answer
                    fileContent = "User's answer: " + UserAnswer.Text + "\nCorrect answer: " + result + "\nUser's answer and correct answer matches" + "\n\n";
                }
                else
                {
                    MessageBox.Show("Oops! That's not correct answer. Correct answer is " + result, "Results");
                    points -= POINTS_CHANGE; //points decrease by 1 when you give wrong answer
                    fileContent = "User's answer: " + UserAnswer.Text + "\nCorrect answer: " + result + "\nUser's answer and correct answer does not match" + "\n\n";
                }
                RandomNumber();
                PointsEarned();
                RightAnswer.Content = string.Empty;
                UserAnswer.Text = string.Empty;
                File.AppendAllText(fullpath, fileContent);
            }
            catch
            {
                MessageBox.Show("Please enter answer in correct format");
            }
        }
        #endregion

        #region Points winner and loser decider
        private void PointsEarned()
        {
            Points.Content = "Points Earned: " + points;
            if (points >= POINTS_WIN)
            {
                Results results = new Results();
                MessageBox.Show("Hurray! You won the game. Click OK to see your results");
                this.Content = results;
            }
            else if (points <= POINTS_LOSE)
            {
                Results results = new Results();
                MessageBox.Show("Oh no! You are short of points. Game is Over! Click OK to see your results");
                this.Content = results;
            }
        }
        #endregion

        #region Skip Click
        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            RandomNumber(); //generates two new random number and new random operator
            RightAnswer.Content = "?";
            UserAnswer.Text = string.Empty;
            points -= POINTS_CHANGE; //on skip, you will lose 1 point
            PointsEarned();
        }
        #endregion
    }
}
