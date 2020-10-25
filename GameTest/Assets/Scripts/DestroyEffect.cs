using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    private float _second;
    [SerializeField] private GameObject _particle;

   private void Update()
    {
        _second += Time.deltaTime;
        if(_second >= 0.5f)
        {
            Instantiate(_particle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
