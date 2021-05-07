using Layer;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab_8_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBLayer layer = new DBLayer();
        private ObservableCollection<Client> clients = null;
        private ObservableCollection<Broker> brokers = null;
        private ObservableCollection<Order> orders = null;

        public Order newOrder;
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

                //Clients
                var clientsList = await layer.GetClientsAsync();
                clientsList.ForEach(x => clients.Add(x));

                ClientsGrid.ItemsSource = clients;

                //Brokers
                var brokersList = await layer.GetBrokersAsync();
                brokersList.ForEach(x => brokers.Add(x));

                BrokersGrid.ItemsSource = brokers;

                //Orders
                var ordersList = await layer.GetOrdersAsync();
                ordersList.ForEach(x => orders.Add(x));
                OrdersGrid.ItemsSource = orders;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var renewedObject = e.Row.Item as Order;
            UpdateOrder(renewedObject);
        }

        public async void AddOrderAsync()
        {
            try
            {
                if (newOrder != null)
                {
                    await layer.AddOrderAsync(newOrder);
                }

                MessageBox.Show("Заказ добавлен");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void UpdateOrder(Order order)
        {
            try
            {
                await layer.UpdateOrderAsync(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = OrdersGrid.SelectedItem as Order;

                if (selectedRow != null)
                {
                    orders.Remove(selectedRow);
                    await layer.DeleteOrderByIdAsync(selectedRow);

                    MessageBox.Show("Строка удалена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Owner = this;
            addWindow.Show();
            addWindow.Owner.IsHitTestVisible = false;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await layer.GetClientByIdAsync(id);
        }
        public async Task<Broker> GetBrokerByIdAsync(int id)
        {
            return await layer.GetBrokerByIdAsync(id);
        }

        public void AddOrder()
        {
            orders.Add(newOrder);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
