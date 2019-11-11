using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("Hit", 10);
        }
        else if (other.gameObject.CompareTag("EnemyBolt"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
