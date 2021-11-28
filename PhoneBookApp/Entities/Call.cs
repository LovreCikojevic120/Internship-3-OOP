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
        public int callDuration { get => _callDuration; set => _callDuration = value; }

        public Call()
        {
            _timeOfCall = DateTime.Now;
            SetStatus();
        }

        private void SetStatus()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            var status = rnd.Next(2);
            _status = (CallStatus)status;
        }
        public enum CallStatus
        {
            InProgress,
            Missed,
            Ended
        };
    }
}
