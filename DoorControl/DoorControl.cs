using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorControl
{
    public class DoorControl
    {
        private IDoor _door;
        private IUserValidation _userValidation;
        private IEntryNotification _entry;
        private DoorControlState _state;
        private IAlarm _alarm; 
        //public bool DoorOpen { get; set; }
        //public bool DoorClose { get; set; }
        private bool DoorOpen;

        private bool DoorClose; 

        public DoorControl(IDoor door, IUserValidation UV, IEntryNotification EN, IAlarm alarm)
        {
            _door = door;
            _userValidation = UV;
            _entry = EN;
            _alarm = alarm; 
            _state = DoorControlState.Closed;
        }

        private enum DoorControlState
        {
            Closed,
            Closing,
            Opening
        }

        public void Hej()
        {
            var id = "";
            switch (_state)
            {
                  
                case DoorControlState.Closed:
                    if (RequestEntry(id))
                    {
                        _door.Open();
                        _entry.NotifyEntryGranted();
                        _state = DoorControlState.Opening;    
                    }
                    if (RequestEntry(id)==false)
                    {   
                        _entry.NotifyEntryDenied();
                        _state = DoorControlState.Closed;
                    }
                    else if(RequestEntry(id) == false && DoorOpen)
                    {
                        _door.Close();
                        _alarm.SignalAlarm();
                    }
                    break;

                case DoorControlState.Opening:
                    DoorOpened();
                    //_door.Close();
                    _state = DoorControlState.Closing;
                    break;

                case DoorControlState.Closing:
                    _door.Close();
                    DoorClosed();
                    _state = DoorControlState.Closed;
                    break;

            }   
        }

        public bool RequestEntry(string id)
        {
           return  _userValidation.ValidateEntryRequest(id);
        }

        public void DoorOpened()
        {
            DoorClose = false;
            DoorOpen = true;
        }

        public void DoorClosed()
        {
            DoorClose = true;
            DoorOpen = false;
        }
    }
}
