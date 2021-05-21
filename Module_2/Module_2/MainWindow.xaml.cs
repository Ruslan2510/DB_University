using Layer;
using Layer.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Module_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private LINQLayer layer = new LINQLayer();
        private ObservableCollection<Material> materials = null;
        private ObservableCollection<StorageUnit> storageUnits = null;
        private ObservableCollection<Provider> providers = null;
        private ObservableCollection<MeasureInfo> measureInfo = null;


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                materials = new ObservableCollection<Material>();
                storageUnits = new ObservableCollection<StorageUnit>();
                providers = new ObservableCollection<Provider>();
                measureInfo = new ObservableCollection<MeasureInfo>();

                //Materials
                var materialsList = await layer.GetMaterialsAsync();
                materialsList.ForEach(x => materials.Add(x));

                MaterialsGrid.ItemsSource = materials;

                //StorageUnit
                var storageUnitsList = await layer.GetStorageUnitsIncludeAsync();
                storageUnitsList.ForEach(x => storageUnits.Add(x));

                StorageUnitsGrid.ItemsSource = storageUnits;

                //Providers
                var providersList = await layer.GetProvidersAsync();
                providersList.ForEach(x => providers.Add(x));

                ProviderGrid.ItemsSource = providers;

                //MeasureInfo
                var measureInfoList = await layer.GetMeasureInfoAsync();
                measureInfoList.ForEach(x => measureInfo.Add(x));

                MeasureInfoGrid.ItemsSource = measureInfo;
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
                    var result = layer.GetFilteredProvidersAsync(inputedText);
                    ProviderGrid.ItemsSource = await result;
                }
                else
                {
                    ProviderGrid.ItemsSource = providers;
                }
            }
            catch (Exception ex)
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
