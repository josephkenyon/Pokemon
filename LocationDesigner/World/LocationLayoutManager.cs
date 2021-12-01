using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;

namespace LocationDesigner.World
{
    public class LocationLayoutManager
    {
        public static LocationLayoutJson LocationLayout { get; private set; }

        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

        public static void Initialize() {
            LocationLayout = new LocationLayoutJson();
        }

        public static void SaveFile(string fileName) {
            string jsonText = JsonConvert.SerializeObject(LocationLayout, JsonSerializerSettings);

            File.WriteAllText(fileName, JToken.Parse(jsonText).ToString(Formatting.Indented));
        }

        public static void LoadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    try
                    {
                        LocationLayoutJson locationLayoutJson = JsonConvert.DeserializeObject<LocationLayoutJson>(r.ReadToEnd(), JsonSerializerSettings);
                        LocationLayout = locationLayoutJson;
                    }
                    catch (Exception e) {
                        Trace.TraceError(e.Message);
                    }
                }
            }
            else
            {
                Trace.TraceError("LocationLayout file not found at " + filePath + ".");
            }
        }
    }
}
