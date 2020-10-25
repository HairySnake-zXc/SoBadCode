using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    public float _moveSpeed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (gameObject.transform.position.x > 4)
        {
            _moveSpeed = -4;
        }
        if (gameObject.transform.position.x < -4)
        {
            _moveSpeed = 4;
        }
        Moving();
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
    private void Moving()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed, 0);
    }
}
