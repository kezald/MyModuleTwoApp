
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyModuleTwoApp
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTable : ContentPage
    {

        ObservableCollection<TableModel> listTableElements = new ObservableCollection<TableModel>();
        //List<TableModel> tableInfo;

        public PageTable()
        {
            InitializeComponent();

            TableList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null) return;
                ((ListView)sender).SelectedItem = null;
            };

            populateList();
        }

        public async void populateList()
        {
            List<TableModel> tableInfo = await AzureManager.AzureManagerInstance.GetTableInformation();
            listTableElements = new ObservableCollection<TableModel>(tableInfo);
            TableList.ItemsSource = listTableElements;
        }

        private void buttonAddNew_Clicked(object sender, EventArgs e)
        {
            TableModel row = new TableModel()
            {
                Description = entryDescription.Text 
            };
            AzureManager.AzureManagerInstance.PostTableInfo(row);
            listTableElements.Add(row);
        }

        private async void buttonDelete_Clicked(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            var response = await DisplayAlert("Confirm action", "Delete Row?", "Yes", "No");
            if (response)
            {
                TableModel row = (TableModel)bt.BindingContext;

                await AzureManager.AzureManagerInstance.TableModelTable.DeleteAsync(row);
                listTableElements.Remove(row);
            }
        }

        private void buttonrefresh_Clicked(object sender, EventArgs e)
        {
            populateList();
        }
    }
}
