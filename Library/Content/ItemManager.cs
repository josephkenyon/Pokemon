using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Linq;
using Library.Assets.Json;
using Newtonsoft.Json;
using Library.Domain;

namespace Library.Content
{
    public static class ItemManager
    {
        private static ItemsConfigurationJson ItemsConfiguration { get; set; }

        public static void Load(ContentManager contentManager)
        {
            string cutScenesFilePath = contentManager.RootDirectory + FileHelper.ConfigurationDirectory + FileHelper.ItemConfigurationsFileName + FileHelper.JsonExtension;
            if (File.Exists(cutScenesFilePath))
            {
                using StreamReader r = new StreamReader(cutScenesFilePath);
                ItemsConfiguration = JsonConvert.DeserializeObject<ItemsConfigurationJson>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);
            }
        }

        public static int? GetHealAmount(ItemType itemType)
            => ItemsConfiguration.Items.Where(item => item.ItemType == itemType).First().Heal;
    }
}