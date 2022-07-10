using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクロール速度を上げるコンポーネント
/// </summary>
/// 
public class SpeedUpController : ItemBase   // ItemBase2D を継承している
{
    /// <summary>増加スピード</summary>
    [SerializeField] int _upSpeed = 500;


    public override void Activate()
    {
        FindObjectOfType<PlayerController>().SpeedUp(_upSpeed);
    }
}
