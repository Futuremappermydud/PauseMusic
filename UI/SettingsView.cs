using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeatSaberMarkupLanguage.Components.Settings;
using IPA.Utilities;
using PauseMusic.Configuration;


namespace PauseMusic.UI
{
	[HotReload(RelativePathToLayout = @"SettingsView.bsml")]
	[ViewDefinition("PauseMusic.UI.SettingsView")]
	public class SettingsView : BSMLAutomaticViewController
	{
		[UIValue("FutureMessage")]
		internal string Future => "Made by <color=#87CEEB>FutureMapper</color>";

		[UIValue("enabled")]
		internal bool enabled
		{
			get => PluginConfig.Instance.enabled;
			set => PluginConfig.Instance.enabled = value;
		}

		[UIValue("fade-speed")]
		internal float fadeSpeed
		{
			get => PluginConfig.Instance.fadeSpeed;
			set => PluginConfig.Instance.fadeSpeed = value;
		}

		[UIValue("volume")]
		internal float volume
		{
			get => PluginConfig.Instance.audioVolume;
			set => PluginConfig.Instance.audioVolume = value;
		}

		[UIValue("audio-file")]
		internal string audioFile
		{
			get => PluginConfig.Instance.selectedAudioFile;
			set => PluginConfig.Instance.selectedAudioFile = value;
		}

		[UIValue("available-files")]
		private List<object> Files => Directory.GetFiles(path).Select(x => (object)Path.GetFileNameWithoutExtension(x)).ToList();

		[UIAction("#post-parse")]
		internal void PostParse()
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		internal string path = Path.Combine(UnityGame.UserDataPath, "PauseMusic");
	}
}
