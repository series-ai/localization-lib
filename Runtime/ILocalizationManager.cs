using System;
using System.Threading;
using System.Threading.Tasks;

namespace Padoru.Localization
{
	public interface ILocalizationManager
	{
		bool UseMissingLogPrefix { get; }
		
		Languages CurrentLanguage { get; }

		Languages DefaultLanguage { get; set; }
		
		event Action<Languages> OnLanguageChanged;

		void SetLanguage(Languages language);

		Task LoadFile(Languages language, string fileUri, CancellationToken cancellationToken);

		void AddFile(Languages language, LocalizationFile file);

		string GetLocalizedTextForLanguage(string entryName, Languages language);
		
		string GetLocalizedText(string entryName);

		bool HasLocalizedTextInLanguage(string entryName, Languages languages);
		
		bool HasLocalizedText(string entryName);

		bool TryGetLocalizedTextForLanguage(string entryName, Languages language, out string localizedText);
		
		bool TryGetLocalizedText(string entryName, out string localizedText);
	}
}
