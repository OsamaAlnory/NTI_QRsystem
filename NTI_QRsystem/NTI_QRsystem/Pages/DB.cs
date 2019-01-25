using Newtonsoft.Json;
using NTI_QRsystem.DBK;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NTI_QRsystem.Pages
{
    public class DB
    {
        public static List<Account> accounts = new List<Account>();
        public static List<Lecture> lectures = new List<Lecture>();

        public static async Task LoadAccounts()
        {
            accounts.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync("http://qr.somee.com/api/Accounts");
            accounts = JsonConvert.DeserializeObject<List<Account>>(responce);
        }

        public static async Task LoadLectures()
        {
            lectures.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync("http://qr.somee.com/api/Lec");
            lectures = JsonConvert.DeserializeObject<List<Lecture>>(responce);
        }

        public static Account getAccountByName(string name)
        {
            for(int x = 0; x < accounts.Count; x++)
            {
                if(accounts[x].Username == name)
                {
                    return accounts[x];
                }
            }
            return null;
        }

    }
}
