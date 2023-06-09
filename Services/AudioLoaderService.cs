using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace PauseMusic.Services
{
	internal class AudioLoaderService : IInitializable
	{
		private AudioClip currentClip = null;
		private string path => Path.Combine(IPA.Utilities.UnityGame.UserDataPath, "PauseMusic.ogg");
		public void Initialize()
		{
			var watcher = new FileSystemWatcher(Path.GetDirectoryName(path));

			watcher.NotifyFilter = NotifyFilters.LastWrite;

			watcher.Changed += OnChanged;
			watcher.Created += OnChanged;

			watcher.Filter = "PauseMusic.ogg";
			watcher.EnableRaisingEvents = true;

			LoadAudioClip();
		}

		private void OnChanged(object sender, FileSystemEventArgs e)
		{
			if (e.ChangeType != WatcherChangeTypes.Changed)
			{
				return;
			}
			LoadAudioClip();
		}

		private void LoadAudioClip()
		{
			if (currentClip != null)
			{
				UnityEngine.Object.Destroy(currentClip);
			}
			UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, AudioType.OGGVORBIS);
			{
				var webreq = www.SendWebRequest();
				webreq.completed += (op) => {
					currentClip = DownloadHandlerAudioClip.GetContent(www);
					www.Dispose();
				};
			}
		}

		public AudioClip GetCurrentClip()
		{
			if(!currentClip)
			{
				Plugin.Log.Warn("No Pause Music Clip Found. Ignoring");
				return null;
			}
			return currentClip;
		}
	}
}
