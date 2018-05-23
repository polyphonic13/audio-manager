namespace Polyworks 
{
    using UnityEngine;
    using Polyworks;

    public enum AudioFadeState 
    {
        FADING_IN,
        FADING_OUT,
        IDLE
    }

    public class AudioFader: PolyBehaviour 
    {
        public AudioSource audioSource;

        private AudioFadeState state;
        
        public void FadeIn()
        {

        }

        public void FadeOut()
        {

        }

        private void Awake()
        {
            state = AudioFadeState.IDLE;
        }

        private void FixedUpdate()
        {
            if(state == AudioFadeState.FADING_IN)
            {
                // continue fade in until 1
            }
            else if(state == AudioFadeState.FADING_OUT)
            {
                // continue fade out until 0
            }
        }
    }
}