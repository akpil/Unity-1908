using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
    private Player mPlayer;
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = transform.parent.gameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Interact"))
        {
            mPlayer.AttackTarget(collision.gameObject);
        }
    }
}
