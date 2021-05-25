using DAL;
using System;
using System.Windows;

namespace Lab_9
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
