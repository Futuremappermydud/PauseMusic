using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PauseMusic.Configuration;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace PauseMusic.Services
{
	internal class AudioLoaderService : IInitializable
	{
		private AudioClip _currentClip = null;
		private static string Path => System.IO.Path.Combine(IPA.Utilities.UnityGame.UserDataPath, "PauseMusic", PluginConfig.Instance.selectedAudioFile + ".ogg");

		public void Initialize()
		{

			LoadAudioClip();
		}

		private void LoadAudioClip()
		{
			if (_currentClip != null)
			{
				UnityEngine.Object.Destroy(_currentClip);
			}
			if (!File.Exists(Path))
			{
				return;
			}
			var www = UnityWebRequestMultimedia.GetAudioClip("file:///" + Path, AudioType.OGGVORBIS);
			{
				var webreq = www.SendWebRequest();
				webreq.completed += (op) => {
					_currentClip = DownloadHandlerAudioClip.GetContent(www);
					www.Dispose();
				};
			}
		}

		public AudioClip GetCurrentClip()
		{
			if (_currentClip) return _currentClip;
			Plugin.Log.Warn("No Pause Music Clip Found. Ignoring");
			return null;
		}
	}
}
