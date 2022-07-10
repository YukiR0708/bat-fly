using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField, Tooltip("アイテムの回転速度")]
    float _rotateSpeed;
    float _poppingPosition;
    float _poppingInterval;

    // Start is called before the first frame update
    void Start()
    {
        _poppingPosition = transform.position.y;
        _poppingInterval = Random.Range(0.5f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, _poppingPosition + Mathf.PingPong(Time.time / _poppingInterval, 0.3f), transform.position.z);
    }
}

