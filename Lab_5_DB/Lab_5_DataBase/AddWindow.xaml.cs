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

namespace Lab_5_DataBase
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

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.IsHitTestVisible = true;
            MainWindow mainWindow = (MainWindow)this.Owner;


            mainWindow.enteredClient.ClientSurname = SurnameTb.Text;
            mainWindow.enteredClient.ClientName = NameTb.Text;
            mainWindow.enteredClient.ClientPatronymic = PatronymicTb.Text;
            mainWindow.enteredClient.ClientEmail = EmailTb.Text;
            mainWindow.enteredClient.ClientPhone = PhoneTb.Text;
            mainWindow.enteredClient.ClientFortune = Convert.ToDecimal(BalanceTb.Text);

            mainWindow.AddUser();
            this.Close();
        }
    }
}
