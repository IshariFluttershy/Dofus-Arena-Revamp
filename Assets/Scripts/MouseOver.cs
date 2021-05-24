using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Highlightable comp = GetComponent<Highlightable>();

        if (comp != null)
            comp.HighlightFlags = comp.HighlightFlags | Highlight.MouseOver;
    }

    private void OnMouseExit()
    {
        Highlightable comp = GetComponent<Highlightable>();

        if (comp != null)
            comp.HighlightFlags &= ~Highlight.MouseOver; 
    }
}
