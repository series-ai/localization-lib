using Padoru.Core;
using System.IO;
using Padoru.Core.Files;
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

            var fileManager = Locator.Get<IFileManager>();
            
            var filesLoader = new LocalFilesLoader(path, "json", fileManager);
            var localizationManager = new LocalizationManager(filesLoader, language);

            Locator.Register<ILocalizationManager>(localizationManager);
        }

        public void Shutdown()
        {
            Locator.Unregister<ILocalizationManager>();
        }
    }
}
