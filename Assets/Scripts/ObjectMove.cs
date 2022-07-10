using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [Tooltip("�A�C�e���̉�]���x")] float _rotateSpeed = 100f;
    float _poppingPosition;
    float _poppingInterval;
    GameObject _player;
    [Tooltip("�v���C���[�ƃA�C�e���̋���")] float _distance;
    [SerializeField, Header("�폜���鋗��")] float _deleteDistance;

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
}

