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
		
		public bool UseMissingLogPrefix { get; }
		public Languages CurrentLanguage { get; private set; }
		public Languages DefaultLanguage { get; set; }
		
		public event Action<Languages> OnLanguageChanged;

		public LocalizationManager(ILocalizationFilesLoader filesLoader, Languages startingLanguage, bool useMissingLogPrefix, Languages defaultLanguage = Languages.en_US)
		{
			Debug.Log($"Initialized on {startingLanguage}", Constants.LOCALIZATION_LOG_CHANNEL);

			CurrentLanguage = startingLanguage;

			DefaultLanguage = defaultLanguage;
			
			UseMissingLogPrefix = useMissingLogPrefix;

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

		public string GetLocalizedTextForLanguage(string entryName, Languages language)
		{
			var file = GetFile(language);
			
			if (!file.entries.ContainsKey(entryName))
			{
				throw new Exception($"The file for {language} does not contain an entry for {entryName}");
			}

			return file.entries[entryName];
		}

		public string GetLocalizedText(string entryName)
		{
			return GetLocalizedTextForLanguage(entryName, CurrentLanguage);
		}
		
		public bool HasLocalizedTextInLanguage(string entryName, Languages languages)
		{
			if (TryGetFile(languages, out var file))
			{
				return file.entries.ContainsKey(entryName);
			}

			return false;
		}
		
		public bool HasLocalizedText(string entryName)
		{
			return HasLocalizedTextInLanguage(entryName, CurrentLanguage);
		}
		
		public bool TryGetLocalizedTextForLanguage(string entryName, Languages language, out string localizedText)
		{
			if (TryGetFile(language, out var file))
			{
				return file.entries.TryGetValue(entryName, out localizedText);
			}

			localizedText = Constants.COULD_NOT_LOCALIZE_STRING;
			return false;
		}

		public bool TryGetLocalizedText(string entryName, out string localizedText)
		{
			return TryGetLocalizedTextForLanguage(entryName, CurrentLanguage, out localizedText);
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
