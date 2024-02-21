using System;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR1
{
    public partial class MainWindow : Window
    {
        private string vivod;
        public int I, M, G;
        public Button[] buttons = new Button[9];
        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[] { Buton1, Buton2, Buton3, Buton4, Buton5, Buton6, Buton7, Buton8, Buton9 };
            Block();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = "x";

            (sender as Button).IsEnabled = false;

            SloznayaProverka();
            Proverochka();

            if (CheckIfEverythingIsBlocked() != 9)
            {
                Random();
                ProverkaNolik();
                Proverochka();
            }
        }
        private void Start_Igra(object sender, RoutedEventArgs e)
        {
            I++;
            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
                button.Content = "";
            }
            Win.Text = "";
            M = 0;
            G = 0;


        }
        public void SloznayaProverka()
        {
            string[,] board = new string[3, 3];


            board[0, 0] = Buton1.Content.ToString();
            board[0, 1] = Buton2.Content.ToString();
            board[0, 2] = Buton3.Content.ToString();
            board[1, 0] = Buton4.Content.ToString();
            board[1, 1] = Buton5.Content.ToString();
            board[1, 2] = Buton6.Content.ToString();
            board[2, 0] = Buton7.Content.ToString();
            board[2, 1] = Buton8.Content.ToString();
            board[2, 2] = Buton9.Content.ToString();

            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == "x" && board[i, 1] == "x" && board[i, 2] == "x" ||
                    board[0, i] == "x" && board[1, i] == "x" && board[2, i] == "x")
                {
                    Win.Text = "Выйграл крестик";
                    Block();
                    return;
                }
            }

            if (board[0, 0] == "x" && board[1, 1] == "x" && board[2, 2] == "x" ||
                board[0, 2] == "x" && board[1, 1] == "x" && board[2, 0] == "x")
            {
                Win.Text = "Выйграл крестик";
                Block();
                return;
            }
        }

        public void ProverkaNolik()
        {
            if ((Buton1.Content == "o" && Buton2.Content == "o" && Buton3.Content == "o") ||
                (Buton4.Content == "o" && Buton7.Content == "o" && Buton1.Content == "o") ||
                (Buton5.Content == "o" && Buton2.Content == "o" && Buton8.Content == "o") ||
                (Buton5.Content == "o" && Buton4.Content == "o" && Buton6.Content == "o") ||
                (Buton5.Content == "o" && Buton1.Content == "o" && Buton9.Content == "o") ||
                (Buton5.Content == "o" && Buton3.Content == "o" && Buton7.Content == "o") ||
                (Buton9.Content == "o" && Buton6.Content == "o" && Buton3.Content == "o") ||
                (Buton9.Content == "o" && Buton8.Content == "o" && Buton7.Content == "o"))
            {
                Win.Text = "Выйграл нолик";
                Block();
            }
        }

        private int CheckIfEverythingIsBlocked()
        {
            int G = 0;
            foreach (Button button in buttons)
            {
                if (!button.IsEnabled)
                {
                    G++;
                }
            }
            return G;
        }
        public void Proverochka()
        {
            if (CheckIfEverythingIsBlocked() == 9 && Win.Text != "Выйграл нолик" && Win.Text != "Выйграл крестик")
            {
                Win.Text = "Ничья";
                Block();
            }
        }

        public void Random()
        {
            Random random = new Random();
            while (true)
            {
                int value = random.Next(0, 9);
                if (buttons[value].IsEnabled == true)
                {
                    buttons[value].Content = "o";
                    buttons[value].IsEnabled = false;
                    M++;
                    return;
                }
                else if (M == 4)
                {
                    break;
                }
            }
        }
        public void Block()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }
    }
}
