using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject cellReference;

    private List<GameObject> cells = new List<GameObject>();

    public Dictionary<Vector2Int, Cell> worldState = new Dictionary<Vector2Int, Cell>();

    public Cell startingCell;

    public Cell endingCell;

    public int difficulty = 3;

    private int noOfBombs;
    private int noOfRows = 5;
    private int noOfColumns = 3;

    private float xMax, xMin, yMax, yMin, increment;

    public static LevelGenerator instance;

    private void Awake()
    {
        instance = this;
        if (difficulty == 1)
        {
            noOfBombs = Random.Range(5, 7);
            xMax = 3.22f;
            xMin = -2.78f;
            yMax = 4.29f;
            yMin = -1.71f;
            increment = 2;
        }
        if (difficulty == 2)
        {
            noOfBombs = Random.Range(10, 15);
            xMax = 3.73f;
            xMin = -3.77f;
            yMax = 2.54f;
            yMin = -3.46f;
            increment = 1.5f;
        }
        if (difficulty == 3)
        {
            noOfBombs = Random.Range(20, 33);
            xMax = 4.54f;
            xMin = -4.46f;
            yMax = 3;
            yMin = -3;
            increment = 1;
        }

        //noOfBombs = 10 * difficulty;
    }

    public void Generate()
    {

        foreach (GameObject cellW in GameObject.FindGameObjectsWithTag("Ground")) Destroy(cellW);

        cells.Clear();
        worldState.Clear();

        for (float i = -xMin; i <= xMax; i+=increment)
        {
            for (float j = -yMin; j <= yMax; j+=increment)
            {
                Vector3 position = new Vector3(i, j, 0);
                Vector2Int vector2IntOfPos = new Vector2Int(i, j);
                GameObject cell = Instantiate(cellReference, new Vector3(i, j, 0), Quaternion.identity, transform.parent);
                worldState.Add(vector2IntOfPos, cell.GetComponent<Cell>());
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
            int randomXPos = Random.Range(-noOfRows, noOfRows + 1);
            int randomYPos = Random.Range(-noOfColumns, noOfColumns + 1);

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
            int randomXPos = Random.Range(-noOfRows, noOfRows + 1);
            int yPos = noOfColumns;

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
            int randomXPos = Random.Range(-noOfRows, noOfRows + 1);
            int yPos = Random.Range(-noOfColumns, -noOfColumns + 2);

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


