using System;
using System.Collections.Generic;
namespace HomeseerScripts
{
    public class HomeseerWrapper
    {
        private List<HomeseerDevice> HomeseerDevices { get; set; }
        private List<HomeseerDeviceVsp> HomeseerDevicesVsp { get; set; }

        public HomeseerWrapper()
        {
            HomeseerDevices = new List<HomeseerDevice>();
            HomeseerDevicesVsp = new List<HomeseerDeviceVsp>();
        }

        public void WriteLogEx(string type, string message, string color) {
            Console.WriteLine(String.Format("{0} - {1}", type, message));
        }

        public void WriteLog(string type, string message) {
            WriteLogEx(type, message, "#000000");
        }

        public int GetDeviceRefByName(string name)
        {
            HomeseerDevice device = GetHomeseerDeviceByName(name);
            if(device == null) {
                return -1;
            }
            return device.DeviceId;
        }

        public double DeviceValueByNameEx(string name)
        {
            HomeseerDevice device = GetHomeseerDeviceByName(name);
            if (device == null)
            {
                return 0.0;
            }
            return device.DeviceValue;
        }

        public string DeviceVSP_GetStatus(int deviceRef, double deviceValue, ePairStatusControl type) {
            HomeseerDeviceVsp device = GetHomeseerDevicevspByIdAndValue(deviceRef, deviceValue);
            if (device == null)
            {
                return "";
            }
            return device.ValueAsString;
        }


        public void AddHomeseerDevice(HomeseerDevice device) {
            HomeseerDevices.Add(device);
        }

        public void AddHomeseerDeviceVsp(HomeseerDeviceVsp vsp) {
            HomeseerDevicesVsp.Add(vsp);
        }

        public HomeseerDevice GetHomeseerDeviceByName(string name) {
            foreach(HomeseerDevice device in HomeseerDevices) {
                if(name.Equals(device.Name)) {
                    return device;
                }
            }
            return null;
        }

        public HomeseerDeviceVsp GetHomeseerDevicevspByIdAndValue(int id, double value)
        {
            foreach (HomeseerDeviceVsp device in HomeseerDevicesVsp)
            {
                if (id == device.DeviceId && Math.Abs(value - device.Value) < 0.005)
                {
                    return device;
                }
            }
            return null;
        }

        public class HomeseerDevice
        {
            public string Name { get; private set; }
            public int DeviceId { get; private set; }
            public double DeviceValue { get; set; }

            public HomeseerDevice(string name, int id, double value)
            {
                this.Name = name;
                this.DeviceId = id;
                this.DeviceValue = value;
            }
        }

        public class HomeseerDeviceVsp
        {
            public int DeviceId { get; private set; }
            public double Value { get; set; }
            public string ValueAsString { get; set; }

            public HomeseerDeviceVsp(int id, double value, string valueAsString) {
                DeviceId = id;
                Value = value;
                ValueAsString = valueAsString;
            }
        }

    }
    public enum ePairStatusControl {
        Status,
        Control,
        Both
    }
}
