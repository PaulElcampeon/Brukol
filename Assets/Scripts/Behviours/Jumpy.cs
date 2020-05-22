using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpy : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField]
    private float jumpHeight;

    private Rigidbody2D rigidbody2D;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //// Cast a ray straight down.
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        //// If it hits something...
        //if (hit.collider != null)
        //{
        //    // Calculate the distance from the surface and the "error" relative
        //    // to the floating height.
        //    float distance = Mathf.Abs(hit.point.y - transform.position.y);
        //    float heightError = floatHeight - distance;

        //    // The force is proportional to the height error, but we remove a part of it
        //    // according to the object's speed.
        //    float force = liftForce * heightError - rb2D.velocity.y * damping;

        //    // Apply the force to the rigidbody.
        //    rb2D.AddForce(Vector3.up * force);
        //}

    }

    private void FixedUpdate()
    {
        if (isGrounded) rigidbody2D.AddForce(new Vector2(0, jumpHeight) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (rigidbody2D.velocity.y == 0) isGrounded = true;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (rigidbody2D.velocity.y > 0) isGrounded = false;
        }
    }
}
