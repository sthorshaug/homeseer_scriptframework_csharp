#region Using
using System;
using System.Net;
#endregion
using HomeseerScripts;
namespace HomeseerScripts.Scripts
{
    public class VeraLights
    {
        HomeseerWrapper hs;
        public VeraLights(HomeseerWrapper wrapper)
        {
            hs = wrapper;
        }


        #region Scripts
        /// <summary>
        /// Required since HomeSeer supports functions poorly for C# at the moment
        /// </summary>
        /// <param name="param">The command-line arguments. String separated with | </param>
        public object Main(object[] param) {
            string paramsRaw = Convert.ToString(param[0]);
            if(string.IsNullOrEmpty(paramsRaw))
            {
                hs.WriteLogEx("VeraLights", "No input parameters", "#FF0000");
                return 0;
            }
            string[] args = paramsRaw.Split('|');
            try
            {
                switch (args[0]) // function to call
                {
                    case "TestFunction1":
                        TestFunction1(args);
                        break;
                    case "TestFunction2":
                        TestFunction2(args);
                        break;
                    default:
                        hs.WriteLogEx("VeraLights", "Invalid function: " + args[0], "#FF0000");
                        break;
                }
            } catch(Exception ex) {
                hs.WriteLogEx("VeraLights", "Unhandled exception: " + ex.Message, "#FF0000");
            }
            return 0;
        }

        public void TestFunction1(string[] args)
        {
            // Do whatever you want
        }

        public void TestFunction2(string[] args)
        {
            // Do whatever you want
        }

        /// <summary>
        /// Gets the current time category.
        /// </summary>
        /// <returns>The current time category.</returns>
        public TimeCategory GetCurrentTimeCategory() 
        {
            int deviceId = hs.GetDeviceRefByName("Admin Time TimeCategory");
            if(deviceId < 1) 
            {
                throw new Exception("Could not locate the TimeCategory device");
            }
            double deviceValue = hs.DeviceValueByNameEx("Admin Time TimeCategory");
            string status = hs.DeviceVSP_GetStatus(deviceId, deviceValue, ePairStatusControl.Status);
            switch(status)
            {
                case "Morning":
                    return TimeCategory.Morning;
                case "Day":
                    return TimeCategory.Day;
                case "Evening":
                    return TimeCategory.Evening;
                case "Night":
                    return TimeCategory.Night;
                default:
                    throw new Exception("Unknown TimeCategory: " + status);
            }
        }

        public enum TimeCategory
        {
            Night,
            Morning,
            Day,
            Evening
        }
        #endregion
    }
}
