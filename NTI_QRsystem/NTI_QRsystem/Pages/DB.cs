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
        private const string L = "http://www.sc.somee.com/api/";
        private const string AC = "Accounts";
        private const string IN = "Info";
        private const string LE = "Lec";
        public static List<Account> accounts = new List<Account>();
        public static List<Lecture> lectures = new List<Lecture>();
        public static List<Info> infos = new List<Info>();



        public static async Task RegisterInfo(Account student, Lecture lecture)
        {
            Info info = new Info { Studentname=student.Username,LecId=lecture.Rid,
             };
            infos.Add(info);
            await AddInfo(info);
        }

        public static async Task LoadAccounts()
        {
            accounts.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync(L+AC);
            accounts = JsonConvert.DeserializeObject<List<Account>>(responce);
        }

        public static async Task LoadLectures()
        {
            lectures.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync(L+LE);
            lectures = JsonConvert.DeserializeObject<List<Lecture>>(responce);
        }

        public static async Task LoadInfos()
        {
            infos.Clear();
            HttpClient client = new HttpClient();
            var responce = await client.GetStringAsync(L+IN);
            infos = JsonConvert.DeserializeObject<List<Info>>(responce);
        }

        public static async Task<HttpResponseMessage> AddLecture(Lecture lecture)
        {
            var json = JsonConvert.SerializeObject(lecture);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(L+LE, c);
        }

        public static async Task<HttpResponseMessage> RemoveAccount(Account account)
        {
            HttpClient client = new HttpClient();
            return await client.DeleteAsync(L + AC +"/" + account.ID);
        }

        public static async Task<HttpResponseMessage> RemoveLecture(Lecture lecture)
        {
            HttpClient client = new HttpClient();
            return await client.DeleteAsync(L + LE + "/" + lecture.Rid);
        }

        public static async Task<HttpResponseMessage> RemoveInfo(Info info)
        {
            if (infos.Contains(info))
            {
                infos.Remove(info);
            }
            HttpClient client = new HttpClient();
            return await client.DeleteAsync(L + IN + "/" + info.ID);
        }

        public static async Task<HttpResponseMessage> FullyRemoveLecture(Lecture lecture)
        {
            var b = GetLectureById(lecture.Rid);
            for (int x = 0; x < infos.Count; x++)
            {
                if(infos[x].LecId == b.Rid)
                {
                    await RemoveInfo(infos[x]);
                }
            }
            if (b != null)
            {
                lectures.Remove(b);
                return await RemoveLecture(b);
            }
            return null;
        }

        public static async Task<HttpResponseMessage> EditLec(Lecture lec)
        {
            var json = JsonConvert.SerializeObject(lec);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var responce = await client.PutAsync(L + LE + "/" + lec.Rid, content);
            return responce;
        }

        public static Account CheckMobileID(string id)
        {
            for(int x = 0; x < accounts.Count; x++)
            {
                if(accounts[x].MobileID != null && accounts[x].MobileID == id)
                {
                    return accounts[x];
                }
            }
            return null;
        }

        public static async Task<HttpResponseMessage> EditAccount(Account account)
        {
            var json = JsonConvert.SerializeObject(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var responce = await client.PutAsync(L + AC + "/" + account.ID, content);
            return responce;
        }

        public static async Task<HttpResponseMessage> AddAccount(Account account)
        {
            var json = JsonConvert.SerializeObject(account);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(L + AC, c);
        }

        public static async Task<HttpResponseMessage> AddInfo(Info info)
        {
            var json = JsonConvert.SerializeObject(info);
            var c = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            return await client.PostAsync(L + IN, c);
        }

        public static async Task<HttpResponseMessage> FullyAddInfo(Info info)
        {
            infos.Add(info);
            return await AddInfo(info);
        }

        public static async Task<HttpResponseMessage> FullyAddLecture(Lecture lec)
        {
            lectures.Add(lec);
            return await AddLecture(lec);
        }

        public static Lecture CheckLecture(string room)
        {
            for(int x = 0; x < lectures.Count; x++)
            {
                if(lectures[x].DeviceID == room)
                {
                    return lectures[x];
                }
            }
            return null;
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

        public static Lecture GetLectureByTeacher(Account teacher)
        {
            for(int x = 0; x < lectures.Count; x++)
            {
                if (lectures[x].AdminID == teacher.Username)
                {
                    return lectures[x];
                }
            }
            return null;
        }

        public static Lecture GetLectureById(string rid) {
            for(int x = 0; x < lectures.Count; x++)
            {
                if(lectures[x].Rid == rid)
                {
                    return lectures[x];
                }
            }
            return null;
        }

        public static Account GetDevice(string room)
        {
            for(int x = 0; x < accounts.Count; x++)
            {
                Account a = accounts[x];
                if (a.isAdmin && a.Class == "Device" && a.Username == room)
                {
                    return a;
                }
            }
            return null;
        }

        public static bool IsDevice(Account account)
        {
            return account.isAdmin && account.Class == "Device";
        }

        public static Lecture GetLecByDevice(Account device)
        {
            for(int x = 0; x < lectures.Count; x++)
            {
                Lecture lec = lectures[x];
                if(lec.DeviceID != null && lec.DeviceID == device.Username)
                {
                    return lec;
                }
            }
            return null;
        }

        public static bool CheckStudent(Account account)
        {
            for(int x = 0; x < infos.Count; x++)
            {
                if(infos[x].Studentname == account.Username)
                {
                    return true;
                }
            }
            return false;
        }

        public static Info CheckStudent(Account account, Lecture lec)
        {
            for (int i = 0; i < infos.Count; i++)
            {
                if(infos[i].LecId == lec.Rid && infos[i].Studentname == account.Username)
                {
                    return infos[i];
                }
            }
            return null;
        }

    }
}
