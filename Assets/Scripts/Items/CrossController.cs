using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ライフを減らすアイテムのコンポーネント
/// </summary>
public class CrossController : ItemBase   // ItemBase2D を継承している
{
    /// <summary>ダメージ数</summary>
    [SerializeField] int _decreaseLife = 1;


    public override void Activate()
    {
        FindObjectOfType<GameManager>().DecreaseLife(_decreaseLife);
    }
}
