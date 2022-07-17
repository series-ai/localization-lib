using Padoru.Core;
using System.IO;
using UnityEngine;

namespace Padoru.Localization
{
    public class LocalizationManagerInitializer : MonoBehaviour, IInitializable, IShutdowneable
    {
        [SerializeField] private Languages language;
        [SerializeField] private string projectPath;

        public void Init()
        {
            var path = Path.Combine(Application.dataPath, projectPath);

            var filesProvider = new LocalFilesProvider(path, "json");
            var localizationManager = new LocalizationManager(filesProvider, language);

            Locator.RegisterService<ILocalizationManager>(localizationManager);
        }

        public void Shutdown()
        {
            Locator.UnregisterService<ILocalizationManager>();
        }
    }
}
