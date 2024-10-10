using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class MailData
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string EmailFrom { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

}
