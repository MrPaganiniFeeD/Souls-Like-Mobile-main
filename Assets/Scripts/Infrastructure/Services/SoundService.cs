using System.Collections.Generic;
using Infrastructure.AssetsManagement;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        private IAssets _assets;
        private AudioSource _audioSource;

        [SerializeField] private List<AudioClip> _musics;

        private void Awake() => 
            _audioSource = GetComponent<AudioSource>();

        public void EnableMusic()
        {
            _audioSource.loop = true;
            _audioSource.volume = 0.6f;
            _audioSource.clip = _musics[Random.Range(0, _musics.Count - 1)];
            _audioSource.Play();
        }
    }
}