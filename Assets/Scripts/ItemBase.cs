using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���𐧌䂷����N���X
/// �A�C�e���̋��ʋ@�\����������
/// </summary>
/// 
public abstract class ItemBase : MonoBehaviour
{
    [Tooltip("�A�C�e���̉�]���x")] float _rotateSpeed = 100f;
    float _poppingPosition;
    float _poppingInterval;
    GameObject _player;
    [Tooltip("�v���C���[�ƃA�C�e���̋���")] float _distance;
    [SerializeField, Tooltip("�폜���鋗��")] float _deleteDistance;
    AudioSource _audioSource;
    [SerializeField, Tooltip("�A�C�e���l������SE")] AudioClip _sound = default;

    /// <summary>
    /// �A�C�e��������������ʂ�����(override)����
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
        _distance = _player.transform.position.x - gameObject.transform.position.x; //�v���C���[��x���W - �A�C�e����x���W

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

