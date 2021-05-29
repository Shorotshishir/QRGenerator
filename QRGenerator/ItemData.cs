using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator
{
    public class ItemData
    {
        public string Id { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;

        public ItemData()
        {
        }

        public ItemData(string id, string ip)
        {
            Id = id;
            Ip = ip;
        }
    }
}