using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Threading;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private ClickObj[] TextPool = new ClickObj[4];
    [SerializeField] private GameObject[] Platforms;
    [SerializeField] private SoundSwitcher _soundSwitcher;
    [SerializeField] private GameObject ClickTextPrefab;
    [SerializeField] private GameObject ClickParent;
    [SerializeField] private GameObject FinalPlatform;
    [SerializeField] private GameObject Success;
    [SerializeField] private GameObject laser;
    [SerializeField] private CamMovement camera;
    [SerializeField] private GameObject Jump;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private Text Text;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _currentScore;
    [SerializeField] private Animator Anim;
    [SerializeField] private int PlatformCount = 0;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private AudioSource _audioJump;
    [SerializeField] private AudioSource _audioDeath;

    public GameObject Ball;
    
    private int[] _spawnX = { 3, -3 };
    private int _count, _index, _chanceRandom;
    private int _randomCount;
    private int _clickNum = 0;
    private int _platformCooldown;
    private int _bestScore = 0;
    private float _f, _posX;
    private bool _timer;
    private bool _timer1;
    private float _seconds;
    private float _seconds1;
    private float _spawnY = -2.75f;
    private float _time;
    private float _groundRadius = 0.5f;
    private double _score, _totalScore = -100;
    private bool _canJump = false;
    private bool _isGrounded = false;
    private bool _isClicked = false;
    private bool _deathCheck = true;
    public bool CanSpawnPlatform = false;
    private Vector2 _pos;
    private Vector2 _antiJump;
    private Rigidbody2D _rigidbody;
    public void JumpClick()
    {
        if (_isGrounded)
        {
            if (_canJump)
            {
                _audioJump.Play();
                _rigidbody.AddForce(_antiJump);
                _canJump = false;
                _isClicked = false;
            }
        }
    }
    private void Start()
    {
        AudioListener.pause = false;
        _randomCount = UnityEngine.Random.Range(10, 21);
        _antiJump = new Vector2(0, 450);
        _rigidbody = GetComponent<Rigidbody2D>();
        for (int i = 0; i < TextPool.Length; i++)
        {
            TextPool[i] = Instantiate(ClickTextPrefab, ClickParent.transform).GetComponent<ClickObj>();
        }
        _randomCount = UnityEngine.Random.Range(10, 21);
    }
    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, WhatIsGround);
        //anim.SetBool("Ground", isGrounded);
        //anim.SetFloat("vSpeed", rigidBody.velocity.y);
        if (!_isGrounded)
            return;
    }

    private void Update()
    {
        if(_timer1)
        {
            _seconds1 += Time.deltaTime;
            if (_seconds1 > 2f)
            {
                _deathScreen.SetActive(true);
                if (_totalScore > PlayerPrefs.GetInt("bestScore"))
                { 
                    PlayerPrefs.SetInt("bestScore", Convert.ToInt32(_totalScore));
                }
                _highScore.text = PlayerPrefs.GetInt("bestScore") + "";
                _currentScore.text = _totalScore + "";
            }
        }
        if(_timer)
        {
            _seconds += Time.deltaTime;
            if(_seconds > 1f)
            {
                Instantiate(FinalPlatform, new Vector2(0, _spawnY + 10f), Quaternion.identity);
                _timer = false;
            }
        }
        
        ScoreCalculation(100, _posX);

        JumpingAnimation(_isGrounded);
        if (CanSpawnPlatform)
        {
            _time += Time.deltaTime;
            if (_time >= 1.8f)
            {
                SpawnPlatform(Platforms[_index]);
                _time = 0f;
                CanSpawnPlatform = false;
            }


        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (_deathCheck)
        {
            _canJump = true;
            if (other.tag == "platform")
            {
                switch(_count)
                {
                    case 64:
                    case 145:
                        _soundSwitcher.OnLocationChange();
                break;
                }
                platformCalc(_count);
                _posX = other.transform.position.x;
                ScoreCalculation(100, _posX);
                _score = ((Math.Round(_f / 10) * 10) - 50) * 2;
                if (_score == 100f)
                {
                    if (_count >= 1)
                        Instantiate(Success, other.transform.position, Quaternion.identity);
                }
                _totalScore += _score;
                TextPool[_clickNum].StartMotion((int)_score);
                _clickNum = _clickNum == TextPool.Length - 1 ? 0 : _clickNum + 1;
                Text.text = "" + _totalScore;


                if (_platformCooldown == _randomCount)

                {
                    //178
                    if (_count < 178)
                    {
                        SpawnBall();
                    }
                    else
                    {
                        SpawnLaser();
                    }

                    _randomCount = UnityEngine.Random.Range(10, 21);
                    _platformCooldown = 0;

                }
                else
                {
                    if (_count < 290)
                        SpawnPlatform(Platforms[_index]);
                    else 
                        Finish();
                }
                other.tag = "notplatform";
                other.GetComponent<MovingPlatform>()._moveSpeed = 0f;
            }
            if (other.tag == "death")
            {
                _soundSwitcher.gameObject.SetActive(false);
                _audioDeath.Play();
                Jump.SetActive(false);
                Anim.Play("Death");
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(_antiJump * 1.4f);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                _deathCheck = false;
                _timer1 = true;
                camera.enabled = false;
            }
        }
    }
    private void SpawnPlatform(GameObject platform)
    {
        _platformCooldown += 1;
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            _pos = new Vector2(-4f, _spawnY);
            Instantiate(platform, _pos, Quaternion.identity);
            platform.GetComponent<MovingPlatform>()._moveSpeed = 4;

        }
        else
        {
            _pos = new Vector2(4f, _spawnY);
            Instantiate(platform, _pos, Quaternion.identity);
            platform.GetComponent<MovingPlatform>()._moveSpeed = -4;

        }
        _spawnY += .31f;
        _count += 1;
    }
    private void SpawnBall()
    {
        Instantiate(Ball, new Vector2(_spawnX[UnityEngine.Random.Range(0, 2)], _spawnY), Quaternion.identity);
        CanSpawnPlatform = true;
    }
    private void SpawnLaser()
    {
        laser.GetComponent<Lasers>().CountFix = _count;
        Instantiate(laser, new Vector2(0, _spawnY + .31f), Quaternion.identity);

        CanSpawnPlatform = true;
    }
    private void ScoreCalculation(float k, float x)
    {
        _f = k / (Mathf.Abs(x) + 1);
        //f=sign(b)*a/(abs(b)+ex)
    }
    private void platformCalc(int a)
    {
        if (a < 64)
        {
            _index = UnityEngine.Random.Range(0, 2);
        }
        if (a >= 64 && a < 145)
        {
            _chanceRandom = UnityEngine.Random.Range(2, 101);
            if (_chanceRandom <= 100 & _chanceRandom > 90)
            {
                _index = 5;
            }
            if (_chanceRandom <= 90 & _chanceRandom > 50)
            {
                _index = 4;
            }
            if (_chanceRandom <= 50 & _chanceRandom > 10)
            {
                _index = 3;
            }
            if (_chanceRandom <= 10 & _chanceRandom > 0)
            {
                _index = 2;
            }
        }
        if (a >= 145 && a < 178)
        {
            _index = UnityEngine.Random.Range(6, 10);
        }
        if (a >= 178 && a < 213)
        {
            _index = UnityEngine.Random.Range(7, 11);
        }
        if (a >= 213 && a < 246)
        {
            _index = UnityEngine.Random.Range(8, 12);
        }
        if (a >= 246 && a < 280)
        {
            _index = UnityEngine.Random.Range(9, 13);
        }
        if (a >= 280)
        {
            _index = UnityEngine.Random.Range(10, 14);
        }
        PlatformCount++;
    }
    private void JumpingAnimation(bool IsOnGround)
    {
        if (IsOnGround)
        {
            Anim.SetBool("isJumping", false);
        }
        else
        {
            Anim.SetBool("isJumping", true);
        }
        //if (IsOnGround && Input.GetKeyDown(KeyCode.Space))


    }
    private void Finish()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("ScreenShake");
        float positionY = gameObject.transform.position.y;
        camera.enabled = false;

        _rigidbody.AddForce(new Vector2(0, .15f));
        _rigidbody.mass = 0;
        _rigidbody.gravityScale = 0;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _deathCheck = false;
        _timer = true;

        FinalPlatform.GetComponent<ScreenShake>().GetPosition(positionY);
    }



}
