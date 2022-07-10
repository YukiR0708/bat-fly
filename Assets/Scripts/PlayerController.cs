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
    //[SerializeField, Header("コイン獲得時のエフェクト")] ParticleSystem _coinParticle;
    //[SerializeField, Header("ハート獲得時のエフェクト")] ParticleSystem _heartParticle;
    private AudioSource _audioSource;
    [SerializeField, Header("コイン獲得時のSE")] AudioClip _coinSE;
    [SerializeField, Header("ハート獲得時のSE")] AudioClip _heartSE;
    [SerializeField, Header("地面衝突時時のSE")] AudioClip _boundSE;
    [SerializeField, Header("地面衝突時の跳ね返り")] float _bound;
    [SerializeField, Header("ScoreTextのオブジェクトをアサインする")] GameObject _scoreText;
    [SerializeField, Header("LifeTextのオブジェクトをアサインする")] GameObject _lifeText;
    [Tooltip("前回Update時の秒数")] int _score = 0;
    [SerializeField, Header("速度上昇獲得時のSE")] AudioClip _speedUpSE;
    [SerializeField, Header("速度下降獲得時のSE")] AudioClip _speedDownSE;
    [SerializeField, Header("現在の残基")] public int life;
    [Header("残基の上限")] int _maxLife = 3;
    [SerializeField, Header("十字架衝突時のSE")] AudioClip _crossSE;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = gameObject.GetComponent<Rigidbody>();
        isGame = true;
        isLowEnough = true;
        _audioSource = gameObject.GetComponent<AudioSource>();
        life = _maxLife;
        SetLifeText(life);
    }

    private void SetLifeText(int life)
    {
        string lifeHeart = ""; //残基のstring型♥テキスト

        if(life == 1)
        {
            lifeHeart = "♥";
        }
        else if(life == 2)
        {
            lifeHeart = "♥♥";
        }
        else if(life == 3)
        {
            lifeHeart = "♥♥♥";
        }

        _lifeText.GetComponent<Text>().text = $"Life:{lifeHeart}";
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
        _scoreText.GetComponent<Text>().text = "Score:" + _score.ToString("");
    }

    private void AddScore()
    {
        _score += 100;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            //_coinParticle.Play();
            _audioSource.PlayOneShot(_coinSE, 2.0f);
            AddScore();
            isGame = true;
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Heart"))
        {
            //_heartParticle.Play();
            _audioSource.PlayOneShot(_heartSE, 1.0f);
            Destroy(other.gameObject);

            if (life < _maxLife)
            {
                life++;
                SetLifeText(life);
            }
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
            Debug.Log("天井に当たった");
            _audioSource.PlayOneShot(_boundSE, 1.0f);
            _playerRb.AddForce(0, -_bound, 0, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("SpeedUp"))
        {
            _audioSource.PlayOneShot(_speedUpSE, 1.0f);
            Destroy(other.gameObject);
            _moveForceR += 500;
        }

        else if (other.gameObject.CompareTag("SpeedDown"))
        {
            _audioSource.PlayOneShot(_speedDownSE, 2.0f);
            Destroy(other.gameObject);
            _moveForceR -= 500;
        }
        else if (other.gameObject.CompareTag("Cross")　&& life > 0)
        {
            _audioSource.PlayOneShot(_crossSE, 1.0f);
            Destroy(other.gameObject);
            life--;
            SetLifeText(life);
        }
    }
}
