namespace Padoru.Localization
{
	public interface ILocalizationFilesLoader
	{
		LocalizationFile LoadFile(string fileName);
	}
}
