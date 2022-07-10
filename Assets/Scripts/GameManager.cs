using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _timeText;
    [Tooltip("�g�[�^����������")] float _totalTime;
    [Header("�������ԁi���j"), SerializeField] int _minute;
    [Header("�������ԁi�b�j"), SerializeField] float _seconds;
    [Tooltip("�O��Update���̕b��")] float _oldSeconds;
    [SerializeField, Header("�c�莞��")] Text _timerText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _totalTime = _minute * 60 + _seconds;  //�@��U�C���X�y�N�^�[����󂯎�����g�[�^���̐������ԁi�b�j���v�Z�G

        if (_totalTime > 0f)
        {
            _totalTime -= Time.deltaTime;   //�J�E���g�_�E������
        }

        if (_oldSeconds >= 0f && _totalTime <= 0f)   //�O��Update�̎c�莞�Ԃ�0�ȏォ�@�����Update�̎c�莞�Ԃ�0�����̂Ƃ�����
        {
            SceneManager.LoadScene("ResultScene");
        }

        _minute = (int)_totalTime / 60;   //�@�Đݒ�
        _seconds = _totalTime - _minute * 60;

        if ((int)_seconds != (int)_oldSeconds)    //�@�^�C�}�[�\���pUI�e�L�X�g�Ɏc�莞�Ԃ�\������
        {
            _timerText.text = $"Time:{_minute:0}:{(int)_seconds:00}";
        }

        _oldSeconds = _seconds;
    }
}

