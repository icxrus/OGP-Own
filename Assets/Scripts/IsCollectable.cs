using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class IsCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.parent = other.transform;
        }
    }
}
