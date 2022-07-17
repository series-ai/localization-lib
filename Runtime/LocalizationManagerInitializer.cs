using Padoru.Core;
using UnityEngine;

namespace Padoru.Localization
{
    public class LocalizationManagerInitializer : MonoBehaviour, IInitializable, IShutdowneable
    {
        [SerializeField] private Languages language;

        public void Init()
        {
            var filesProvider = new LocalFilesProvider(Application.persistentDataPath, "json");
            var localizationManager = new LocalizationManager(filesProvider, language);

            Locator.RegisterService<ILocalizationManager>(localizationManager);
        }

        public void Shutdown()
        {
            Locator.UnregisterService<ILocalizationManager>();
        }
    }
}
