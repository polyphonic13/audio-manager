using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polyworks;

public class Main : MonoBehaviour 
{
	private AudioManager _audioManager;

	// Use this for initialization
	void Awake () 
	{
		_audioManager = AudioManager.Instance;
		if(_audioManager)
		{
			_audioManager.Init();
			_audioManager.Play();
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
