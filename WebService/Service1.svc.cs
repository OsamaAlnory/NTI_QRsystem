using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }



        public List<accounts> GetAccounts()
        {
            try
            {
                MSSQL400028DataContext dc = new MSSQL400028DataContext();
                List<accounts> results = new List<accounts>();
                foreach (Account cust in dc.Accounts)
                {
                    results.Add(new accounts()
                    {
                        //Database colummen /proerties 
                        ID = cust.ID,
                        Username = cust.Username,
                        Password = cust.Password,
                        Class = cust.Class,
                        isAdmin = cust.isAdmin
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }


        public List<info> Getinfo()
        {
            try
            {
                MSSQL400028DataContext dc = new MSSQL400028DataContext();
                List<info> results = new List<info>();
                foreach (Info cust in dc.Infos)
                {
                    results.Add(new info()
                    {
                        //Database colummen /proerties 
                        ID = cust.ID,
                        Studentname = cust.Studentname,
                        Atime = cust.ATime,
                        Date = cust.Date
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }
    }
}
