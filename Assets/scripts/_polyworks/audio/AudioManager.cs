namespace Polyworks 
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Audio;
	public class AudioManager : Singleton<AudioManager>
	{
		public bool isAutoAdvance;
		public bool isCrossFade;
		// public AudioClip[] clips;

		public AudioSource[] sources;

		private const float FADE_TIME = 0.5f;
		private int _index;
		private AudioSource _current;

		private bool _isPlaying = false;


		public void Init() 
		{
			Debug.Log("audio manager init, sources.length = " + sources.Length);
			// sources = gameObject.GetComponent<AudioSource>();
			_current = sources[0];
		}
	
		public bool GetIsPlaying()
		{
			return _isPlaying;
		}

		public float GetTime()
		{
			return _current.time;
		}

		public float GetLength()
		{
			if(!_current.clip)
			{
				return -1f;
			}
			return _current.clip.length;
		}

		public void Next()
		{
			Debug.Log("next");
			if(_index < sources.Length - 1)
			{
				_index++;
			} 
			else 
			{
				_index = 0;
			}
			Play(_index);
		}

		public void Previous()
		{
			Debug.Log("prev");
			if(_index > 0)
			{
				_index--;
			}
			else
			{
				_index = sources.Length - 1;
			}
			Play(_index);
		}

		public void Pause()
		{
			if(!_current.isPlaying)
			{
				return;
			}
			_current.Pause();
		}

		public void Resume()
		{
			if(_current.isPlaying)
			{
				return;
			}
			_current.UnPause();
		}

		private IEnumerator FadeOut(AudioSource source)
		{
			while(source.volume > 0.1)
			{
				Mathf.Lerp(source.volume, 0, FADE_TIME * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			source.volume = 0;
		}

		private IEnumerator FadeIn(AudioSource source)
		{
			while(source.volume < 1)
			{
				Mathf.Lerp(source.volume, 1, FADE_TIME * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			source.volume = 1;
		}
		
		void Update () 
		{
			if(_isPlaying)
			{
				// Debug.Log("time: " + sources.time + " / " + sources.clip.length);
				if(_current.time >= _current.clip.length)
				{
					Debug.Log("ended");
					_isPlaying = false;

					if(isAutoAdvance && _index < sources.Length - 1)
					{
						_index++;
						Play(_index);
					}
				}
			}
		}

		public void Play(int index = 0) 
		{
			if(_current.isPlaying)
			{
				if(!isCrossFade)
				{
					_current.Stop();
				}
				else
				{
					FadeOut(_current);
				}
			}
			Debug.Log("AudioManager/Play, index = " + index);
			if(index < sources.Length)
			{
				Debug.Log(" clip = " + sources[index].clip);
				// sources.clip = clips[index];
				_index = index;
				_current = sources[index];
				_current.Play();
				// sources[_index].Play();

				if(isCrossFade)
				{
					FadeIn(_current);
				}				
				_isPlaying = true;
			}
		}
	}
}

