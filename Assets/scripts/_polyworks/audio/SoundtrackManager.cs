namespace Polyworks 
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public struct Track 
    {
        public AudioClip clip;
        public bool isFading;
        public bool isPlaying;
    }

    [System.Serializable]
    public struct Song 
    {
        public string name;
        public int numMaxTracks;
        public int numStartingTracks;
        public bool isRandomized;
        public Track[] tracks;
    }

    public class SoundtrackManager: PolyBehaviour
    {
        #region public members
        public Song[] songs;
        public AudioSource[] sources;
        public float nearEndThreshold = 5.0f;
        public float replaceThreshold = 0.75f;
        public float nextTrackThreshold = 1.0f;
        #endregion

        #region private members
        private Song currentSong;
        private List<bool> playingTracks;
        private float timeSinceLastTrackAdd = 0;
        private int currentTrackIndex = -1;
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

            if(currentSong.tracks.Length == 0)
            {
                return;
            }

            isSongWithTracks = true;

            InitPlayingTracks();

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

            StartNextTrack();
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

        public void AddTrack(int index = -1)
        {
            if(index > -1)
            {
                // add specific track
            }
            else
            {
                // add a new, random track
            }
        }

        public void Clear()
        {
            if(currentSourceIndex > -1 && sources[currentSourceIndex].isPlaying)
            {
                sources[currentSourceIndex].Stop();
            }
            isPlaying = isSongWithTracks = isSongStarted = isActive = false;
            currentTrackIndex = currentSourceIndex = -1;
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

        private void InitPlayingTracks()
        {
            playingTracks = new List<bool>();

            for(int i = 0; i < currentSong.tracks.Length; i++)
            {
                currentSong.tracks[i].isFading = false;
                currentSong.tracks[i].isPlaying = false;

                playingTracks.Add(false);
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

        private void StartNextTrack(int trackIndex = -1)
        {
            currentSourceIndex = GetNextSourceIndex(sources.Length);
            currentTrackIndex = GetNextTrackIndex(currentSong, trackIndex);

            AudioSource source = sources[currentSourceIndex];

            source.clip = currentSong.tracks[currentTrackIndex].clip;
            source.Play();

            playingTracks[currentTrackIndex] = true;
        }

        private int GetNextSourceIndex(int arrayLength)
        {
            int index = 0;
            if(currentSourceIndex < arrayLength - 1)
            {
                index++;
            } 
            return index;
        }

        private int GetNextTrackIndex(Song song, int trackIndex = -1)
        {
            int index = 0;

            if(trackIndex > -1)
            {
                // specific track already playing
                if(playingTracks[trackIndex])
                {
                    return -1;
                }

                currentTrackIndex = trackIndex;
                return currentTrackIndex;
            }
            else if(song.isRandomized)
            {
                int lastIndex = index;

                while(currentTrackIndex == lastIndex)
                {
                    index = Random.Range(0, song.tracks.Length);
                }
            }
            else if(currentTrackIndex < song.tracks.Length - 1)
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