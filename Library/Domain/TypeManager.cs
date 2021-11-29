using Library.Assets;
using Library.Content;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Library.GameState.Battle;

namespace Library.Domain
{
    public static class TypeManager
    {
        public static Dictionary<Type, List<Type>> SuperEffective { get; private set; }
        public static Dictionary<Type, List<Type>> NotVeryEffective { get; private set; }
        public static Dictionary<Type, List<Type>> NoEffect { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            SuperEffective = new Dictionary<Type, List<Type>>();
            NotVeryEffective = new Dictionary<Type, List<Type>>();
            NoEffect = new Dictionary<Type, List<Type>>();

            string filePath = contentManager.RootDirectory + FileHelper.EncyclopediaDirectory + "\\superEffective" + FileHelper.JsonExtension;
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    SuperEffective = JsonConvert.DeserializeObject<Dictionary<Type, List<Type>>>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);
                }
            }
            else
            {
                Trace.TraceError("SuperEffective file not found at " + filePath + ".");
            }

            filePath = contentManager.RootDirectory + FileHelper.EncyclopediaDirectory + "\\notVeryEffective" + FileHelper.JsonExtension;
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    NotVeryEffective = JsonConvert.DeserializeObject<Dictionary<Type, List<Type>>>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);
                }
            }
            else
            {
                Trace.TraceError("NotVeryEffective file not found at " + filePath + ".");
            }

            filePath = contentManager.RootDirectory + FileHelper.EncyclopediaDirectory + "\\noEffect" + FileHelper.JsonExtension;
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    NoEffect = JsonConvert.DeserializeObject<Dictionary<Type, List<Type>>>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);
                }
            }
            else
            {
                Trace.TraceError("NoEffect file not found at " + filePath + ".");
            }
        }
    }
}
