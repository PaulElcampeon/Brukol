using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LittleOne : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private float movementSpeed = 0.5f;

    private Rigidbody2D rgb;

    void Start()
    {
        rgb = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {
        CheckIfHittingWall();
    }

    private void FixedUpdate()
    {
        rgb.velocity = new Vector2(movementSpeed, rgb.velocity.y);

        if (rgb.velocity.y == 0) rgb.velocity = new Vector2(rgb.velocity.x, jumpHeight);
    }

    private void CheckIfHittingWall()
    {
        float dist = 0.55f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left * 0.28f + Vector3.down / 5, Vector2.right, dist, groundLayerMask);

        Debug.DrawRay(transform.position + Vector3.left * 0.28f  + Vector3.down / 5, Vector2.right * dist, Color.red);

        if (hit.collider != null)
        {
            movementSpeed *= -1f;
        }
    }
}
