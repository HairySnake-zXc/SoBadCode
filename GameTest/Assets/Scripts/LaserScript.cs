using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _audioLaser;
    private bool _isStarted = true;
    private void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Activate"))
        {

            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            if(_isStarted)
            {
                _audioLaser.Play();
                _isStarted = false;
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}
