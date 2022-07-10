using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField, Header("TimeTextのオブジェクトをアサインする")] GameObject _timeText;
    [SerializeField] GameObject _player;
    [Tooltip("トータル制限時間")] float totalTime;
    [Header("制限時間（分）"), SerializeField] int _minute;
    [Header("制限時間（秒）"), SerializeField] float _seconds;
    [Tooltip("前回Update時の秒数")] float _oldSeconds;
    [SerializeField, Header("残り時間")] Text _timerText;
    private PlayerController _playerControllerScript;



    // Start is called before the first frame update
    void Start()
    {
        _playerControllerScript = _player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime = _minute * 60 + _seconds;  //　一旦インスペクターから受け取ったトータルの制限時間（秒）を計算；

        if (totalTime > 0f)
        {
            totalTime -= Time.deltaTime;   //カウントダウンする
        }

        if (_oldSeconds >= 0f && totalTime <= 0f || _playerControllerScript.life == 0)   //前のUpdateの残り時間が0以上かつ　今回のUpdateの残り時間が0未満のときだけ
        {
            SceneManager.LoadScene("ResultScene");
        }

        _minute = (int)totalTime / 60;   //　再設定
        _seconds = totalTime - _minute * 60;

        if ((int)_seconds != (int)_oldSeconds)    //　タイマー表示用UIテキストに残り時間を表示する
        {
            _timerText.text = $"Time:{_minute:0}:{(int)_seconds:00}";
        }

        _oldSeconds = _seconds;
    }
}

