using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectButton : MonoBehaviour
{
    SynthManagerFMOD synthManagerFMOD;
    public Parameter effect;
    bool toggled = false;

    public void Awake()
    {
        synthManagerFMOD = GameObject.Find("SynthManager").GetComponent<SynthManagerFMOD>();
    }

    public void OnClick()
    {
        toggled = !toggled;
        synthManagerFMOD.setParameter(effect, toggled);
    }

    public void turnOn()
    {
        toggled = true;
    }

    public void turnOff()
    {
        toggled = false;
    }

    void Update()
    {
        gameObject.GetComponent<Outline>().enabled = toggled;
    }
}
