namespace Polyworks 
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class AudioManager : MonoBehaviour
	{

		private AudioManager _instance;

		public AudioManager Instance() 
		{
			if(_instance == null) 
			{
				_instance = new AudioManager();
			}
			return _instance;
		}

		public void Init() 
		{

		}
	
		void Update () 
		{
		
		}
	}
}

