using System;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Web.Http;
using Board.Client.Models;

namespace Board.Client.Controllers
{
    using System.Web;

    public class VirtualMachineInfoController : ApiController
    {
        /// <summary>
        /// Returns information about virtual machine.
        /// </summary>
        /// <returns>
        /// Information about virtual machine.
        /// </returns>
        public VirtualMachineInfo Get()
        {
            var username = GetCurrentUser();
            var machineName = GetCurrentMachineName();
            var ip = GetLocalIpAddress();

            return new VirtualMachineInfo(machineName, username, ip);
        }

        private string GetCurrentMachineName()
        {
            return HttpContext.Current.Server.MachineName;
        }

        private IPAddress GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            return null;
        }

        private string GetCurrentUser()
        {
            return GetProcessOwner("explorer.exe");
        }

        private string GetProcessOwner(string processName)
        {
            string query = "Select * from Win32_Process Where Name = \"" + processName + "\"";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    string owner = argList[1] + "\\" + argList[0];
                    return owner;       
                }
            }

            return "NO OWNER";
        }
    }
}