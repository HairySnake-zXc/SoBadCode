using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Balls : MonoBehaviour
{
    [SerializeField] private GameObject _iceBall;
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private GameObject _effect;
    [SerializeField] private GameObject _alert;
    [SerializeField] private GameObject _iceAlert;
    [SerializeField] private Rigidbody2D rigidbody;
    private bool _canMoveRight = false;
    private bool _isFireball = false;
    private bool _canMove = false;
    private float _seconds;

    private void Start()
    {
        if (transform.position.x < 0)
        {
            _canMoveRight = true;
        }
        if (Random.Range(0, 2) == 1)
        {
            _fireBall.SetActive(true);
            _isFireball = true;
            transform.position += new Vector3(0, .25f, 0);
        }
        else
        {
            _iceBall.SetActive(true);
            transform.position += new Vector3(0, 0, 0);
        }
    }
    private void Update()
    {
        _seconds += Time.deltaTime;
        if (_seconds > .5f)
        {
            _alert.SetActive(false);
            _iceAlert.SetActive(false);
            _canMove = true;
        }

        if (_canMove)
        {
            if (_canMoveRight == true)
            {
                MovePlatform(4);
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
                MovePlatform(-4);

            }
            if (transform.position.x > 3 || transform.position.x < -3)
            {
                DestroyThis();
            }

        }

    }

    private void DestroyThis()
    {
        if (_isFireball == true)
        {
            _effect.GetComponent<ParticleSystem>().startColor = new Color(255, 61, 0);
        }
        else
        {
            _effect.GetComponent<ParticleSystem>().startColor = new Color(0, 250, 255);
        }
        Instantiate(_effect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void MovePlatform(float movespeed)
    {
        rigidbody.velocity = new Vector2(movespeed, 0);
    }
}
