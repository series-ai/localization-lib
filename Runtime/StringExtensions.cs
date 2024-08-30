using Padoru.Core;
using Padoru.Diagnostics;

namespace Padoru.Localization
{
    public static class StringExtensions
    {
        private const string MISSING_LOC_PREFIX = "MISSING:";
        
        public static string ToLocalized(this string key)
        {
            var localizationManager = Locator.Get<ILocalizationManager>();

            if (localizationManager.TryGetLocalizedText(key, out var localizedText))
            {
                return localizedText;
            }

            Debug.LogWarning("Missing localized text for key: " + key, Constants.LOCALIZATION_LOG_CHANNEL);

            if (localizationManager.UseMissingLogPrefix)
            {
                return $"{MISSING_LOC_PREFIX}{key}";
            }
            
            return key;
        }
        
        public static string ToLocalized(this string key, params object[] replacements)
        {
            return string.Format(key.ToLocalized(), replacements);
        }
    }
}