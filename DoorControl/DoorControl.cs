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
        public bool DoorOpen { get; set; }
        public bool DoorClose { get; set; }

        public DoorControl(IDoor door, IUserValidation UV, IEntryNotification EN)
        {
            _door = door;
            _userValidation = UV;
            _entry = EN;
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
                    else if(RequestEntry(id)==false)
                    {   
                        _entry.NotifyEntryDenied();
                        _state = DoorControlState.Closed;
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
