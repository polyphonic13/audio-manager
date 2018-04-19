namespace Polyworks 
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Audio;
	public class AudioManager : Singleton<AudioManager>
	{
		public bool isAutoAdvance;
		public AudioMixer mixer;
		public AudioClip[] clips;

		private AudioSource _source;

		private int _index;
		private bool _isPlaying = false;
		public void Init() 
		{
			Debug.Log("audio manager init, mixer = " + mixer);
			_source = gameObject.GetComponent<AudioSource>();
		}
	
		void Update () 
		{
			if(_isPlaying)
			{
				if(!_source.isPlaying)
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

