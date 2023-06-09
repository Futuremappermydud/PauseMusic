using IPA.Utilities;
using PauseMusic.Configuration;
using PauseMusic.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PauseMusic.Managers
{
	internal class PauseAudioManager : MonoBehaviour
	{
		private IGamePause _gamePause;
		private AudioLoaderService _audioLoaderService;
		private AudioSource _audioSource;

		[Inject]
		private void Initialize(IGamePause gamePause, AudioLoaderService audioLoaderService)
		{
			_audioLoaderService = audioLoaderService;
			_gamePause = gamePause;
			_audioSource = gameObject.AddComponent<AudioSource>();
			_audioSource.ignoreListenerPause = true;
			_audioSource.loop = true;
			_audioSource.playOnAwake = false;
			_audioSource.volume = 0f;

			_gamePause.didPauseEvent += Pause;
			_gamePause.willResumeEvent += Resume;
		}

		private IEnumerator Fade(float to, bool stop = false)
		{
			while (Mathf.Abs(_audioSource.volume - to) > 0.05f) 
			{
				_audioSource.volume = Mathf.Lerp(_audioSource.volume, to, Time.deltaTime * PluginConfig.Instance.fadeSpeed);
				yield return new WaitForEndOfFrame();
			}
			if(stop)
				_audioSource.Stop();
		}

		private void Pause()
		{
			AudioClip clip = _audioLoaderService.GetCurrentClip();
			if (_audioSource == null || clip == null) 
				return;
			_audioSource.clip = clip;
			_audioSource.Play();
			StopAllCoroutines();
			StartCoroutine(Fade(PluginConfig.Instance.audioVolume));
		}

		private void Resume()
		{
			if (_audioSource != null)
			{
				StopAllCoroutines();
				StartCoroutine(Fade(0f, true));
			}
		}
	}
}
