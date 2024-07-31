using System;
using System.Threading;
using System.Threading.Tasks;

namespace Padoru.Localization
{
	public interface ILocalizationManager
	{
		Languages CurrentLanguage { get; }
		
		event Action<Languages> OnLanguageChanged;

		void SetLanguage(Languages language);

		Task LoadFile(Languages language, string fileUri, CancellationToken cancellationToken);

		void AddFile(Languages language, LocalizationFile file);

		string GetLocalizedText(string entryName);

		bool HasLocalizedText(string entryName);

		bool TryGetLocalizedText(string entryName, out string localizedText);
	}
}
