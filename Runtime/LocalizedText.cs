using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Padoru.Core;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class LocalizedText : MonoBehaviour
	{
		[SerializeField] private string entryName;
		[SerializeField] private bool useUnsafeLoc;

		private Text text;
		private TMP_Text tmpText;
		private ILocalizationManager localizationManager;

		private void Awake()
		{
			text = GetComponent<Text>();
			tmpText = GetComponent<TMP_Text>();

			localizationManager = Locator.Get<ILocalizationManager>();
			localizationManager.OnLanguageChanged += OnLanguageUpdates;

			UpdateText();
		}

		private void OnDestroy()
		{
			if(localizationManager != null)
			{
				localizationManager.OnLanguageChanged -= OnLanguageUpdates;
			}
		}

		private void OnLanguageUpdates(Languages language)
		{
			UpdateText();
		}

		[ContextMenu("Localize text")]
		private void UpdateText()
		{
			var localizedText = Constants.COULD_NOT_LOCALIZE_STRING;

			try
			{
				if (useUnsafeLoc)
                {
					localizedText = localizationManager.GetLocalizedText(entryName);
				}
				else
                {
					localizedText = entryName.ToLocalized();
				}
				
			}
			catch (Exception e)
			{
				Debug.LogException(e, Constants.LOCALIZATION_LOG_CHANNEL);
			}

			if (text != null)
			{
				text.text = localizedText;
			}

			if (tmpText != null)
			{
				tmpText.text = localizedText;
			}

			Debug.Log($"Text updated to: {localizedText}", Constants.LOCALIZATION_LOG_CHANNEL, gameObject);
		}
	}
}
