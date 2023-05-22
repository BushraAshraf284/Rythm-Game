using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissHitDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Block"))
        {
            other.GetComponent<Block>().Hit();
            GameManager.Instance.MissBlock();
        }
    }
}
