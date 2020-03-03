using Harmony;
using Oculus.Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using QModManager.API.ModLoading;
namespace SNHardcorePlus
{
    [QModCore]
    public class QPatch
    {
        [QModPatch]
        public static void Patch()
        {
            ManageSettingsFile();
            
            HarmonyInstance harmony = HarmonyInstance.Create("qwiso.snhardcoreplus.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


        private static void ManageSettingsFile()
        {
            string modDirectory = Environment.CurrentDirectory + @"\QMods";
            string settingsPath = modDirectory + @"\SNHardcorePlus\config.json";

            if (!File.Exists(settingsPath))
            {
                WriteDefaultSettingsFile(settingsPath);
                return;
            }

            var userSettings = JsonConvert.DeserializeObject<HCPSettings>(File.ReadAllText(settingsPath));
            ApplyUserSettings(userSettings);
        }


        private static void ApplyUserSettings(HCPSettings userSettings)
        {
            var fields = typeof(HCPSettings).GetFields();

            foreach (var field in fields)
            {
                var userValue = field.GetValue(userSettings);
                field.SetValue(HCPSettings.Instance, userValue);
            }
        }

        private static void WriteDefaultSettingsFile(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(HCPSettings.Instance));
        }
    }
}
