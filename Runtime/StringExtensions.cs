using System;
using Padoru.Core;
using Padoru.Diagnostics;

namespace Padoru.Localization
{
    public static class StringExtensions
    {
        public static string ToLocalized(this string key)
        {
            var localizationManager = Locator.Get<ILocalizationManager>();

            if (localizationManager.TryGetLocalizedText(key, out var localizedText))
            {
                return localizedText;
            }

            Debug.LogWarning("Missing localized text for key: " + key, Constants.LOCALIZATION_LOG_CHANNEL);
#if NO_MISSING_LOC_PREFIX
            return key;
#else
            return $"MISSING:{key}";
#endif
        }
        
        public static string ToLocalized(this string key, params object[] replacements)
        {
            return string.Format(key.ToLocalized(), replacements);
        }
    }
}