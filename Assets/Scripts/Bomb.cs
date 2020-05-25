using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    private GameObject particle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Packet")
        {
            other.gameObject.GetComponent<LittleOne>().Die();

            Instantiate(particle, gameObject.transform.position, Quaternion.identity);

        }
    }
}
