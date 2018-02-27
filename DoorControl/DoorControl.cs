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
        public bool DoorOpen { get; set; }
        public bool DoorClose { get; set; }

        public DoorControl(IDoor door, IUserValidation UV, IEntryNotification EN)
        {
            _door = door;
            _userValidation = UV;
            _entry = EN;
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
