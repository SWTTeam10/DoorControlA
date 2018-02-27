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
            switch (_state)
            {
                case DoorControlState.Closed:
                    _door.Open();
                    _state = DoorControlState.Opening;
                    break;

                case DoorControlState.Opening:
                    _door.Close();
                    _state = DoorControlState.Closing;
                    break;

                case DoorControlState.Closing:

                    _state = DoorControlState.Closed;
                    break;
            }   
        }

        public void RequestEntry(string id)
        {
            
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
