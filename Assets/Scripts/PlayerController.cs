using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    Rigidbody _playerRb;
    [SerializeField, Header("�㉺�ړ����x")] float _moveForceV;
    [Tooltip("�Q�[�������ǂ���")] public bool isGame;
    [Tooltip("�v���C���[��y���W���\���ɏ��������ǂ���")] bool isLowEnough;
    [Tooltip("�v���C���[��y���W")] float _playePositionY;
    [SerializeField, Header("�v���C���[��y���ړ����")] float _upLimit;
    [SerializeField, Header("�����X�N���[�����x")] float _moveForceR;
    [SerializeField, Header("���C���J����")] GameObject _mainCamera;
    [Tooltip("�v���C���[����O�ɔz�u����")] float _zAdjust = -3.0f;  //Z������
    [Tooltip("�J������Y���W")] float _yPosition = 1.8f;  //Z������
    float _xAdjust = 2.0f;  //X������
    //[SerializeField, Header("�R�C���l�����̃G�t�F�N�g")] ParticleSystem _coinParticle;
    //[SerializeField, Header("�n�[�g�l�����̃G�t�F�N�g")] ParticleSystem _heartParticle;
    private AudioSource _audioSource;
    [SerializeField, Header("�R�C���l������SE")] AudioClip _coinSE;
    [SerializeField, Header("�n�[�g�l������SE")] AudioClip _heartSE;
    [SerializeField, Header("�n�ʏՓˎ�����SE")] AudioClip _boundSE;
    [SerializeField, Header("�n�ʏՓˎ��̒��˕Ԃ�")] float _bound;
    [SerializeField] GameObject _scoreText;
    [Tooltip("�O��Update���̕b��")] int _score = 0;
    [SerializeField, Header("���x�㏸�l������SE")] AudioClip _speedUpSE;
    [SerializeField, Header("���x���~�l������SE")] AudioClip _speedDownSE;

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
        }
        // if player collides with ground, bound
        else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("���ɓ�������");
            _audioSource.PlayOneShot(_boundSE, 1.0f);
            _playerRb.AddForce(0, _bound, 0, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Ceiling"))
        {
            Debug.Log("�V��ɓ�������");
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
    }
}
