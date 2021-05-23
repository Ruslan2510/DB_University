using Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab_10_DB
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

        SQLiteDAL layer = new SQLiteDAL();
        public ObservableCollection<Client> clients = new ObservableCollection<Client>();
        public Client addClient = new Client();
        public File addFile = new File();

        private  void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var clientsList = layer.GetClients();
                clientsList.ForEach(x => clients.Add(x));

                ClientsGrid.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddClient()
        {
            try
            {
                layer.AddNewClient(addClient, addFile);

                clients.Add(addClient);
                MessageBox.Show("Клиент добавлен успешно!");
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddWindow addWindow = new AddWindow();
                addWindow.Owner = this;
                addWindow.Show();
                addWindow.Owner.IsHitTestVisible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DropButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = ClientsGrid.SelectedItem as Client;

                if (selectedRow != null)
                {
                    clients.Remove(selectedRow);
                    layer.DeleteClientById(selectedRow.Id);

                    MessageBox.Show("Строка удалена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateClient(Client updatedClient)
        {
            try
            {
                layer.UpdateClient(updatedClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var renewedObject = e.Row.Item as Client;
            UpdateClient(renewedObject);
        }
    }
}
