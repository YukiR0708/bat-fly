using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���C�t���񕜂���A�C�e���̃R���|�[�l���g
/// </summary>
public class HeartController : ItemBase   // ItemBase2D ���p�����Ă���
{
    /// <summary>�񕜐�</summary>
    [SerializeField] int _recoverLife = 1;


    public override void Activate()
    {
        FindObjectOfType<GameManager>().AddLife(_recoverLife);
    }
}
