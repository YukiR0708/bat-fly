using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���C�t�����炷�A�C�e���̃R���|�[�l���g
/// </summary>
public class CrossController : ItemBase   // ItemBase2D ���p�����Ă���
{
    /// <summary>�_���[�W��</summary>
    [SerializeField] int _decreaseLife = 1;


    public override void Activate()
    {
        FindObjectOfType<GameManager>().DecreaseLife(_decreaseLife);
    }
}
