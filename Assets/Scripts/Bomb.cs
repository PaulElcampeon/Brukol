using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    private GameObject particle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySFX(3);

            other.gameObject.GetComponent<LittleOne>().Die();

            Instantiate(particle, gameObject.transform.position, Quaternion.identity);

            Invoke("OpenPlayAgain", 1f);
        }
    }

    public void OpenPlayAgain()
    {
        InGameMenu.instance.OpenPlayAgainPanel();
    }
}
