using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class LocalizationManager : ILocalizationManager
	{
		private readonly Dictionary<Languages, LocalizationFile> files = new();
		
		private ILocalizationFilesLoader filesLoader;

		public bool UseMissingLogPrefix { get; private set; }
		public Languages CurrentLanguage { get; private set; }
		
		public event Action<Languages> OnLanguageChanged;

		public LocalizationManager(ILocalizationFilesLoader filesLoader, Languages startingLanguage, bool useMissingLogPrefix)
		{
			Debug.Log($"Initialized on {startingLanguage}", Constants.LOCALIZATION_LOG_CHANNEL);

			CurrentLanguage = startingLanguage;

			this.filesLoader = filesLoader;
		}

		public void Shutdown()
		{
			filesLoader = null;
			
			files.Clear();
		}

		public async Task LoadFile(Languages language, string fileUri, CancellationToken cancellationToken)
		{
			var file = await filesLoader.LoadFile(fileUri, cancellationToken);
			
			Debug.Log($"Localization file loaded {fileUri} for language {language}", Constants.LOCALIZATION_LOG_CHANNEL);

			AddFile(language, file);
		}

		public void AddFile(Languages language, LocalizationFile file)
		{
			if (file == null || file.entries == null || file.entries.Count <= 0)
			{
				return;
			}

			if (!files.ContainsKey(language))
			{
				files.Add(language, new LocalizationFile()
				{
					entries = new Dictionary<string, string>()
				});
			}

			foreach (var entry in file.entries)
			{
				files[language].entries.TryAdd(entry.Key, entry.Value);
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

		public bool HasLocalizedText(string entryName)
		{
			if(TryGetFile(CurrentLanguage, out var file))
			{
				return file.entries.ContainsKey(entryName);
			}

			return false;
		}

		public bool TryGetLocalizedText(string entryName, out string localizedText)
		{
			if(TryGetFile(CurrentLanguage, out var file))
			{
				return file.entries.TryGetValue(entryName, out localizedText);
			}

			localizedText = Constants.COULD_NOT_LOCALIZE_STRING;
			return false;
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

		private bool TryGetFile(Languages language, out LocalizationFile file)
		{
			return files.TryGetValue(language, out file);
		}
	}
}
