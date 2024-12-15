using System;
using UnityEngine;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip collectCoins;
        [SerializeField] private AudioClip loseCoins;
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip fail;
        [SerializeField] private AudioClip win;

        [SerializeField] private AudioClip[] footsteps;
        private float timer = 0f;

        [NonSerialized] public static PlayerAudio Instance;
        private AudioSource _source;

        private void Start()
        {
            Instance = this;
            _source = GetComponentInChildren<AudioSource>();
        }

        private void Update()
    {
        if (GameManager.Instance.GameIsOn)
        {
            timer += Time.deltaTime;

            if (timer >= 0.5f)
            {
                timer = 0;
                int index = UnityEngine.Random.Range(0, footsteps.Length);
                _source.PlayOneShot(footsteps[index]);
            }
        }
        else
        {
            timer = 0;
        }
    }



        public void CollectCoins() => _source.PlayOneShot(collectCoins);
        public void LoseCoins() => _source.PlayOneShot(loseCoins);
        public void Click() => _source.PlayOneShot(click);
        public void Fail() => _source.PlayOneShot(fail);
        public void Win() => _source.PlayOneShot(win);
        public void Play(AudioClip clip) => _source.PlayOneShot(clip);
    }
}