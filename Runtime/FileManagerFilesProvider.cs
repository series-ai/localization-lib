using System.Threading;
using Padoru.Core.Files;
using System.Threading.Tasks;
using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class FileManagerFilesLoader : ILocalizationFilesLoader
	{
		private readonly IFileManager fileManager;

		public FileManagerFilesLoader(IFileManager fileManager)
		{
			this.fileManager = fileManager;
		}

		public async Task<LocalizationFile> LoadFile(string fileUri, CancellationToken cancellationToken)
		{
			if (!await fileManager.Exists(fileUri, cancellationToken))
			{
				Debug.LogError($"Cannot load file {fileUri} because it does not exist", Constants.LOCALIZATION_LOG_CHANNEL);
				return null;
			}

			var file = await fileManager.Read<LocalizationFile>(fileUri, cancellationToken);

			return file.Data;
		}
	}
}
