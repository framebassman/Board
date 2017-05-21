using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Board.Client.Models
{
    public class VirtualMachineInfo
    {
        public bool IsBusy
        {
            get
            {
                if (string.IsNullOrEmpty(UsedBy))
                    return false;
                return true;
            }
        }

        public string UsedBy;
        public string IpAddressString => _ipAddress.ToString();
        public string Name;
        private readonly IPAddress _ipAddress;

        public VirtualMachineInfo(string name, string usedBy, IPAddress ipAddress)
        {
            Name = name;
            UsedBy = usedBy;
            _ipAddress = ipAddress;
        }
    }
}