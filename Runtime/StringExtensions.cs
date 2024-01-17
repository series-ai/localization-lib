using System;
using Padoru.Core;
using Padoru.Diagnostics;

namespace Padoru.Localization
{
	public static class StringExtensions
	{
		public static string ToLocalized(this string key)
		{
			try
			{
				var localizationManager = Locator.Get<ILocalizationManager>();
				
				return localizationManager.GetLocalizedText(key);
			}
			catch (Exception e)
			{
				Debug.LogException(e, Constants.LOCALIZATION_LOG_CHANNEL);

				return key;
			}
		}
	}
}