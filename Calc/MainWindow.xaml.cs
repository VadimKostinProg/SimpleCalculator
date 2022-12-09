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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double firstMember;
        private double secondMember;
        bool isOperationClicked;
        bool isOperationChoosed;
        public MainWindow()
        {
            InitializeComponent();

            firstMember = secondMember = 0;
            isOperationClicked = false;
            isOperationChoosed = false;
        }

        private void Add_Number(object sender, RoutedEventArgs e)
        {
            string digit = ((Button)e.OriginalSource).Content.ToString();
            if(this.isOperationClicked)
            {
                this.Main_TextBox.Text = digit;
                this.isOperationClicked = false;
                return;
            }
            if (this.Main_TextBox.Text == "0")
            {
                if(digit == ",")
                {
                    this.Main_TextBox.Text += ((Button)e.OriginalSource).Content.ToString();
                }
                else if(digit != "0")
                {
                    this.Main_TextBox.Text = digit;
                }
            }
            else
                this.Main_TextBox.Text += ((Button)e.OriginalSource).Content.ToString();
        }

        private void Change_MinusPlus(object sender, RoutedEventArgs e)
        {
            if(this.Main_TextBox.Text != "0")
            {
                this.Main_TextBox.Text = (-1 * double.Parse(this.Main_TextBox.Text)).ToString();
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if(this.Main_TextBox.Text.Length == 1)
            {
                this.Main_TextBox.Text = "0";
            }
            else
            {
                this.Main_TextBox.Text = this.Main_TextBox.Text.Substring(0,this.Main_TextBox.Text.Length-1);
            }
        }

        private void Cleare_Click(object sender, RoutedEventArgs e)
        {
            this.Main_TextBox.Text = "0";
            this.Upper_TextBox.Text = String.Empty;
            this.firstMember = this.secondMember = 0;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            this.firstMember = double.Parse(this.Main_TextBox.Text);
            this.Upper_TextBox.Text = firstMember.ToString() + " " + ((Button)e.OriginalSource).Content;
            this.isOperationClicked = true;
            this.isOperationChoosed = true;
        }

        private void DifficultOperation_Click(object sender, RoutedEventArgs e)
        {
            double result = 0;
            switch (((Button)e.OriginalSource).Name)
            {
                case "sqrt":
                    result = Math.Sqrt(double.Parse(this.Main_TextBox.Text));
                    Upper_TextBox.Text = Upper_TextBox.Text == String.Empty ? $"sqrt({Main_TextBox.Text})" : $"sqrt({Upper_TextBox.Text})";
                    Main_TextBox.Text = result.ToString();
                    break;
                case "sqr":
                    result = Math.Pow(double.Parse(this.Main_TextBox.Text),2);
                    Upper_TextBox.Text = Upper_TextBox.Text == String.Empty ? $"sqr({Main_TextBox.Text})" : $"sqr({Upper_TextBox.Text})";
                    Main_TextBox.Text = result.ToString();
                    break;
                case "reverse":
                    result = 1/double.Parse(this.Main_TextBox.Text);
                    Upper_TextBox.Text = Upper_TextBox.Text == String.Empty ? $"1/({Main_TextBox.Text})" : $"1/({Upper_TextBox.Text})";
                    Main_TextBox.Text = result.ToString();
                    break;
                case "procent":
                    result = 0.01 * double.Parse(this.Main_TextBox.Text);
                    Upper_TextBox.Text = Upper_TextBox.Text == String.Empty ? $"0,01*({Main_TextBox.Text})" : $"0,01*({Upper_TextBox.Text})";
                    Main_TextBox.Text = result.ToString();
                    break;
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            this.secondMember = double.Parse(this.Main_TextBox.Text);
            if(!this.isOperationChoosed)
            {
                this.Upper_TextBox.Text = secondMember.ToString() + " =";
                return;
            }
            double result = 0;
            switch(Upper_TextBox.Text[Upper_TextBox.Text.Length-1])
            {
                case '+':
                    result = firstMember + secondMember;
                    break;
                case '-':
                    result = firstMember - secondMember;
                    break;
                case '*':
                    result = firstMember * secondMember;
                    break;
                case '/':
                    result = firstMember / secondMember;
                    break;
                case '^':
                    result = Math.Pow(firstMember, secondMember);
                    break;
            }
            this.Upper_TextBox.Text += " " + secondMember.ToString() + " =";
            this.Main_TextBox.Text = result.ToString();

            HistoryWindow.History.Add($"{this.Upper_TextBox.Text} {this.Main_TextBox.Text}");

            this.firstMember = this.secondMember = 0;
            this.isOperationChoosed = false;
        }
        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
        }
    }
}
