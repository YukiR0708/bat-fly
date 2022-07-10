using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    Rigidbody _playerRb;
    [SerializeField, Header("上下移動速度")] float _moveForceV;
    [Tooltip("ゲーム中かどうか")] public bool isGame;
    [Tooltip("プレイヤーのy座標が十分に小さいかどうか")] bool isLowEnough;
    [Tooltip("プレイヤーのy座標")] float _playePositionY;
    [SerializeField, Header("プレイヤーのy軸移動上限")] float _upLimit;
    [SerializeField, Header("強制スクロール速度")] float _moveForceR;
    [SerializeField, Header("メインカメラ")] GameObject _mainCamera;
    [Tooltip("プレイヤーより手前に配置する")] float _zAdjust = -3.0f;  //Z軸調整
    [Tooltip("カメラのY座標")] float _yPosition = 1.8f;  //Z軸調整
    float _xAdjust = 2.0f;  //X軸調整
    private AudioSource _audioSource;
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
            _playerRb.AddForce(Vector3.right * _moveForceR * Time.deltaTime);
            _mainCamera.transform.position = new Vector3(transform.position.x + _xAdjust, _yPosition, transform.position.z + _zAdjust);

            if (isLowEnough && Input.GetKey(KeyCode.W))
            {
                _playerRb.velocity = Vector3.up * _moveForceV;
            }
            else if (isLowEnough && Input.GetKey(KeyCode.S))
            {
                _playerRb.velocity = Vector3.down * _moveForceV;
            }
        }
        else
        {
            isLowEnough = false;
        }
        isLowEnough = true;
    }

    /// <summary>
    /// スクロール速度を増加させるメソッド
    /// </summary>
    public void SpeedUp(float upSpeed)
    {
        _moveForceR += upSpeed;
    }

    /// <summary>
    /// スクロール速度を減少させるメソッド
    /// </summary>

    public void SpeedDown(float downSpeed)
    {
        _moveForceR += downSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with ground, bound
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("床に当たった");
            _audioSource.PlayOneShot(_boundSE, 1.0f);
            _playerRb.AddForce(0, _bound, 0, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Ceiling"))
        {
            Debug.Log("天井に当たった");
            _audioSource.PlayOneShot(_boundSE, 1.0f);
            _playerRb.AddForce(0, -_bound, 0, ForceMode.Impulse);
        }
    }
}
