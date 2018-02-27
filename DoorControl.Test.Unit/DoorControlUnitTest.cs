using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;


namespace DoorControl.Test.Unit
{
    [TestFixture]
    public class DoorControlUnitTest
    {
        private IAlarm _alarm;
        private IDoor _door;
        private IEntryNotification _entryNotification;
        private IUserValidation _userValidation;
        private DoorControl _uut; 
        
        [SetUp]
        public void Setup()
        {
            _alarm = Substitute.For<IAlarm>();
            _door = Substitute.For<IDoor>();
            _entryNotification = Substitute.For<IEntryNotification>();
            _userValidation = Substitute.For<IUserValidation>();
            _uut = new DoorControl(_door,_userValidation,_entryNotification,_alarm);        
        }

        [Test]
        public void Hej_xxx_xxx()
        {
            _uut.Hej();
            _uut.RequestEntry("22").Returns(true);
            _uut.DoorOpened();
            _uut.Received().DoorClosed();

        }

    }
}
