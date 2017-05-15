using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Management;
using System.DirectoryServices.AccountManagement;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;



namespace PCStatusApplication.Controllers
{
  
    /**
     * Controller managing the status of user connection 
     * 
     **/
    public class UserController : ApiController
    {
        [DllImport("wtsapi32.dll", SetLastError = true)]

        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);
        const int WTS_CURRENT_SESSION = -1;
        static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

       /**
        * This disconnects the current user
        * */
        [Route("api/User/disconnect")]
        public Boolean GetDisconnect()
        {
          return  WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE,
        WTS_CURRENT_SESSION, true);
        }

        [Route("api/User/{username}/{enable}")]
        public string GetUserEnable(string username, Boolean enable)
        {

            Debug.WriteLine("Appel de la méthode " );

            try
            {
                // set up domain context
                PrincipalContext ctx = new PrincipalContext(ContextType.Machine);

                // find a user
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx, username);

                if (user != null)
                {
                    user.Enabled = enable;
                    user.Save();
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception exceptionnelle "+e);
               
            }
            return "username";
        }

        // GET api/values/5
        public StatusWS Get(int id)
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

            StatusWS status = new StatusWS();
            status.username = username;
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
