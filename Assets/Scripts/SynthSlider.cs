using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynthSlider : MonoBehaviour
{

    public string float_name;
    public string title_text;
    LibPdInstance pdInstance;


    void Awake()
    {
        pdInstance = GameObject.Find("SynthManager").GetComponent<LibPdInstance>();
    }

    public void OnValueChange()
    {
        float value = GetComponent<Slider>().value;
        pdInstance.SendFloat(float_name, value);
        gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = title_text + value;
        Debug.Log(gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
    }


}
