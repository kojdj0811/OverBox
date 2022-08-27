using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Obstacle : MonoBehaviour
{
    public abstract void OnHit(Collision collision);

    /// <summary>
    /// 플레이어가 상호작용키를 눌렀을 때 호출됩니다.
    /// </summary>
    public abstract void OnInteractive(Hyerin.Player pl);
}
