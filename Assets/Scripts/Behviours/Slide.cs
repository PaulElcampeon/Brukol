using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float speed;

    [Header("LayerMask")]
    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private bool shouldMoveLeft;

    private bool isHittingASide = false;

    private Rigidbody2D rgb;

    void Start()
    {
        rgb = GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        float rayDistance = 0.3f;

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(-1 * 0.23f, 0.05f, 0), Vector2.left, rayDistance, groundLayerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(-1 * 0.27f, 0.05f, 0), Vector2.right, rayDistance, groundLayerMask);

        Debug.DrawRay(transform.position + new Vector3(-1 * 0.23f, 0.05f, 0), Vector2.left * rayDistance, Color.red);
        Debug.DrawRay(transform.position + new Vector3(-1 * 0.27f, 0.05f, 0), Vector2.right * rayDistance, Color.red);


        if (hitLeft.collider != null || hitRight.collider != null)
        {
            isHittingASide = true;
        }
        else
        {
            isHittingASide = false;
        }
    }

    private void FixedUpdate()
    {
        float dir = shouldMoveLeft ? 1 : -1;

        if (isHittingASide) rgb.velocity = new Vector2(0, rgb.velocity.y);
        if (!isHittingASide) rgb.velocity = new Vector2(dir * speed, rgb.velocity.y);
    }
}
