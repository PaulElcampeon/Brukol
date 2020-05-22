using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpy : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField]
    private float jumpHeight;

    [Header("LayerMask")]
    [SerializeField]
    private LayerMask groundLayerMask;

    private Rigidbody2D rgb;

    private bool isGrounded;
    private bool canIJumpAgain = true;

    void Start()
    {
        rgb = GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        
        if (!canIJumpAgain) return;

        float rayDistance = 0.3f;

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - Vector3.right * 0.2f, Vector2.down, rayDistance, groundLayerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * 0.2f, Vector2.down, rayDistance, groundLayerMask);

        Debug.DrawRay(transform.position - Vector3.right * 0.2f, Vector2.down * rayDistance, Color.red);
        Debug.DrawRay(transform.position + Vector3.right * 0.2f, Vector2.down * rayDistance, Color.red);


        if (hitLeft.collider != null || hitRight.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            StartCoroutine(ResetCanJump());
            rgb.velocity = new Vector2(rgb.velocity.x, jumpHeight);
        }

        rgb.velocity = new Vector2(rgb.velocity.x, rgb.velocity.y - 0.2f);

    }

    private IEnumerator ResetCanJump()
    {
        canIJumpAgain = false;

        yield return new WaitForSeconds(0.2f);

        canIJumpAgain = true;
    }
}
