using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�N���[�����x���グ��R���|�[�l���g
/// </summary>
/// 
public class SpeedUpController : ItemBase   // ItemBase2D ���p�����Ă���
{
    /// <summary>�����X�s�[�h</summary>
    [SerializeField] int _upSpeed = 500;


    public override void Activate()
    {
        FindObjectOfType<PlayerController>().SpeedUp(_upSpeed);
    }
}