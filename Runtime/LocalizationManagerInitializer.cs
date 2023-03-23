using Padoru.Core;
using Padoru.Core.Files;
using UnityEngine;

namespace Padoru.Localization
{
    public class LocalizationManagerInitializer : MonoBehaviour, IInitializable, IShutdowneable
    {
        [SerializeField] private Languages language;
        [SerializeField] private string localizationsFolder;

        public void Init()
        {
            var fileManager = Locator.Get<IFileManager>();
            
            var filesLoader = new LocalFilesLoader(fileManager);
            var localizationManager = new LocalizationManager(
                filesLoader, 
                language,
                Protocols.LOCAL_JSON_PPROTOCOL,
                localizationsFolder,
                ".json");

            Locator.Register<ILocalizationManager>(localizationManager);
        }

        public void Shutdown()
        {
            Locator.Unregister<ILocalizationManager>();
        }
    }
}
