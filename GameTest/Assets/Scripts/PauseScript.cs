using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _soundsOff;
    [SerializeField] private GameObject _soundsOn;
    private bool _isPaused = false;
    private static bool _isSounds;
    private int _count;
    private void Start()
    {
        PlayerPrefs.SetInt("firstStart", 1);
        _isSounds = Convert.ToBoolean(PlayerPrefs.GetString("sounds"));
        _soundsOn.SetActive(_isSounds);
        _soundsOff.SetActive(!_isSounds);

    }
    private void Update()
    {

        _count++;
        if (_count < 2)
        {
            AudioListener.pause = !_isSounds;

        }
    }

    public void PauseController()
    {
        if (_isPaused)
        {
            _isPaused = !_isPaused;
            _pause.SetActive(false);
            Time.timeScale = 1f;


        }
        else
        {
            _isPaused = !_isPaused;
            _pause.SetActive(true);
            Time.timeScale = 0f;

        }
    }
   
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void SoundsController()
    {

        if (_isSounds)
        {
            _isSounds = !_isSounds;
            AudioListener.pause = true;
            PlayerPrefs.SetString("sounds", "false");
            _soundsOn.SetActive(false);
            _soundsOff.SetActive(true);
        }
        else
        {
            _isSounds = !_isSounds;
            AudioListener.pause = false;
            PlayerPrefs.SetString("sounds", "true");
            _soundsOff.SetActive(false);
            _soundsOn.SetActive(true);
        }
        PlayerPrefs.Save();
    }
}