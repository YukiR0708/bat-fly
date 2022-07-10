using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRepeat : MonoBehaviour
{
    [Tooltip("���݂̍��W")] Vector3 _currentPosition;
    [Tooltip("�ǂ̂��炢���ꂽ�烊�s�[�g���邩")] float _repeatDistance;
    [Tooltip("�ړ���̍��W")] Vector3 _nextPosition;
    [SerializeField, Header("�v���C���[")] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = transform.position;
        _repeatDistance = GetComponent<BoxCollider>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // If background moves left by its repeat width, move it back to start position
        if (player.transform.position.x - _currentPosition.x > _repeatDistance)
        {
            _nextPosition = new Vector3(_currentPosition.x + 30f, _currentPosition.y, _currentPosition.z);
            transform.position = _nextPosition;
            _currentPosition = transform.position;
        }
    }

}

