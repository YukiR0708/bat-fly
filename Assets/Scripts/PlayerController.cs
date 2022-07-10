using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _playerRb;
    [SerializeField, Header("上下移動速度")] float _moveForceV;
    [Tooltip("ゲーム中かどうか")] bool isGame;
    [Tooltip("プレイヤーのy座標が十分に小さいかどうか")] bool isLowEnough;
    [Tooltip("プレイヤーのy座標")] float _playePositionY;
    [SerializeField, Header("プレイヤーのy軸移動上限")] float _upLimit;
    [SerializeField, Header("強制スクロール速度")] float _moveForceR;
    [SerializeField, Header("メインカメラ")] GameObject _mainCamera;
    [Tooltip("プレイヤーより手前に配置する")] float _zAdjust = -3.0f;  //Z軸調整
    [Tooltip("カメラのY座標")] float _yPosition = 1.8f;  //Z軸調整
    float _xAdjust = 2.0f;  //X軸調整
    //[SerializeField, Header("コイン獲得時のエフェクト")] ParticleSystem _coinParticle;
    //[SerializeField, Header("ハート獲得時のエフェクト")] ParticleSystem _heartParticle;
    private AudioSource _audioSource;
    [SerializeField, Header("コイン獲得時のSE")] AudioClip _coinSE;
    [SerializeField, Header("ハート獲得時のSE")] AudioClip _heartSE;
    [SerializeField, Header("地面衝突時時のSE")] AudioClip _boundSE;
    [SerializeField, Header("地面衝突時の跳ね返り")] float _bound;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = gameObject.GetComponent<Rigidbody>();
        isGame = true;
        isLowEnough = true;
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _playePositionY = GetComponent<Transform>().position.y;

        if (_playePositionY < _upLimit && isGame)
        {
            isLowEnough = true;

            _playerRb.AddForce(Vector3.right * _moveForceR);
            _mainCamera.transform.position = new Vector3(transform.position.x + _xAdjust, _yPosition, transform.position.z + _zAdjust);

            if (isLowEnough && Input.GetKey(KeyCode.W))
            {
                _playerRb.velocity = Vector3.up * _moveForceV;
                Debug.Log("Wが押された");
            }
            else if (isLowEnough && Input.GetKey(KeyCode.S))
            {
                _playerRb.velocity = Vector3.down * _moveForceV;
                Debug.Log("Sが押された");
            }
        }
        else
        {
            isLowEnough = false;
        }
        isLowEnough = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            //            _coinParticle.Play();
            _audioSource.PlayOneShot(_coinSE, 1.0f);
            isGame = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Heart"))
        {
            //            _heartParticle.Play();
            _audioSource.PlayOneShot(_heartSE, 1.0f);
            Destroy(other.gameObject);
        }
        // if player collides with ground, bound
        else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("床に当たった");
            _audioSource.PlayOneShot(_boundSE, 1.0f);
            _playerRb.AddForce(0, _bound, 0, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Ceiling"))
        {
            Debug.Log("床に当たった");
            _audioSource.PlayOneShot(_boundSE, 1.0f);
            _playerRb.AddForce(0, -_bound, 0, ForceMode.Impulse);


        }
    }
}
