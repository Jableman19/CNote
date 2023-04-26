using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDynamicRescaler : MonoBehaviour
{
    /*public float height = 6;
    public float width = 9;*/
    public float scale = 1;

    // Update is called once per frame
    void Update()
    {
        //Calculate height, width based on distance
        Vector3 origin = Camera.main.transform.position;
        Vector3 position = this.gameObject.transform.position;
        float distance = Vector3.Distance(origin, position);
        /*float newHeight = height * distance*scale;
        float newWidth = width * distance*scale;*/

        //Rescale
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.localScale = Vector3.one * distance*scale;
        /*gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);*/
        
    }
}
