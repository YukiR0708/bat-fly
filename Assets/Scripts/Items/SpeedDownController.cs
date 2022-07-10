using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクロール速度を下げるコンポーネント
/// </summary>
/// 
public class SpeedDownController : ItemBase   // ItemBase2D を継承している
{
    /// <summary>増加スピード</summary>
    [SerializeField] int _downSpeed = 500;


    public override void Activate()
    {
        FindObjectOfType<PlayerController>().SpeedDown(_downSpeed);
    }
}
