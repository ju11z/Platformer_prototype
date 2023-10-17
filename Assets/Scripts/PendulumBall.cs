using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumBall : MonoBehaviour
{
    public int Damage = 4;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.BeDamaged(Damage);
        }
    }
}
