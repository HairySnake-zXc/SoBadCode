using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _tracks;
    private int _index;
    private bool _isChanged = false;
    public void OnLocationChange()
    {
        _isChanged = true;
    }
    private void Update()
    {
        if (_isChanged)
        {
            _tracks[_index].GetComponent<AudioSource>().volume -= .005f;
            if (_tracks[_index].GetComponent<AudioSource>().volume <= 0)
            {
                ChangeMusic();
                _isChanged = false;
            }
        }
    }
    private void ChangeMusic()
    {
        _tracks[_index].SetActive(false);
        _index += 1;
        _tracks[_index].SetActive(true);
    }
}
