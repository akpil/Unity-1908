using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Give Damage to boss");
        }
    }
}
