using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float jumpHeight;

    private Rigidbody2D rgb;

    void Start()
    {
        rgb = GetComponentInChildren<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (rgb.velocity.y == 0) rgb.velocity = new Vector2(rgb.velocity.x, jumpHeight);
    }
}
