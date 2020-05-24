using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private GameObject mud;

    [SerializeField]
    private GameObject bomb;

    [SerializeField]
    private GameObject goal;

    public bool isExplored = false;
    public Cell exploredFrom;

    public bool isStartingPoint;
    public bool hasBomb;
    public bool isEndPoint;

    public void RemoveMud()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        mud.SetActive(false);

        if (hasBomb) bomb.SetActive(true);
    }

    public void SetGoal(bool hasGoal)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        mud.SetActive(false);
    
        if (hasGoal) goal.SetActive(true);
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }
}
