using System;
using System.Threading.Tasks;

namespace Padoru.Localization
{
	public interface ILocalizationManager
	{
		Languages CurrentLanguage { get; }
		
		event Action<Languages> OnLanguageChanged;

		void SetLanguage(Languages language);

		Task LoadFile(Languages language, string fileUri);

		void AddFile(Languages language, LocalizationFile file);

		string GetLocalizedText(string entryName);
	}
}
