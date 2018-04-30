using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE_Entity
{
    public class Ent_User
    {
        public Guid USE_GUID_ID { get; set; }
        public Guid? PER_GUID_ID { get; set; }
        public Guid? STD_GUID_ID { get; set; }
        public string USE_LOGIN { get; set; }
        public string USE_PASS { get; set; }
        public bool USE_STATUS { get; set; }
    }
}