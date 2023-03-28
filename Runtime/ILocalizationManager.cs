using System;
using System.Threading.Tasks;

namespace Padoru.Localization
{
	public interface ILocalizationManager
	{
		Languages CurrentLanguage { get; }
		
		event Action OnLanguageChanged;

		void SetLanguage(Languages language);

		Task LoadFile(string fileName);

		string GetLocalizedText(string fileName, string entryName);
	}
}
