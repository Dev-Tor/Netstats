using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public class RequestFailedException: Exception
    {
        public RequestFailedException(string message):base(message)
        {

        }
        public RequestFailedException(string message, Exception exception) : base(message, exception)
        {
            
        }

        public PageType Recieved { get; set; }

        public PageType Expected { get; set; }
    }
}
