using UnityEngine;

namespace Player
{
    public class AnimController : MonoBehaviour
    {
        [SerializeField]private Animator _animator;

        private void Update()
        {
            bool isWalking = GameManager.Instance.GameIsOn;
            _animator.SetBool("walking", isWalking);
            _animator.SetBool("idle", !isWalking);
        }
    }
}