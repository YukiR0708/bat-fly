using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�N���[�����x��������R���|�[�l���g
/// </summary>
/// 
public class SpeedDownController : ItemBase   // ItemBase2D ���p�����Ă���
{
    /// <summary>�����X�s�[�h</summary>
    [SerializeField] int _downSpeed = 500;


    public override void Activate()
    {
        FindObjectOfType<PlayerController>().SpeedDown(_downSpeed);
    }
}
