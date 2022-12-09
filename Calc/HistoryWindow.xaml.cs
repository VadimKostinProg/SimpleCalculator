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
using System.Windows.Shapes;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public static List<string> History = new List<string>();
        public HistoryWindow()
        {
            InitializeComponent();
            if (History.Count == 0) this.History_ListView.Items.Add("History is empty");
            else this.History_ListView.ItemsSource = History;
        }
    }
}
