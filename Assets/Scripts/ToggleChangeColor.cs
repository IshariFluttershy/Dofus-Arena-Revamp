using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateToggleColor(bool p_ison)
    {
        if (p_ison)
            GetComponent<Image>().color = Color.gray;
        else
            GetComponent<Image>().color = Color.white;
    }
}
