using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellManager : MonoBehaviour
{
    public static GridCellManager Instance { get; private set; }

    [SerializeField]
    int xCells;
    [SerializeField]
    int yCells;

    [SerializeField]
    private GridCell gridCellPrefab;

    private List<GridCell> ResetableTiles;

    //      frontier = Queue()
    //      frontier.put(start )
    //      reached = set()
    //      reached.add(start)
    //
    //      while not frontier.empty():
    //          current = frontier.get()
    //          for next in graph.neighbors(current):

    //          if next not in reached:
    //              frontier.put(next)
    //              reached.add(next)
    //

    

    private Map<GridCell> gridCellList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;

        ResetableTiles = new List<GridCell>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gridCellList = new Map<GridCell>();
        GridCell[] array = FindObjectsOfType(typeof(GridCell)) as GridCell[];

        for (int i = 0; i < array.Length; i++)
        {
            var cell = array[i];

            if (!cell.isActiveAndEnabled)
                continue;

            gridCellList[cell.GridPosition.x, cell.GridPosition.y] = cell;

        }
        for (int i = 0; i < array.Length; i++)
        {
            var cell = array[i];

            if (!cell.isActiveAndEnabled)
                continue;

            cell.SetNeighbours(gridCellList[cell.GridPosition.x + 1, cell.GridPosition.y],
                    gridCellList[cell.GridPosition.x - 1, cell.GridPosition.y],
                    gridCellList[cell.GridPosition.x, cell.GridPosition.y + 1],
                    gridCellList[cell.GridPosition.x, cell.GridPosition.y - 1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GridCell GetCell(int p_x, int p_y)
    {
        return gridCellList[p_x, p_y];
    }

    public Dictionary<GridCell, GridCell> Pathfinding(GridCell p_start)
    {
        Queue<GridCell> frontier = new Queue<GridCell>();
        Dictionary<GridCell, GridCell> cameFrom = new Dictionary<GridCell, GridCell>();

        frontier.Enqueue(p_start);
        cameFrom[p_start] = null;

        while (frontier.Count != 0)
        {
            var current = frontier.Dequeue();

            foreach (GridCell cell in current.neighbours)
            {
               if (!cameFrom.ContainsKey(cell) && !cell.Forbidden)
                {
                    frontier.Enqueue(cell);
                    cameFrom[cell] = current;
                }
            }
        }

        return cameFrom;
    }

    public void FindSelectableTiles(GridCell p_start, int p_maxRange, int p_minRange = 1, bool p_blocking = true)
    {
        Queue<GridCell> frontier = new Queue<GridCell>();

        frontier.Enqueue(p_start);
        p_start.Visited = true;
        
        while (frontier.Count != 0)
        {
            var current = frontier.Dequeue();

            if (current.Distance >= p_minRange)
            {
                current.SetSelectable(true);
            }

            ResetableTiles.Add(current);

            if (current.Distance < p_maxRange)
            {
                foreach (GridCell cell in current.neighbours)
                {
                    if (!cell.Visited && ((p_blocking && !cell.Forbidden && !cell.Occupied) || !p_blocking))
                    {
                        cell.Distance = 1 + current.Distance;
                        cell.Visited = true;
                        frontier.Enqueue(cell);
                    }
                }
            }
        }
    }

    public void ResetSelectableTiles()
    {
        foreach (GridCell cell in ResetableTiles)
        {
            cell.SetSelectable(false);
            cell.Visited = false;
            cell.Distance = 0;
        }
    }
}

//      frontier = Queue()
//      frontier.put(start)
//      came_from = dict()
//      came_from[start] = None
//      
//      while not frontier.empty():
//         current = frontier.get()
//         for next in graph.neighbors(current):
//            if next not in came_from:
//                  frontier.put(next)
//                  came_from[next] = current
//      