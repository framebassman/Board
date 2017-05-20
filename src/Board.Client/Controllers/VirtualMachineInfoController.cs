using System;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Web.Http;
using Board.Client.Models;

namespace Board.Client.Controllers
{
    public class VirtualMachineInfoController : ApiController
    {
        public VirtualMachineInfo Get()
        {
            string username = GetCurrentUser();
            var ip = GetLocalIpAddress();

            return new VirtualMachineInfo(username, ip);
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