using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum Highlight : short
{
    None = 0,
    MouseOver = 1,
    Selectable = 2
}


public class Highlightable : MonoBehaviour
{
    public Highlight HighlightFlags = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (HighlightFlags.HasFlag(Highlight.Selectable))
        {
            if (TurnBasedSystem.Instance.GetCurrentHero().IsActionSelected)
                GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue * 0.2f);
            else
                GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green * 0.2f);
            GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
        if (HighlightFlags.HasFlag(Highlight.MouseOver))
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0.2f);
            GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }

        if (HighlightFlags == Highlight.None)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0.2f);
            GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
