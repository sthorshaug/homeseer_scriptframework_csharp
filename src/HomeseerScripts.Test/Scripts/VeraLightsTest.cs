using System;
using NUnit.Framework;
using HomeseerScripts;
using HomeseerScripts.Scripts;
namespace HomeseerScripts.Scripts.Test
{
    [TestFixture()]
    public class VeraLightsTest
    {
        [Test()]
        public void ParseTimeOfDay() {
            HomeseerWrapper wrapper = new HomeseerWrapper();
            VeraLights vera = new VeraLights(wrapper);

            var ex = Assert.Throws<Exception>(() => vera.GetCurrentTimeCategory());
            Assert.That(ex.Message, Is.EqualTo("Could not locate the TimeCategory device"));

            HomeseerWrapper.HomeseerDevice device = new HomeseerWrapper.HomeseerDevice("Admin Time TimeCategory", 1, 10.0);
            wrapper.AddHomeseerDevice(device);
            ex = Assert.Throws<Exception>(() => vera.GetCurrentTimeCategory());
            Assert.That(ex.Message, Is.EqualTo("Unknown TimeCategory: "));

            HomeseerWrapper.HomeseerDeviceVsp vsp = new HomeseerWrapper.HomeseerDeviceVsp(1, 10, "Night");
            wrapper.AddHomeseerDeviceVsp(vsp);
            vsp = new HomeseerWrapper.HomeseerDeviceVsp(1, 20, "Morning");
            wrapper.AddHomeseerDeviceVsp(vsp);
            vsp = new HomeseerWrapper.HomeseerDeviceVsp(1, 30, "Day");
            wrapper.AddHomeseerDeviceVsp(vsp);
            vsp = new HomeseerWrapper.HomeseerDeviceVsp(1, 40, "Evening");
            wrapper.AddHomeseerDeviceVsp(vsp);
            Assert.True(vera.GetCurrentTimeCategory() == VeraLights.TimeCategory.Night);

            device.DeviceValue = 20.0;
            Assert.True(vera.GetCurrentTimeCategory() == VeraLights.TimeCategory.Morning);

            device.DeviceValue = 30.0;
            Assert.True(vera.GetCurrentTimeCategory() == VeraLights.TimeCategory.Day);

            device.DeviceValue = 40.0;
            Assert.True(vera.GetCurrentTimeCategory() == VeraLights.TimeCategory.Evening);
        }
    }
}
