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
        private static bool _isInProgress;
        private int _callDuration;

        public DateTime timeOfCall { get => _timeOfCall; set => _timeOfCall = value; }
        public CallStatus status { get => _status; set => _status = value; }
        public bool isInProgress { get => _isInProgress; set => _isInProgress = value; }

        public Call(int callDuration)
        {
            _timeOfCall = DateTime.Now;
            _callDuration = callDuration;
        }
        public enum CallStatus
        {
            InProgress,
            Missed,
            Ended
        };
    }
}
