using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    void Update()
    {
        CheckForTouch();
    }

    private void CheckForTouch()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {

                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPosition = new Vector2(wp.x, wp.y);

                foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Ground"))
                {
                    if (gameObject.GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPosition))
                    {
                        gameObject.GetComponent<Cell>().RemoveMud();
                    }
                }
            }
        }

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPosition = new Vector2(wp.x, wp.y);

                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Ground"))
                {
                    if (gameObject.GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPosition))
                    {
                        gameObject.GetComponent<Cell>().RemoveMud();
                    }
                }
            }
        }
    }

}
