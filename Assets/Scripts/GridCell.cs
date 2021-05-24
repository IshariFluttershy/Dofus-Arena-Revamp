using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridCell : MonoBehaviour
{
    [SerializeField]
    Material forbiddenMaterial;

    [SerializeField]
    GridCell right;
    [SerializeField]
    GridCell left;
    [SerializeField]
    GridCell up;
    [SerializeField]
    GridCell down;

    [SerializeField]
    public List<GridCell> neighbours { get; private set; }

    [SerializeField]
    private bool forbidden;
    public bool Forbidden { get { return forbidden; }}

    Vector2Int gridPosition;
    public Vector2Int GridPosition { get { return gridPosition; } }

    public int Distance { get; set; }
    public bool Visited { get; set; }
    public bool Selectable { get { return HighlightableComp.HighlightFlags.HasFlag(Highlight.Selectable); }}
    public bool Occupied { get; private set; }

    Highlightable HighlightableComp;

    public Highlight Highlight { get; set; }

    public Hero CellHero { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Init(transform.position.x, transform.position.z);
        HighlightableComp = GetComponent<Highlightable>();

        if (Forbidden)
            GetComponent<Renderer>().material = forbiddenMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool haveHit = Physics.Raycast(transform.position - Vector3.up * 2, Vector3.up, out hit);

        if (haveHit)
        {
            var cellHero = hit.transform.GetComponent<Hero>();

            if (cellHero != null)
                Occupied = true;
            else
                Occupied = false;
        }
        else 
            Occupied = false;
    }

    public void Init(float p_x, float p_y)
    {
        gridPosition.x = (int)p_x;
        gridPosition.y = (int)p_y;
    }

    public void SetNeighbours(GridCell p_right, GridCell p_left, GridCell p_up, GridCell p_down)
    {
        neighbours = new List<GridCell>();

        if (p_down != null)
        {
            down = p_down;
            neighbours.Add(down);
        }

        if (p_right != null)
        {
            right = p_right;
            neighbours.Add(right);
        }

        if (p_left != null)
        {
            left = p_left;
            neighbours.Add(left);
        }

        if (p_up != null)
        {
            up = p_up;
            neighbours.Add(up);
        }
    }

    public void SetSelectable(bool p_value)
    {
        if (p_value)
            HighlightableComp.HighlightFlags |= Highlight.Selectable;
        else
            HighlightableComp.HighlightFlags &= ~Highlight.Selectable;

    }

    public void OnMouseUpAsButton()
    {
        var hero = TurnBasedSystem.Instance.GetCurrentHero();

        if (!Forbidden && Selectable && !TurnBasedSystem.Instance.GetCurrentHero().IsActionSelected)
        {
            var cameFrom = GridCellManager.Instance.Pathfinding(hero.CurrentCell);

            GridCell current = this;
            Stack<GridCell> path = new Stack<GridCell>();
            var start = hero.CurrentCell;

            while (current != start)
            {
                path.Push(current);
                current = cameFrom[current];
            }

            hero.SetPath(path);
            //path.Enqueue(start);
            //path.Reverse();
        }

        else if (hero.IsActionSelected)
        {
            if (!Forbidden && Selectable)
            {
                RaycastHit hit;
                bool haveHit = Physics.Raycast(transform.position - Vector3.up*2, Vector3.up, out hit);

                if (haveHit)
                {
                    var cellHero = hit.transform.GetComponent<Hero>();

                    if (cellHero != null)
                        hero.DoSelectedAction(cellHero);
                }
                else
                    hero.DoSelectedAction(null);
            }

            hero.DeselectAction();
        }
    }

    public void Reset()
    {
        SetSelectable(false);
        Visited = false;
        Distance = 0;
    }
}

//      current = goal
//      path = []
//      while current != start: 
//         path.append(current)
//         current = came_from[current]
//      path.append(start) # optional
//      path.reverse() # optional
