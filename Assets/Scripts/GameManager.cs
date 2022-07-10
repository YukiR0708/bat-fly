using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _timeText;
    [Tooltip("トータル制限時間")] float _totalTime;
    [Header("制限時間（分）"), SerializeField] int _minute;
    [Header("制限時間（秒）"), SerializeField] float _seconds;
    [Tooltip("前回Update時の秒数")] float _oldSeconds;
    [SerializeField, Header("残り時間")] Text _timerText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _totalTime = _minute * 60 + _seconds;  //　一旦インスペクターから受け取ったトータルの制限時間（秒）を計算；

        if (_totalTime > 0f)
        {
            _totalTime -= Time.deltaTime;   //カウントダウンする
        }

        if (_oldSeconds >= 0f && _totalTime <= 0f)   //前のUpdateの残り時間が0以上かつ　今回のUpdateの残り時間が0未満のときだけ
        {
            SceneManager.LoadScene("ResultScene");
        }

        _minute = (int)_totalTime / 60;   //　再設定
        _seconds = _totalTime - _minute * 60;

        if ((int)_seconds != (int)_oldSeconds)    //　タイマー表示用UIテキストに残り時間を表示する
        {
            _timerText.text = $"Time:{_minute:0}:{(int)_seconds:00}";
        }

        _oldSeconds = _seconds;
    }
}

