using Library.Content;
using Library.Domain;
using Library.GameState.Base;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Library.Cutscenes
{
    public class CutsceneManager
    {
        public static Cutscene ActiveCutscene { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            string cutScenesFilePath = contentManager.RootDirectory + FileHelper.ConfigurationDirectory + FileHelper.CutScenesFileName + FileHelper.JsonExtension;
            if (File.Exists(cutScenesFilePath))
            {
                using StreamReader r = new StreamReader(cutScenesFilePath);
                List<CutsceneJson> cutSceneJsons = JsonConvert.DeserializeObject<List<CutsceneJson>>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);

                cutSceneJsons.ForEach(cutSceneJson =>
                {
                    BaseStateManager.Instance.LocationStates[cutSceneJson.LocationName].Cutscenes.Add(cutSceneJson);
                });
            }
        }

        public static void BeginCutscene(CutsceneJson cutSceneJson)
        {
            ActiveCutscene = new Cutscene
            {
                LocationName = cutSceneJson.LocationName,
                TriggerPoints = cutSceneJson.TriggerPoints,
                Repeating = cutSceneJson.Repeating
            };

            cutSceneJson.CutsceneTransactions.ForEach(cutsceneTransactionJson =>
            {
                if (cutsceneTransactionJson.CutsceneTransactionType == CutsceneTransactionType.Message)
                {
                    ActiveCutscene.CutsceneTransactions.Add(new MessageCutsceneTransaction(cutsceneTransactionJson.Messages));
                }
                else if (cutsceneTransactionJson.CutsceneTransactionType == CutsceneTransactionType.Teleport)
                {
                    ActiveCutscene.CutsceneTransactions.Add(new TeleportCutsceneTransaction(cutsceneTransactionJson.TeleportTransactions));
                }
                else if (cutsceneTransactionJson.CutsceneTransactionType == CutsceneTransactionType.Movement)
                {
                    ActiveCutscene.CutsceneTransactions.Add(new MovementCutsceneTransaction(cutsceneTransactionJson.MovementTransaction));
                }
                else if (cutsceneTransactionJson.CutsceneTransactionType == CutsceneTransactionType.Flag)
                {
                    ActiveCutscene.CutsceneTransactions.Add(new FlagCutsceneTransaction((Flag)cutsceneTransactionJson.Flag));
                }
                else if (cutsceneTransactionJson.CutsceneTransactionType == CutsceneTransactionType.Special_Action)
                {
                    ActiveCutscene.CutsceneTransactions.Add(new SpecialActionCutsceneTransaction((SpecialActionKey)cutsceneTransactionJson.SpecialActionKey));
                }
            });

            if (!ActiveCutscene.Repeating)
            {
                BaseStateManager.Instance.LocationStates[cutSceneJson.LocationName].Cutscenes.Remove(cutSceneJson);
            }

            BaseStateManager.Instance.StateStack.Push(BaseState.Cutscene);
        }

        public static void EndCutscene()
        {
            ActiveCutscene = null;
            BaseStateManager.Instance.StateStack.Pop();
        }
    }
}