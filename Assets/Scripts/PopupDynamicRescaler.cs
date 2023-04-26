using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDynamicRescaler : MonoBehaviour
{
    public float scale = 1;

    float parentScale;
    RectTransform rt;
    Transform parent;

    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        parent = gameObject.transform.parent;
        parentScale = parent.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate height, width based on distance
        Vector3 origin = Camera.main.transform.position;
        Vector3 position = parent.position;
        float distance = Vector3.Distance(origin, position);

        //Rescale
        rt.localScale = Vector3.one*distance*scale * (1/parent.localScale.x);
        
    }
}
