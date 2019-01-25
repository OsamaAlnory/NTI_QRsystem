using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NTI_QRsystem.DBK
{
    public class DB
    {
        public static List<Account> accounts = new List<Account>();

        public static async Task LoadAccounts()
        {
            accounts.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync("http://qr.somee.com/api/Accounts");
            accounts = JsonConvert.DeserializeObject<List<Account>>(responce);
        }
    }
}
