using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{

    private float _maxHeight = -1f;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        MoveCamera();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "notplatform")
        {
            collision.GetComponent<MovingPlatform>().DestroyObj();
        }
    }
    void MoveCamera()
    {
            if (_player.transform.position.y > _maxHeight)
            {
                transform.position = _player.transform.position + _offset;
                _maxHeight = _player.transform.position.y;
            }
    }
}
