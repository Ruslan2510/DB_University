using ConnectedLayer;
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

namespace HW_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Layer layer = new Layer();

        private void FirstTaskRunButton_Click(object sender, RoutedEventArgs e)
        {
            int biddingId = int.Parse(biddingID.Text),
                paperId = int.Parse(paperID.Text),
                paperAmount = int.Parse(paperNumber.Text);

            string deal = ComboBoxDealType.Text;

            char customer = ' ';

            if (ComboBoxCustomer.Text == "Человек")
            {
                customer = 'Ч';
            }
            else if (ComboBoxCustomer.Text == "Биржа")
            {
                customer = 'Б';
            }

            try
            {
                layer.SellOrBuyPaper(biddingId, paperId, paperAmount, deal, customer);
                MessageBox.Show("Операция выполнена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowTableButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BottomDataGrid.ItemsSource = layer.ShowTable(string.Format($"{TableName.Text}")).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RunFunc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string lowLimit = lowLimitPickerGrid.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
                string upperLimit = upperLimitPickerGrid.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");

                FunctionResultGrid.ItemsSource = layer.UserFunction(lowLimit, upperLimit).DefaultView;

                MessageBox.Show("Функция выполена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
