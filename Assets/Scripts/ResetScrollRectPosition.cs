using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScrollRectPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        this.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }

}
