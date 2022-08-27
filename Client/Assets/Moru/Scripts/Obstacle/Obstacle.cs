using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Obstacle : MonoBehaviour
{
    public abstract void OnHit(Collision collision);

    /// <summary>
    /// �÷��̾ ��ȣ�ۿ�Ű�� ������ �� ȣ��˴ϴ�.
    /// </summary>
    public abstract void OnInteractive(Hyerin.Player pl);
}
