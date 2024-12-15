using UnityEngine;

namespace Player
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _greens;
        [SerializeField] private ParticleSystem[] _reds;

        public void PlayParticles(int id)
        {
            if (id == 0)
            {
                for (int i = 0; i < _greens.Length; i++)
                {
                    _greens[i].Play();
                }
            }
            else
            {
                for (int i = 0; i < _reds.Length; i++)
                {
                    _reds[i].Play();
                }
            }
        }
    }
}
