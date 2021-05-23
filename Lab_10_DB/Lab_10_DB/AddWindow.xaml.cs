using Layer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        MainWindow mainWindow = null;

        public AddWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Owner.IsHitTestVisible = true;
                mainWindow = (MainWindow)this.Owner;

                mainWindow.addClient.Name = NameTb.Text;
                mainWindow.addClient.Email = EmailTb.Text;
                mainWindow.addClient.Phone = PhoneTb.Text;
                mainWindow.addClient.Balance = Convert.ToDecimal(BalanceTb.Text);

                this.Close();
                mainWindow.AddClient();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    mainWindow = (MainWindow)this.Owner;

                    mainWindow.addFile.Title = openFileDialog.Title;

                    var fileName = openFileDialog.FileName;

                    mainWindow.addClient.ImageUrl = fileName;
                    mainWindow.addFile.FileName = fileName;
                    mainWindow.addFile.ImageData = GetImageData(fileName);

                    AddWindowImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private byte[] GetImageData(string fileName)
        {
            string shortFileName = fileName.Substring(fileName.LastIndexOf('\\') + 1); // forest.jpg

            // массив для хранения бинарных данных файла
            byte[] imageData;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, imageData.Length);
            }

            return imageData;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsHitTestVisible = true;
            this.Close();
        }
    }
}
