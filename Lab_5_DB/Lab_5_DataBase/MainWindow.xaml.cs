using Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace Lab_5_DataBase
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

        LinqLayer linqLayer = null;

        public Client enteredClient { get; set; } = new Client();

        ObservableCollection<Client> clients = null;
        private void DropButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = FirstDataGrid.SelectedItem as Client;

                if (selectedRow != null)
                {
                    linqLayer = new LinqLayer();
                    clients.Remove(selectedRow);
                    linqLayer.DeleteClientById(selectedRow.ClientId);

                    MessageBox.Show("Строка удалена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                linqLayer = new LinqLayer();
                var table = linqLayer.GetClientTable();
                //FirstDataGrid.ItemsSource = table.DefaultView;

                clients = new ObservableCollection<Client>();

                foreach (DataRow row in table.Rows)
                {
                    clients.Add(new Client()
                    {
                        ClientId = (int)row["ClientId"],
                        ClientSurname = (string)row["ClientSurname"],
                        ClientName = (string)row["ClientName"],
                        ClientPatronymic = (string)row["ClientPatronymic"],
                        ClientEmail = (string)row["ClientEmail"],
                        ClientPhone = (string)row["ClientPhone"],
                        ClientFortune = (decimal)row["ClientFortune"]
                    });
                }
                FirstDataGrid.ItemsSource = clients;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //TODO: сделать изменение данных в форме, добавление (вернуть данные из дочерней формы AddWindow)
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Owner = this;
            addWindow.Show();
            addWindow.Owner.IsHitTestVisible = false;
        }

        public void AddUser()
        {
            try
            {
                var list = FirstDataGrid.ItemsSource;
                linqLayer = new LinqLayer();
                var newuserId = linqLayer.AddClient(enteredClient.ClientSurname, enteredClient.ClientName, enteredClient.ClientPatronymic,
                                                enteredClient.ClientEmail, enteredClient.ClientPhone, enteredClient.ClientFortune);

                enteredClient.ClientId = newuserId;
                clients.Add(enteredClient);
                FirstDataGrid.ItemsSource = clients;

                MessageBox.Show("Клиент добавлен");
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
                linqLayer = new LinqLayer();
                linqLayer.UpdateClient(updatedClient);
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

        private void ProcedureStartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string upperLimit = SecondTabDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
                int papersAmount = Convert.ToInt32(PapersAmount.Text);
                int paperId = Convert.ToInt32(paperTB.Text);
                int biddingId = Convert.ToInt32(biddingTB.Text);
                string customer = ComboBoxCustomer.Text;
                string deal = ComboBoxDealType.Text;


                linqLayer = new LinqLayer();
                var table = linqLayer.StoredProcedure(upperLimit, papersAmount, paperId, biddingId, customer, deal);


                foreach (DataRow row in table.Rows)
                {
                    clients.Add(new Client()
                    {
                        ClientId = (int)row["ClientId"],
                        ClientSurname = (string)row["ClientSurname"],
                        ClientName = (string)row["ClientName"],
                        ClientPatronymic = (string)row["ClientPatronymic"],
                        ClientEmail = (string)row["ClientEmail"],
                        ClientPhone = (string)row["ClientPhone"],
                        ClientFortune = (decimal)row["ClientFortune"]
                    });
                }
                FirstDataGrid.ItemsSource = clients;

                MessageBox.Show("Хранимая процедура выполнена успешно!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}