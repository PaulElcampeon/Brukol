using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Queue<Cell> queue = new Queue<Cell>();
    private bool isRunning = true;
    private Cell searchCenter;
    private List<Cell> path = new List<Cell>();

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public static PathFinder instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        isRunning = true;

        LevelGenerator.instance.Generate();

        queue.Clear();
        path.Clear();

        try
        {
            foreach (Cell cell in GetPath()) Debug.Log(cell.GetGridPos());
        }
        catch
        {
            Debug.LogWarning("Calling Regenerate again");
            Generate();
        }
    }

    public List<Cell> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(LevelGenerator.instance.endingCell);

        Cell previous = LevelGenerator.instance.endingCell.exploredFrom;

        while (previous != LevelGenerator.instance.startingCell)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }

        SetAsPath(LevelGenerator.instance.startingCell);
        path.Reverse();
    }

    private void SetAsPath(Cell cell)
    {
        path.Add(cell);
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(LevelGenerator.instance.startingCell);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == LevelGenerator.instance.endingCell)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) return;

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;

            if (LevelGenerator.instance.worldState.ContainsKey(neighbourCoordinates))
            {
                Cell cell;
                if (LevelGenerator.instance.worldState.TryGetValue(neighbourCoordinates, out cell))
                {
                    if (!cell.hasBomb && cell.GetGridPos().y >= LevelGenerator.instance.endingCell.GetGridPos().y)
                    {
                        QueueNewNeighbours(neighbourCoordinates);
                    } else
                    {
                        Debug.Log("Our neighbour at: " + cell.GetGridPos() + " has a bomb, we will ignore him");
                    }
                }
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Cell neighbour = LevelGenerator.instance.worldState[neighbourCoordinates];

        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    public void DisplayPath()
    {
        foreach(Cell cell in path)
        {
            cell.ShowPath();
        }
    }
}
