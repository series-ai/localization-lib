using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class LocalizationManager : ILocalizationManager
	{
		private readonly ILocalizationFilesLoader filesLoader;
		private readonly Dictionary<Languages, LocalizationFile> files = new();

		public Languages CurrentLanguage { get; private set; }
		
		public event Action<Languages> OnLanguageChanged;

		public LocalizationManager(ILocalizationFilesLoader filesLoader, Languages startingLanguage)
		{
			Debug.Log($"Initialized on {startingLanguage}", Constants.LOCALIZATION_LOG_CHANNEL);

			CurrentLanguage = startingLanguage;

			this.filesLoader = filesLoader;
		}

		public async Task LoadFile(Languages language, string fileUri)
		{
			var file = await filesLoader.LoadFile(fileUri);
			
			if (file != null)
			{
				files.Add(language, file);
				
				Debug.Log($"Localization file loaded {fileUri} for language {language}", Constants.LOCALIZATION_LOG_CHANNEL);
			}
		}

		public void RegisterFile(Languages language, LocalizationFile file)
		{
			if (file != null && files.TryAdd(language, file))
			{
				Debug.Log($"Localization file registered for language {language}", Constants.LOCALIZATION_LOG_CHANNEL);
			}
		}

		public string GetLocalizedText(string entryName)
		{
			var file = GetFile(CurrentLanguage);
			
			if(!file.entries.ContainsKey(entryName))
			{
				throw new Exception($"The file for {CurrentLanguage} does not contain an entry for {entryName}");
			}

			return file.entries[entryName];
		}

		public void SetLanguage(Languages language)
		{
			CurrentLanguage = language;

			Debug.Log($"Language changed to {language}", Constants.LOCALIZATION_LOG_CHANNEL);

			OnLanguageChanged?.Invoke(language);
		}

		private LocalizationFile GetFile(Languages language)
		{
			if (!files.ContainsKey(language))
			{
				throw new Exception($"No localization file loaded for language: {language}");
			}

			return files[language];
		}
	}
}
