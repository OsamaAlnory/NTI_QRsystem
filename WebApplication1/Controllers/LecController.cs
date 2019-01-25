using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class LecController : ApiController
    {
        //Get Accounts
        public IEnumerable<Lec> Get()
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                return entities.Lec.ToList();
            }
        }


        //Get Accounts by Id
        public Lec Get(string id)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                return entities.Lec.FirstOrDefault(e => e.Rid == id);
            }
        }


        //Post Accounts

        public void Post([FromBody]Lec lec)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                entities.Lec.Add(lec);
                entities.SaveChanges();
            }
        }


        //Delete Accounts
        public void Delete(string id)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                entities.Lec.Remove(entities.Lec.FirstOrDefault(e => e.Rid == id));
                entities.SaveChanges();
            }
        }


        //Update Accounts (Put)
        public void Put(string id, [FromBody] Lec lec)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                var entity = entities.Lec.FirstOrDefault(e => e.Rid == id);

                entity.Rid = lec.Rid;
                entity.AdminID = lec.AdminID;
                entity.Class = lec.Class;
                entity.LecTime = lec.LecTime;
                entity.DeviceID = lec.DeviceID;
                entity.Extra = lec.Extra;
                entities.SaveChanges();
            }
        }



    }
}
