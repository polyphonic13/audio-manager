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
        public bool isRandomized;
        public AudioClip[] clips;
    }

    public class SoundtrackManager: Singleton<SoundtrackManager>
    {
        #region public members
        public Song[] songs;
        public AudioSource[] sources;
        public int defaultMaxTracks = 3;
        public float nearEndThreshold = 5.0f;
        public float replaceThreshold = 0.75f;
        public float nextTrackThreshold = 1.0f;
        #endregion

        #region private members
        private Song currentSong;
        private List<bool> playingClips;
        private float timeSinceLastTrackAdd = 0;
        private int currentClipIndex = -1;
        private int currentSourceIndex = -1;
        private bool isSongWithTracks = false;
        private bool isActive = false;
        private bool isSongStarted = false;
        private bool isPlaying = false;
        #endregion

        #region public methods
        public void PlaySong(string name, bool isAutoStart = true)
        {
            isSongStarted = isPlaying = isSongWithTracks = false;
            currentSong = GetSong(name);

            if(currentSong.clips.Length == 0)
            {
                return;
            }

            isSongWithTracks = true;

            InitPlayingClips();

            if(isAutoStart)
            {
                Play();
            }
        }

        public void Play()
        {
            if(isSongStarted || !isSongWithTracks)
            {
                return;
            }

            StartNextClip();
        }

        public void Pause()
        {
            if(!isSongStarted || !isPlaying)
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
            if(currentSourceIndex > -1 && sources[currentSourceIndex].isPlaying)
            {
                sources[currentSourceIndex].Stop();
            }
            isPlaying = isSongWithTracks = isSongStarted = isActive = false;
            currentClipIndex = currentSourceIndex = -1;
        }
        #endregion

        #region private methods
        private void Update()
        {
            if(isPlaying)
            {
                // if()
            }
            // check for track ending soon
            // queue next track

            // check for odds of dropping track
            // determine track to drop

            // check for odds of adding additional track
        }

        private void InitPlayingClips()
        {
            playingClips = new List<bool>();

            foreach(AudioClip clip in currentSong.clips)
            {
                playingClips.Add(false);
            }
        }

        private Song GetSong(string name)
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

        private void StartNextClip()
        {
            currentSourceIndex = GetCurrentSourceIndex(sources.Length);
            currentClipIndex = GetCurrentClipIndex(currentSong);

            AudioSource source = sources[currentSourceIndex];

            source.clip = currentSong.clips[currentClipIndex];
            source.Play();

            playingClips[currentClipIndex] = true;
        }

        private int GetCurrentSourceIndex(int arrayLength)
        {
            int index = 0;
            if(currentSourceIndex < arrayLength - 1)
            {
                index++;
            } 
            return index;
        }

        private int GetCurrentClipIndex(Song song)
        {
            int index = 0;

            if(song.isRandomized)
            {
                int lastIndex = index;

                while(currentClipIndex == lastIndex)
                {
                    index = Random.Range(0, song.clips.Length);
                }
            }
            else if(currentClipIndex < song.clips.Length - 1)
            {
                index++;
            }

            return index;
        }

        private void OnDestroy()
        {
            Clear();
        }
        #endregion
    }
}