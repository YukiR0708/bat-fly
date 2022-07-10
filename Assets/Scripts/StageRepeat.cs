using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRepeat : MonoBehaviour
{
    Vector3 _startPos;
    float _repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // If background moves left by its repeat width, move it back to start position
        if (transform.position.x < _startPos.x - _repeatWidth)
        {
            transform.position = _startPos;
        }
    }

}

