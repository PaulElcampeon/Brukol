using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour
{ 
    [Header("Packet Attributes")]
    [SerializeField]
    private GameObject mud;

    [SerializeField]
    private GameObject surpise;

    public BoxCollider2D boxCollider { set; get; }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

  
    public void RemoveMud()
    {
        surpise.SetActive(true);
        boxCollider.enabled = false;
        mud.gameObject.SetActive(false);
    }
}
