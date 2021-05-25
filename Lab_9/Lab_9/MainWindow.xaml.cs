using DAL;
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

namespace Lab_9
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

        private void GetTableButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)TableNameComboBox.SelectedItem;
            var tablename = selectedItem.Content.ToString();


            switch (tablename)
            {
                case "Client":
                    Tables.ItemsSource = layer.GetClients();
                    break;

                case "Broker":
                    Tables.ItemsSource = layer.GetBrokers();
                    break;

                case "Order":
                    Tables.ItemsSource = layer.GetOrders();
                    break;

                case "Bidding":
                    Tables.ItemsSource = layer.GetBidding();
                    break;

                case "Deal":
                    Tables.ItemsSource = layer.GetDeal();
                    break;

                case "Securities":
                    Tables.ItemsSource = layer.GetSecurities();
                    break;

                case "Profit":
                    Tables.ItemsSource = layer.GetProfit();
                    break;

                default:
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RunFirstFunc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string lowLimit = lowLimitPickerFirstGrid.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
                string upperLimit = upperLimitPickerFirstGrid.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");

                var result = layer.GetBrokerageIncome(lowLimit, upperLimit);
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RunSecondFunc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string lowLimit = lowLimitPickerSecondGrid.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
                string upperLimit = upperLimitPickerSecondGrid.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");

                SecondFuncGrid.ItemsSource = layer.GetBurseIncome(lowLimit, upperLimit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RunThirdFunc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var brokerId = Convert.ToInt32(ThirdFuncTb.Text);

                ThirdFuncGrid.ItemsSource = layer.GetExchangeSecurities(brokerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RunFourthFunc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FourthFuncGrid.ItemsSource = layer.GetMostProfitableSecurities();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RunFifthFunc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string date = dateFifthPicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
                FifthFuncGrid.ItemsSource = layer.GetSecuritiesAmountOnBurse(date);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RunStoredProcedure_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int biddingId = Convert.ToInt32(biddingIdTb.Text);
                int securitiesId = Convert.ToInt32(securitiesIdTb.Text);
                int securitiesAmount = Convert.ToInt32(securitiesAmountTb.Text);
                string delaDate = storedDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");

                var selectedItem = (ComboBoxItem)CustomerCb.SelectedItem;
                var customer = selectedItem.Content.ToString();

                selectedItem = (ComboBoxItem)DealTypeCb.SelectedItem;
                var dealType = selectedItem.Content.ToString();

                layer.BuyOrSellPaper(biddingId, securitiesId, securitiesAmount, delaDate, customer, dealType);

                MessageBox.Show("Хранимая процедура выполнена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
