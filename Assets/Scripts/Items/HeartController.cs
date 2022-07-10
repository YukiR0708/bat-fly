using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ライフを回復するアイテムのコンポーネント
/// </summary>
public class HeartController : ItemBase   // ItemBase2D を継承している
{
    /// <summary>回復数</summary>
    [SerializeField] int _recoverLife = 1;


    public override void Activate()
    {
        FindObjectOfType<GameManager>().AddLife(_recoverLife);
    }
}
