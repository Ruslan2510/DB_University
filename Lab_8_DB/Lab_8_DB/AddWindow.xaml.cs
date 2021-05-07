using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab_8_DB
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.IsHitTestVisible = true;
            MainWindow mainWindow = (MainWindow)this.Owner;
            mainWindow.newOrder = new Layer.Order();


            mainWindow.newOrder.Comment = OrderCommentTb.Text;
            mainWindow.newOrder.Status = OrderStatusCB.Text;

            int clientId = Convert.ToInt32(ClientIdTb.Text);
            int brokerId = Convert.ToInt32(BrokerIdTb.Text);

            mainWindow.newOrder.Client = await mainWindow.GetClientByIdAsync(clientId);
            mainWindow.newOrder.Broker = await mainWindow.GetBrokerByIdAsync(brokerId);

            mainWindow.AddOrderAsync();
            mainWindow.AddOrder();
            this.Close();
        }

        private static readonly Regex onlyNumbers = new Regex("[^0-9.-]+"); //regex that matches disallowed text

        private static bool IsTextAllowed(string text)
        {
            return !onlyNumbers.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
