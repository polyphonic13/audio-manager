namespace Polyworks 
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public struct SceneClipCollection 
    {
        public string scene;
        public int maxTracks;
        public AudioClip[] clips;
    }

    public class SoundtrackManager: Singleton<SoundtrackManager>
    {
        public SceneClipCollection[] sceneClipCollection;

        public AudioSource[] sources;
        public int defaultMaxTracks;

        public float nearEndThreshold;

        public float replaceThreshold;

        private SceneClipCollection currentClips;

        private int currentSource;

        private bool isSceneWithTracks;

        private bool isActive;

        private bool isSceneStarted;

        private bool isPlaying;

        public void Init()
        {

        }

        public void InitScene(string scene, bool isAutoStart = true)
        {
            isSceneStarted = isPlaying = isSceneWithTracks = false;
            currentClips = GetSceneClipCollection(scene);

            if(currentClips.clips.Length > 0)
            {
                isSceneWithTracks = true;

                if(isAutoStart)
                {
                    Play();
                }

            }
        }

        public void Play()
        {
            if(isSceneStarted)
            {
                return;
            }
            isSceneStarted = true;

        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }

        public void Clear()
        {

        }

        private void Update()
        {
            if(isActive && isSceneWithTracks)
            {

            }
            // check for track ending soon
            // queue next track

            // check for odds of dropping track
            // determine track to drop

            // check for odds of adding additional track
        }

        private SceneClipCollection GetSceneClipCollection(string scene)
        {

            foreach(SceneClipCollection clipCollection in sceneClipCollection)
            {
                if(clipCollection.scene == scene)
                {
                    return clipCollection;
                }
            }

            return new SceneClipCollection();
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}