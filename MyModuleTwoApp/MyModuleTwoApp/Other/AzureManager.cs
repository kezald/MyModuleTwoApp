using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.WindowsAzure.MobileServices;

namespace MyModuleTwoApp
{
    public class AzureManager
    {
        private static AzureManager instance;

        private MobileServiceClient client;
        private IMobileServiceTable<TableModel> table;

        private AzureManager()
        {
            this.client = new MobileServiceClient("https://moduletwoapp.azurewebsites.net");
            this.table = this.client.GetTable<TableModel>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public IMobileServiceTable<TableModel> TableModelTable
        {
            get { return table; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<TableModel>> GetTableInformation()
        {
            return await this.table.ToListAsync();
        }

        public async Task PostTableInfo(TableModel tableModel)
        {
            await this.table.InsertAsync(tableModel);
        }
    }
}
