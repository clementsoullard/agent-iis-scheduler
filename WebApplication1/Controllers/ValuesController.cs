using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Management;
using System.Diagnostics;
using System.ComponentModel;

namespace WebApplication1.Controllers
{
    public class StatusController : ApiController
    {
        // GET api/values
        public string Get()
        {
            string username = "";
            try
            {
                Debug.WriteLine("Passage dans la methode value");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
                ManagementObjectCollection collection = searcher.Get();

                for (int i = 0; i < collection.Count; i++)
                {
                    string valueToAdd = (string)collection.Cast<ManagementBaseObject>().ElementAt(i)["UserName"];
                    username += valueToAdd;
                    Debug.WriteLine("");
                    EventLog.WriteEntry("Coucou1", "coucou1");
                }
                //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }

            catch (Exception e)
            {
                EventLog.WriteEntry("Coucou", "coucou");
            }

            return username;
        }

        // GET api/values/5
        public string Get(int id)
        {
            string username = "";
            try
            { 
            Debug.WriteLine("Passage dans la methode value");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            
            for (int i=0;i< collection.Count; i++) { 
             string valueToAdd= (string)collection.Cast<ManagementBaseObject>().ElementAt(i)["UserName"];
               username += valueToAdd;
              Debug.WriteLine("");
                    EventLog.WriteEntry("Coucou1", "coucou1");
                }
                //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }

            catch(Exception e)
            {
                EventLog.WriteEntry("Coucou", "coucou");
            }

            return username;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
