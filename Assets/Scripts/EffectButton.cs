using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectButton : MonoBehaviour
{
    LibPdInstance pdInstance;
    public string effect;
    bool toggled = false;

    public void Awake()
    {
        pdInstance = GameObject.Find("SynthManager").GetComponent<LibPdInstance>();
    }

    public void OnClick()
    {
        toggled = !toggled;
        pdInstance.SendBang(effect);
    }

    void Update()
    {
        gameObject.GetComponent<Outline>().enabled = toggled;
    }
}
