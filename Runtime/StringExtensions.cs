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
            // Return the naked key for now https://series-ai.atlassian.net/browse/RHO-3402
            //return $"MISSING:{key}";
            return key;
        }
    }
}