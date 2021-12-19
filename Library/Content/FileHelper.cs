using Library.Domain;
using Library.GameState;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Library.Content
{
    public static class FileHelper
    {
        // Directories
        public static string EncyclopediaDirectory = "\\encyclopedia";
        public static string SpriteDirectory = "\\sprites";
        public static string LocationsDirectory = "\\locations";
        public static string TileSetDirectory = "\\tilesets";
        public static string WallpaperDirectory = "\\wallpapers";
        public static string ConfigurationDirectory = "\\configuration";
        public static string SaveDirectory = Directory.GetCurrentDirectory() + "\\saves\\";

        // Files
        public static string LocationStichesFileName = "\\locationStiches";
        public static string CutScenesFileName = "\\cutScenes";
        public static string ItemConfigurationsFileName = "\\itemConfigurations";
        public static string MoveFileName = "\\moves";

        // Miscellaneous
        public static string JsonExtension = ".json";

        public static string SaveFileName(int SaveSlot) => "save" + SaveSlot + ".json";
        public static string FormatEnumNameToJson(string name) => "\\" + char.ToLower(name[0]) + name[1..] + ".json";

        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public static void SaveState(int SaveSlot)
        {
            Directory.CreateDirectory(SaveDirectory);

            string jsonText = JsonConvert.SerializeObject(GameStateManager.Instance, JsonSerializerSettings);

            File.WriteAllText(SaveDirectory + SaveFileName(SaveSlot), JToken.Parse(jsonText).ToString(Formatting.Indented));
        }

        public static GameStateManager LoadState(int SaveSlot)
        {
            GameStateManager gameStateManager = null;

            Directory.CreateDirectory(SaveDirectory);
            if (File.Exists(SaveFileName(SaveSlot)))
            {
                using StreamReader r = new StreamReader(SaveDirectory + SaveFileName(SaveSlot));
                gameStateManager = JsonConvert.DeserializeObject<GameStateManager>(r.ReadToEnd(), JsonSerializerSettings);
            }

            Queue<UIState> uiStateQueue = new Queue<UIState>();
            while(gameStateManager.UIStateStack.Count > 0)
            {
                uiStateQueue.Enqueue(gameStateManager.UIStateStack.Pop());
            }

            while (uiStateQueue.Count > 0)
            {
                gameStateManager.UIStateStack.Push(uiStateQueue.Dequeue());
            }

            return gameStateManager;
        }
    }
}