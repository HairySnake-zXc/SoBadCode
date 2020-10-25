using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    [SerializeField] private GameObject[] _laser;
    [SerializeField] private float _seconds;
    public int CountFix;
    private void Start()
    {
        if (CountFix < 213)
        {
            _laser[0].SetActive(true);
        }
        if (CountFix >= 213 && CountFix < 246)
        {
            _laser[1].SetActive(true);

        }
        if (CountFix >= 246 && CountFix < 280)
        {
            _laser[2].SetActive(true);

        }
        if (CountFix >= 280)
        {
            _laser[3].SetActive(true);

        }
    }
        private void Update()
    {
        _seconds += Time.deltaTime;
        if(_seconds >= 6f)
        {
            _laser[0].SetActive(false);
            _laser[1].SetActive(false);
            _laser[2].SetActive(false);
            _laser[3].SetActive(false);
            Destroy(gameObject);
        }
    }
}
