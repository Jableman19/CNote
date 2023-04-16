using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynthSlider : MonoBehaviour
{

    public Parameter float_name;
    //public string title_text;
    SynthManagerFMOD synthManagerFMOD;
    static float keepValue = .5f;


    void Awake()
    {
        synthManagerFMOD = GameObject.Find("SynthManager").GetComponent<SynthManagerFMOD>();
        GetComponent<Slider>().value = keepValue;
    }

    public void OnValueChange()
    {
        float value = GetComponent<Slider>().value;
        keepValue = value;
        print("Slider Value: " + value);
        synthManagerFMOD.setParameter(float_name, value);
        //gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = title_text + value;
        //Debug.Log(gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
    }


}
