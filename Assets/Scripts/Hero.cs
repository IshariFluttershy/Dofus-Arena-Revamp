using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    int maxHp;
    public int MaxHp { get { return maxHp; } }
    public int Hp { get; private set; }

    [SerializeField]
    int maxPA;
    public int MaxPA { get { return maxPA; } }
    public int Pa { get; private set; }

    [SerializeField]
    int maxPM;
    public int MaxPM { get { return maxPM; } }
    public int Pm { get; private set; }

    [SerializeField]
    float moveSpeed = 2.0f;

    List<Action> Actions;
    Action SelectedAction;
    public bool IsActionSelected { get { if (SelectedAction != null) return true; return false; } }

    public bool Moving { get; private set; }

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    Stack<GridCell> path;

    [SerializeField]
    GridCell currentCell;
    public GridCell CurrentCell { get { return currentCell; } }

    Highlightable highlightable;

    // Start is called before the first frame update
    void Start()
    {
        Pm = MaxPM;
        Pa = MaxPA;
        Hp = MaxHp;
        transform.position = CurrentCell.transform.position;

         highlightable = GetComponent<Highlightable>();
    }

    public void Init(HeroData p_data)
    {
        Actions = new List<Action>();

        maxHp = p_data.MaxHP;
        maxPA = p_data.MaxPA;
        maxPM = p_data.MaxPM;

        foreach (var id in p_data.ActionsId)
        {
            var data = ActionsManager.Instance.GetActionDataFromId(id);
            var action = new Action();
            action.SetDatas(data);
            Actions.Add(action);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnBasedSystem.Instance.GetCurrentHero() != this)
            return;

        if (!Moving && SelectedAction == null)
            GridCellManager.Instance.FindSelectableTiles(CurrentCell, Pm);

        if (!Moving && SelectedAction != null)
            GridCellManager.Instance.FindSelectableTiles(CurrentCell, SelectedAction.MaxRange, SelectedAction.MinRange, SelectedAction.IsBlocking);

        if (path != null && Moving)
            Move();


        
    }

    private void OnGUI()
    {
        if (highlightable != null && highlightable.HighlightFlags.HasFlag(Highlight.MouseOver))
        {
            Vector3 screenPos = FindObjectOfType<Camera>().WorldToScreenPoint(transform.position + Vector3.up);
            float newHeight = Screen.height - screenPos.y;
            newHeight = Mathf.Abs(newHeight);
            Rect rect = new Rect(screenPos.x-30, newHeight-20, 100.0f, 40.0f);
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            GUI.Label(rect, "HP : " + Hp + "/" + MaxHp, style);
        }
    }

    public void SetPath(Stack<GridCell> p_path)
    {
        path = p_path;
        Moving = true;
        Pm -= p_path.Count;
        GridCellManager.Instance.ResetSelectableTiles();
        currentCell.Reset();
    }

    public void Move()
    {
        if (path.Count > 0)
        {
            GridCell cell = path.Peek();
            Vector3 target = cell.transform.position;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                currentCell = path.Pop();
            }

            Moving = true;
        }
        else
        {
            Moving = false;
        }
    }

    void CalculateHeading(Vector3 p_target)
    {
        heading = p_target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    public void EndTurnReset()
    {
        Pm = MaxPM;
        Pa = MaxPA;
        SelectedAction = null;

        GridCellManager.Instance.ResetSelectableTiles();
    }

    public void SelectAction(int p_index)
    {
        if (Pa < Actions[p_index].PACost)
            return;

        SelectedAction = Actions[p_index];
        GridCellManager.Instance.ResetSelectableTiles();
    }

    public void DeselectAction()
    {
        SelectedAction = null;
        GridCellManager.Instance.ResetSelectableTiles();
    }

    public void DoSelectedAction(Hero p_target)
    {
        if (p_target != null)
            p_target.ReceiveDamages(SelectedAction.Damages);

        Pa -= SelectedAction.PACost;
    }

    public void ReceiveDamages(int p_damages)
    {
        Hp -= p_damages;

        if (Hp <= 0)
            Die();
    }

    void Die()
    {
        TurnBasedSystem.Instance.HeroDies(this);
        Destroy(gameObject);
    }

    public void OnMouseUpAsButton()
    {
        var hero = TurnBasedSystem.Instance.GetCurrentHero();

        if (hero.IsActionSelected)
        {
            if (!CurrentCell.Forbidden && CurrentCell.Selectable)
                hero.DoSelectedAction(this);

            hero.DeselectAction();
        }
    }

    public int ActionsCount()
    {
        return Actions.Count;
    }

    public string ActionName(int p_index)
    {
        return Actions[p_index].Name;
    }

    public void PAChange(int p_pa)
    {
        Pa -= p_pa;
    }

    public void SetCurrentCell(GridCell p_cell)
    {
        currentCell = p_cell;
    }
}
