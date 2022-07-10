using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField, Header("TimeText�̃I�u�W�F�N�g���A�T�C������")] GameObject _timeText;
    [SerializeField] GameObject _player;
    [Tooltip("�g�[�^����������")] float totalTime;
    [Header("�������ԁi���j"), SerializeField] int _minute;
    [Header("�������ԁi�b�j"), SerializeField] float _seconds;
    [Tooltip("�O��Update���̕b��")] float _oldSeconds;
    [SerializeField, Header("�c�莞��")] Text _timerText;
    private PlayerController _playerControllerScript;



    // Start is called before the first frame update
    void Start()
    {
        _playerControllerScript = _player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime = _minute * 60 + _seconds;  //�@��U�C���X�y�N�^�[����󂯎�����g�[�^���̐������ԁi�b�j���v�Z�G

        if (totalTime > 0f)
        {
            totalTime -= Time.deltaTime;   //�J�E���g�_�E������
        }

        if (_oldSeconds >= 0f && totalTime <= 0f || _playerControllerScript.life == 0)   //�O��Update�̎c�莞�Ԃ�0�ȏォ�@�����Update�̎c�莞�Ԃ�0�����̂Ƃ�����
        {
            SceneManager.LoadScene("ResultScene");
        }

        _minute = (int)totalTime / 60;   //�@�Đݒ�
        _seconds = totalTime - _minute * 60;

        if ((int)_seconds != (int)_oldSeconds)    //�@�^�C�}�[�\���pUI�e�L�X�g�Ɏc�莞�Ԃ�\������
        {
            _timerText.text = $"Time:{_minute:0}:{(int)_seconds:00}";
        }

        _oldSeconds = _seconds;
    }
}

