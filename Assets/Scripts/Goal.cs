using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField]
    private GameObject particle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Game won");

            Instantiate(particle, gameObject.transform.position, Quaternion.identity);

            Invoke("OpenPlayAgain", 1f);
        }
    }

    public void OpenPlayAgain()
    {
        InGameMenu.instance.OpenPlayAgainPanel();
    }
}
