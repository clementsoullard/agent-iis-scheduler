using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Management;
using System.Diagnostics;
using System.ComponentModel;
using PCStatusApplication;

/**
 * Controller provider the status of the computer
 * */

namespace PCStatusApplication.Controllers
{


    public class StatusWS
    {
        /**
         * The usename of the user currently connected
         * */
        public String username;
        /**
         * If the screen saver is running
         * */
        public Boolean screenSaverRunnning;
        /**
         * If the screen is locked
         * */
        public Boolean screenLocked;
    }


    public class StatusController : ApiController
    {
        // GET api/values
        public StatusWS Get()
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
            StatusWS status = new StatusWS();
            status.username = username;
            status.screenLocked = NativeMethods.IsWorkstationLocked();
            status.screenSaverRunnning = NativeMethods.IsScreensaverRunning();
             return status;
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
