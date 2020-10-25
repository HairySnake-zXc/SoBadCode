using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{

    private float _seconds;

    private void Update()
    {
        _seconds += Time.deltaTime;
        if(_seconds >= 1)
        {
            Destroy(gameObject);
        }
    }
}
