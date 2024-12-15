using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] protected List<AudioClip> _sounds;
    protected  AudioSource _audioSource;

    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void PlayRandomSound()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        if (_sounds.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, _sounds.Count);
        AudioClip randomSound = _sounds[randomIndex];

        _audioSource.clip = randomSound;
        _audioSource.Play();
    }
}
