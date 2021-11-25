using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Entities
{
    class Call
    {

        public DateTime timeOfCall { get; set; }
        public static bool isInProgress = false;
        public enum callStatus
        {
            InProgress,
            Missed,
            Ended
        };
    }
}
