using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        //Destroy(other.gameObject);
        other.gameObject.SetActive(false);
    }
}
