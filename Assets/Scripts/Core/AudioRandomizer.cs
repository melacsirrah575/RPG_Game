using UnityEngine;

namespace RPG.Core
{
    public class AudioRandomizer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] audioClips = null;

        private AudioSource audioSource = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomAudioClip()
        {
            if (audioClips != null)
            {
                int index = Random.Range(0, audioClips.Length);
                var randomClip = audioClips[index];
                audioSource.PlayOneShot(randomClip);
            }
        }
    }
}