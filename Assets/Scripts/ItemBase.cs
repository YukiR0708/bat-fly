using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムを制御する基底クラス
/// アイテムの共通機能を実装する
/// </summary>
/// 
public abstract class ItemBase : MonoBehaviour
{
    [Tooltip("アイテムの回転速度")] float _rotateSpeed = 100f;
    float _poppingPosition;
    float _poppingInterval;
    GameObject _player;
    [Tooltip("プレイヤーとアイテムの距離")] float _distance;
    [SerializeField, Tooltip("削除する距離")] float _deleteDistance;
    AudioSource _audioSource;
    [SerializeField, Tooltip("アイテム獲得時のSE")] AudioClip _sound = default;

    /// <summary>
    /// アイテムが発動する効果を実装(override)する
    /// </summary>
    public abstract void Activate();


    // Start is called before the first frame update
    void Start()
    {
        _poppingPosition = transform.position.y;
        _poppingInterval = Random.Range(0.5f, 3.0f);
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _distance = _player.transform.position.x - gameObject.transform.position.x; //プレイヤーのx座標 - アイテムのx座標

        if(_distance > _deleteDistance)
        {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, _poppingPosition + Mathf.PingPong(Time.time / _poppingInterval, 0.3f), transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _sound != null)
        {
            Activate();
            AudioSource.PlayClipAtPoint(_sound, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}

