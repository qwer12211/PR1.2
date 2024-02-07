using System;
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


namespace s
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn;
        private bool isGameOver;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            isPlayerXTurn = true;
            isGameOver = false;

            foreach (var button in grid.Children)
            {
                if (button is Button gameButton)
                {
                    gameButton.Content = string.Empty;
                    gameButton.IsEnabled = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isGameOver)
                return;

            var button = (Button)sender;

            if (button.Content != null && button.Content.ToString() != string.Empty)
                return;

            button.Content = isPlayerXTurn ? "X" : "O";
            button.IsEnabled = false;

            if (CheckForWinner())
            {
                isGameOver = true;
                MessageBox.Show($"Игрок {(isPlayerXTurn ? "X" : "O")} победил!");
            }
            else if (CheckForDraw())
            {
                isGameOver = true;
                MessageBox.Show("Ничья!");
            }
            else
            {
                isPlayerXTurn = !isPlayerXTurn;
                if (!isPlayerXTurn)
                    MakeRobotMove();
            }
        }

        private bool CheckForWinner()
        {
            // Проверка всех возможных комбинаций для победы
            string[,] board = new string[3, 3]
            {
                { GetButtonContent(button1), GetButtonContent(button2), GetButtonContent(button3) },
                { GetButtonContent(button4), GetButtonContent(button5), GetButtonContent(button6) },
                { GetButtonContent(button7), GetButtonContent(button8), GetButtonContent(button9) }
            };

            // Проверка горизонтальных линий
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != null && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
            }

            // Проверка вертикальных линий
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] != null && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }

            // Проверка диагональных линий
            if (board[0, 0] != null && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;

            if (board[2, 0] != null && board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
                return true;

            return false;
        }

        private bool CheckForDraw()
        {
            foreach (var button in grid.Children)
            {
                if (button is Button gameButton && gameButton.Content == null || gameButton.Content.ToString() == string.Empty)
                    return false;
            }

            return true;
        }

        private string GetButtonContent(Button button)
        {
            if (button.Content != null)
                return button.Content.ToString();

            return null;
        }

        private void MakeRobotMove()
        {
            // Ваша логика хода робота
            // Можно использовать случайный выбор кнопки или другие алгоритмы
            // В данном примере робот выбирает случайную доступную кнопку

            Random random = new Random();
            Button[] availableButtons = GetAvailableButtons();

            if (availableButtons.Length > 0)
            {
                int index = random.Next(0, availableButtons.Length);
                availableButtons[index].Content = "O";
                availableButtons[index].IsEnabled = false;

                if (CheckForWinner())
                {
                    isGameOver = true;
                    MessageBox.Show("Робот победил!");
                }
                else if (CheckForDraw())
                {
                    isGameOver = true;
                    MessageBox.Show("Ничья!");
                }
                else
                {
                    isPlayerXTurn = true;
                }
            }
        }

        private Button[] GetAvailableButtons()
        {
            var buttons = new Button[]
            {
                button1, button2, button3,
                button4, button5, button6,
                button7, button8, button9
            };

            var availableButtons = new System.Collections.Generic.List<Button>();

            foreach (var button in buttons)
            {
                if (button.Content == null || button.Content.ToString() == string.Empty)
                    availableButtons.Add(button);
            }

            return availableButtons.ToArray();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}
