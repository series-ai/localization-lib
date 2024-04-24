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
            return $"MISSING:{key}";
        }
    }
}