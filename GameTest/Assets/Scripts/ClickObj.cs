using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ClickObj : MonoBehaviour
{
    private bool _move;
    private Vector2[] _dir1 = new Vector2[2];
    private Vector2 _dir;


    private void Start()
    {
        _dir1[0] = new Vector2(2, -2);
        _dir1[1] = new Vector2(-2, -2);
    }

    private void Update()
    {
        if (!_move) return;
        transform.Translate(_dir);
    }
    public void StartMotion(int scoreIncrease)
    {
        transform.localPosition = Vector2.zero;
        GetComponent<Text>().text = "+" + scoreIncrease;
        _dir = _dir1[Random.Range(0,2)];
        _move = true;
        GetComponent<Animation>().Play();
    }
}
