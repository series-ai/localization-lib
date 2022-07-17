namespace Padoru.Localization
{
	public interface ILocalizationFilesProvider
	{
		LocalizationFile GetFile(string fileName);
	}
}
