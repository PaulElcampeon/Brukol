using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject cellReference;

    [SerializeField]
    private GameObject easyPlatform, mediumPlatform, hardPlatform;

    private List<GameObject> cells = new List<GameObject>();

    public Dictionary<Vector2Int, Cell> worldState = new Dictionary<Vector2Int, Cell>();

    public Cell startingCell;

    public Cell endingCell;

    public int difficulty = 1;

    private int noOfBombs;
    private int noOfRows = 5;
    private int noOfColumns = 3;

    private float xMax, xMin, yMax, yMin, increment, noOfBlocksAcross, noOfBlocksDownward;

    public static LevelGenerator instance;

    private void Awake()
    {
        instance = this;

        difficulty = GameManager.instance.difficulty;

        if (difficulty == 1)
        {
            noOfBombs = Random.Range(5, 7);
            xMax = 3.22f;
            xMin = -2.78f;
            yMax = 2.09f;
            yMin = -3.91f;
            increment = 2;
            noOfBlocksAcross = ((xMax - xMin)/increment) + 1;
            noOfBlocksDownward = ((yMax - yMin) / increment) + 1;
            easyPlatform.SetActive(true);
        }
        if (difficulty == 2)
        {
            noOfBombs = Random.Range(10, 15);
            xMax = 3.73f;
            xMin = -3.77f;
            yMax = 2.54f;
            yMin = -3.46f;
            increment = 1.5f;
            noOfBlocksAcross = ((xMax - xMin) / increment) + 1;
            noOfBlocksDownward = ((yMax - yMin) / increment) + 1;
            mediumPlatform.SetActive(true);
        }
        if (difficulty == 3)
        {
            noOfBombs = Random.Range(20, 33);
            xMax = 4.54f;
            xMin = -4.46f;
            yMax = 3;
            yMin = -3;
            increment = 1;
            noOfBlocksAcross = ((xMax - xMin) / increment) + 1;
            noOfBlocksDownward = ((yMax - yMin) / increment) + 1;
            hardPlatform.SetActive(true);
        }
    }

    public void Generate()
    {
        foreach (GameObject cellW in GameObject.FindGameObjectsWithTag("Ground")) Destroy(cellW);

        cellReference.gameObject.transform.localScale = new Vector3(increment, increment, 0);

        cells.Clear();
        worldState.Clear();

        int currentXPos = 0;

        for (float i = xMin; i <= xMax; i += increment)
        {
            currentXPos++;

            int currentYPos = 0;

            for (float j = yMin; j <= yMax; j += increment)
            {
                currentYPos++;

                Vector3 position = new Vector3(i, j, 0);
                Vector2Int boardPos = new Vector2Int(currentXPos, currentYPos);
                GameObject cell = Instantiate(cellReference, position, Quaternion.identity, transform.parent);
                cell.GetComponent<Cell>().boardPosVector2Int = boardPos;
                worldState.Add(boardPos, cell.GetComponent<Cell>());
            }
        }

        Debug.Log("Generated Level");

        EnableBombs();

        AssignStartingCell();

        AssignEndingCell();
    }

    private void EnableBombs()
    {
        int bombsRemaining = noOfBombs;

        while (bombsRemaining > 0)
        {
            int randomXPos = Random.Range(1, (int)noOfBlocksAcross + 1);
            int randomYPos = Random.Range(1, (int)noOfBlocksDownward + 1);

            Vector2Int key = new Vector2Int(randomXPos, randomYPos);

            Cell cell;

            if (worldState.TryGetValue(key, out cell))
            {
                if (!cell.hasBomb)
                {
                    cell.hasBomb = true;
                    Debug.Log("Cell has bomb " + cell.GetGridPos());
                    bombsRemaining--;

                }
            }
        }

        Debug.Log("No more bombs remaining");
    }

    private void AssignStartingCell()
    {
        Vector2Int position = Vector2Int.zero;

        bool foundAvailablePos = false;

        Cell cell;

        while (foundAvailablePos == false)
        {
            int randomXPos = Random.Range(1, (int) noOfBlocksAcross + 1);
            int yPos = (int) noOfBlocksDownward;

            position = new Vector2Int(randomXPos, yPos);

            if (worldState.TryGetValue(position, out cell))
            {

                if (!cell.hasBomb)
                {
                    cell.isStartingPoint = true;
                    startingCell = cell;
                    foundAvailablePos = true;
                }
            }
        }

        Debug.Log("Starting point: " + position);
    }

    private void AssignEndingCell()
    {
        Vector2Int position = Vector2Int.zero;

        bool foundAvailablePos = false;

        Cell cell;

        while (foundAvailablePos == false)
        {
            int randomXPos = Random.Range(1, (int) noOfBlocksDownward + 1);

            int yPos = Random.Range(1, 1 + 2);

            position = new Vector2Int(randomXPos, yPos);

            if (worldState.TryGetValue(position, out cell))
            {

                if (!cell.hasBomb && !cell.isStartingPoint)
                {
                    cell.isEndPoint = true;
                    endingCell = cell;
                    cell.SetGoal(true);
                    foundAvailablePos = true;
                }
            }
        }

        Debug.Log("Ending point: " + position);
    }
}


