using Layer;
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

namespace Lab_10_DB
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void ConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = loginTB.Text,
                password = passTB.Password;
                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                {
                    Connection сonnection = new Connection();
                    сonnection.CheckConnection(login, password);

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Поля не должны быть пустыми!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
    }
}
