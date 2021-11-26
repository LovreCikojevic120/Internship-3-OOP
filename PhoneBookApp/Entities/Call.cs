using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Entities
{
    class Call
    {
        private DateTime _timeOfCall;
        private CallStatus _status;

        public DateTime timeOfCall { get => _timeOfCall; set => _timeOfCall = value; }
        public CallStatus status { get => _status; set => _status = value; }

        public static bool isInProgress = false;
        public enum CallStatus
        {
            InProgress,
            Missed,
            Ended
        };
    }
}
