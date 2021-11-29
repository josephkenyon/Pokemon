using Library.GameState;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Library.Content
{
    public static class FileHelper
    {
        public static string EncyclopediaDirectory = "\\encyclopedia";
        public static string SpriteDirectory = "\\sprites";
        public static string LocationsDirectory = "\\locations";
        public static string TileSetDirectory = "\\tilesets";
        public static string WallpaperDirectory = "\\wallpapers";

        public static string MoveFileName = "\\moves";

        public static string JsonExtension = ".json";

        public static string SaveFileName(int SaveSlot) => "save" + SaveSlot + ".json";

        public static string FormatEnumNameToJson(string name) => "\\" + char.ToLower(name[0]) + name.Substring(1) + ".json";
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, };

        public static void SaveState(int SaveSlot)
        {
            string jsonText = JsonConvert.SerializeObject(GameStateManager.Instance, JsonSerializerSettings);

            File.WriteAllText(SaveFileName(SaveSlot), JToken.Parse(jsonText).ToString(Formatting.Indented));

        }

        public static GameStateManager LoadState(int SaveSlot)
        {
            if (File.Exists(SaveFileName(SaveSlot)))
            {
                using (StreamReader r = new StreamReader(SaveFileName(SaveSlot)))
                {
                    return JsonConvert.DeserializeObject<GameStateManager>(r.ReadToEnd(), JsonSerializerSettings);
                }
            }
            return null;
        }
    }
}