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
            SoundManager.instance.PlaySFX(0);
            Instantiate(particle, gameObject.transform.position, Quaternion.identity);
            other.gameObject.GetComponent<LittleOne>().MergeWithGoal(transform.position);
            Invoke("OpenPlayAgain", 1f);
        }
    }

    public void OpenPlayAgain()
    {
        InGameUI.instance.OpenPlayAgainPanel();
    }
}
