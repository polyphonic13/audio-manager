namespace Polyworks 
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public struct Song 
    {
        public string name;
        public int numMaxTracks;
        public int numStartingTracks;

        public bool isStartRandomized;

        public AudioClip[] clips;
    }

    public class SoundtrackManager: Singleton<SoundtrackManager>
    {
        public Song[] songs;

        public AudioSource[] sources;
        public int defaultMaxTracks;

        public float nearEndThreshold;

        public float replaceThreshold;

        private Song currentSong;

        private int currentSource;

        private bool isSongWithTracks;

        private bool isActive;

        private bool isSongStarted;

        private bool isPlaying;

        public void Init()
        {

        }


        public void InitSong(string name, bool isAutoStart = true)
        {
            isSongStarted = isPlaying = isSongWithTracks = false;
            currentSong = GetSceneClipCollection(name);

            if(currentSong.clips.Length == 0)
            {
                return;
            }

            isSongWithTracks = true;

            if(isAutoStart)
            {
                Play();
            }
        }

        public void Play()
        {
            if(isSongStarted)
            {
                return;
            }
            isSongStarted = isPlaying = true;

        }

        public void Pause()
        {
            if(!isPlaying)
            {
                return;
            }
            isPlaying = false;
        }

        public void Resume()
        {
            if(!isSongStarted || isPlaying)
            {
                return;
            }
            isPlaying = true;
        }

        public void Clear()
        {

        }

        private void Update()
        {
            if(isActive && isSongWithTracks)
            {

            }
            // check for track ending soon
            // queue next track

            // check for odds of dropping track
            // determine track to drop

            // check for odds of adding additional track
        }

        private Song GetSceneClipCollection(string name)
        {
            foreach(Song song in songs)
            {
                if(song.name == name)
                {
                    return song;
                }
            }

            return new Song();
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}