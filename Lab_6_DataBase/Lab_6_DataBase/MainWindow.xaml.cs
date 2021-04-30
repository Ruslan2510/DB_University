using LinqLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Lab_6_DataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Layer layer = new Layer();
        private ObservableCollection<Client> clients = null;
        private ObservableCollection<Broker> brokers = null;
        private ObservableCollection<Order> orders = null;
        private ObservableCollection<Order> ordersAndClients = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                clients = new ObservableCollection<Client>();
                brokers = new ObservableCollection<Broker>();
                orders = new ObservableCollection<Order>();
                ordersAndClients = new ObservableCollection<Order>();

                //Clients
                var clientsList = await layer.GetClientsAsync();
                clientsList.ForEach(x => clients.Add(x));

                ClientsGrid.ItemsSource = clients;

                //Brokers
                var brokersList = await layer.GetBrokersAsync();
                brokersList.ForEach(x => brokers.Add(x));

                BrokersGrid.ItemsSource = brokers;

                //Orders
                var ordersList = await layer.GetOrdersSampleAsync();
                ordersList.ForEach(x => orders.Add(x));

                OrdersGrid.ItemsSource = orders;

                //Join clients to orders
                var joinList = await layer.JoinClientsAndOrdersAsync();
                joinList.ForEach(x => ordersAndClients.Add(x));

                JoinGrid.ItemsSource = ordersAndClients;

                //GroupByStatus
                StatusOrdersCount.Text = await layer.GroupByStatusAsync();

                //Union
                UnionGrid.ItemsSource = await layer.UnionAsync();

                //Intersect
                IntersectGrid.ItemsSource = await layer.IntersectAsync();

                //Except
                ExceptGrid.ItemsSource = await layer.ExceptAsync();

                //Scalar functions
                ScalarFunctionsOutput.Text = await layer.AggregateFunctionsAsync();

                //IEnumerable
                IEnumBlock.Text = layer.GetIEnumOnTimeAsync();

                //EQueryable
                IQueryBlock.Text = layer.GetIQueryOnTimeAsync();

                //AsNoTracking
                AsNoTrackingBlock.Text = layer.GetAsNoTrackingOnTimeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void SearchInput_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                var inputedText = SearchInput.Text;
                if (!string.IsNullOrEmpty(inputedText))
                {
                    var result = layer.GetFilteredClientsAsync(inputedText);
                    ClientsGrid.ItemsSource = await result;
                }
                else
                {
                    ClientsGrid.ItemsSource = clients;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}