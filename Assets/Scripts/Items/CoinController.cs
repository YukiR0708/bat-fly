using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���_�̂��߂̃R�C���A�C�e���̃R���|�[�l���g
/// </summary>
public class CoinController : ItemBase   // ItemBase2D ���p�����Ă���
{
    /// <summary>��������ɉ��_����l</summary>
    [SerializeField] int _addScore = 100;

    public override void Activate()
    {
        FindObjectOfType<GameManager>().AddScore(_addScore);
    }
}

