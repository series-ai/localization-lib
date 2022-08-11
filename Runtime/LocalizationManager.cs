using System;
using System.Collections.Generic;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class LocalizationManager : ILocalizationManager
	{
		private Languages language;
		private ILocalizationFilesLoader filesLoader;
		private Dictionary<string, LocalizationFile> files = new Dictionary<string, LocalizationFile>();

		public event Action OnLanguageChanged;

		public LocalizationManager(ILocalizationFilesLoader filesLoader, Languages language)
		{
			Debug.Log($"Initialized on {language}", Constants.LOCALIZATION_LOG_CHANNEL);

			this.filesLoader = filesLoader;
			this.language = language;
		}

		public string GetLocalizedText(string fileName, string entryName)
		{
			var file = GetFile(fileName);
			if(file == null)
			{
				throw new Exception($"Could not find a localization file of name: {fileName}");
			}

			if(!file.entries.ContainsKey(entryName))
			{
				throw new Exception($"The file {fileName} does not contain an entry for {entryName}");
			}

			if (!file.entries[entryName].ContainsKey(language))
			{
				throw new Exception($"The entry {entryName} of the file {fileName} is not localized on language: {language}");
			}

			return file.entries[entryName][language];
		}

		public void SetLanguage(Languages language)
		{
			this.language = language;

			Debug.Log($"Language changed to {language}", Constants.LOCALIZATION_LOG_CHANNEL);

			OnLanguageChanged?.Invoke();
		}

		private LocalizationFile GetFile(string fileName)
		{
			if (!files.ContainsKey(fileName))
			{
				var file = filesLoader.LoadFile(fileName);
				if (file != null)
				{
					files.Add(fileName, file);
					Debug.Log($"Localization file loaded {fileName}", Constants.LOCALIZATION_LOG_CHANNEL);
				}
			}

			return files[fileName];
		}
	}
}
