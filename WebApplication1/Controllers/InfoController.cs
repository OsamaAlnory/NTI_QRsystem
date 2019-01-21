using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class InfoController : ApiController
    {
        //Get Info
        public IEnumerable<Info> Get()
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                return entities.Info.ToList();
            }
        }

        //Get info by id
        public Info Get(int id)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                return entities.Info.FirstOrDefault(e => e.ID == id);
            }
        }

        //Post Info
          public void Post([FromBody]Info info)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                entities.Info.Add(info);
                entities.SaveChanges();
            }
        }

        //Delete Info
        public void Delete(int id)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                entities.Info.Remove(entities.Info.FirstOrDefault(e => e.ID == id));
                entities.SaveChanges();
            }
        }


        //Update Info (Put)
        public void Put(int id, [FromBody] Info info)
        {
            using (MSSQL400028Entities1 entities = new MSSQL400028Entities1())
            {
                var entity = entities.Info.FirstOrDefault(e => e.ID == id);

                entity.Studentname = info.Studentname;
                entity.ATime = info.ATime;
                entity.Date = info.Date;
                entities.SaveChanges();
            }
        }


    }
}
