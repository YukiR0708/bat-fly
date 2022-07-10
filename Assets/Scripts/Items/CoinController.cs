using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 得点のためのコインアイテムのコンポーネント
/// </summary>
public class CoinController : ItemBase   // ItemBase2D を継承している
{
    /// <summary>取った時に加点する値</summary>
    [SerializeField] int _addScore = 100;

    public override void Activate()
    {
        FindObjectOfType<GameManager>().AddScore(_addScore);
    }
}

