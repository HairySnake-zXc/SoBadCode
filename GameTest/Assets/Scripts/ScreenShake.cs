using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{


    private float _finalPosition = 0;
    private bool _check = true;

    private void Update()
    {
        if (_check)
        {
            if (gameObject.transform.position.y < _finalPosition)
            {
                Camera.main.GetComponent<Animator>().SetTrigger("ScreenShake");
                _check = false;
            }
        }
    }

    public void GetPosition(float pos)
    {
        _finalPosition = pos;
    }
}
