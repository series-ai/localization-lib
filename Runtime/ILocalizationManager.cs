using System;

namespace Padoru.Localization
{
	public interface ILocalizationManager
	{
		event Action OnLanguageChanged;

		void SetLanguage(Languages language);

		string GetLocalizedText(string fileName, string entryName);
	}
}
