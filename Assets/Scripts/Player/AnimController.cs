using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimController : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            bool isWalking = GameManager.Instance.GameIsOn;
            _animator.SetBool("walking", isWalking);
            _animator.SetBool("idle", !isWalking);

        }
    }
}