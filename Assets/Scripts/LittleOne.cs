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

    private Vector2 goalPosition;
    private bool shouldMoveToRefuge;

    private void Awake()
    {
        if (GameManager.instance.difficulty == 1)
        {
            transform.localScale = new Vector3(transform.localScale.x * 2f, transform.localScale.y * 2f, transform.localScale.z * 2f);

        } else if (GameManager.instance.difficulty == 2)
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z * 1.5f);
        }
    }

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
        if (shouldMoveToRefuge)
        {
            rgb.gravityScale = 0;

            transform.position = Vector2.MoveTowards(transform.position, goalPosition, 2f * Time.deltaTime);
        }
        else
        {
            rgb.velocity = new Vector2(movementSpeed, rgb.velocity.y);

            if (rgb.velocity.y == 0) rgb.velocity = new Vector2(rgb.velocity.x, jumpHeight);
        }
    }

    private void CheckIfHittingWall()
    {
        float multiplier = 1;

        if (GameManager.instance.difficulty == 1) multiplier *= 2f;
        if (GameManager.instance.difficulty == 2) multiplier *= 1.5f;

        float dist = 0.55f * multiplier;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.left * 0.28f + Vector3.down / 5) * multiplier, Vector2.right, dist, groundLayerMask);

        Debug.DrawRay(transform.position + (Vector3.left * 0.28f  + Vector3.down / 5) * multiplier, Vector2.right * dist, Color.red);

        if (hit.collider != null)
        {
            movementSpeed *= -1f;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void MergeWithGoal(Vector2 goalPosition)
    {
        shouldMoveToRefuge = true;
        this.goalPosition = goalPosition;
    }
}
