namespace Polyworks 
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Audio;
	public class AudioManager : Singleton<AudioManager>
	{
		public bool isAutoAdvance;
		public AudioClip[] clips;

		private AudioSource _source;

		private int _index;
		private bool _isPlaying = false;
		public void Init() 
		{
			// Debug.Log("audio manager init");
			_source = gameObject.GetComponent<AudioSource>();
		}
	
		public bool GetIsPlaying()
		{
			return _isPlaying;
		}

		public float GetTime()
		{
			return _source.time;
		}

		public float GetLength()
		{
			if(!_source.clip)
			{
				return -1f;
			}
			return _source.clip.length;
		}

		public void Next()
		{
			Debug.Log("next");
			if(_index < clips.Length - 1)
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
				_index = clips.Length - 1;
			}
			Play(_index);
		}

		public void Pause()
		{
			if(!_source.isPlaying)
			{
				return;
			}
			_source.Pause();
		}

		public void Resume()
		{
			if(_source.isPlaying)
			{
				return;
			}
			_source.UnPause();
		}

		void Update () 
		{
			if(_isPlaying)
			{
				// Debug.Log("time: " + _source.time + " / " + _source.clip.length);
				if(_source.time	>= _source.clip.length)
				{
					Debug.Log("ended");
					_isPlaying = false;

					if(isAutoAdvance && _index < clips.Length - 1)
					{
						_index++;
						Play(_index);
					}
				}
			}
		}

		public void Play(int index = 0) 
		{
			if(_source.isPlaying)
			{
				_source.Stop();
			}
			Debug.Log("AudioManager/Play, index = " + index);
			if(index < clips.Length)
			{
				Debug.Log(" clip = " + clips[index]);
				_source.clip = clips[index];
				_source.Play();
				_index = index;
				_isPlaying = true;
			}
		}
	}
}

