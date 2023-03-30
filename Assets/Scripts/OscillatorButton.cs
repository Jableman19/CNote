using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscillatorButton : MonoBehaviour
{
    public bool toggled = true;

    // The type of oscillator that this button should switch to.
    // AS OF NOW:
    // 0 -> Sine Wave
    // 1 -> Sawtooth Wave
    // 2 -> Noise
    public int osc_type = 0;

    SynthManagerFMOD synthManager;

    void Awake()
    {
        GetComponent<Outline>().enabled = toggled;
        synthManager = GameObject.Find("SynthManager").GetComponent<SynthManagerFMOD>();
    }

    public void OnClick() {
        turnOffAllButtons();
        toggled = true;

        // THIS IS NOT HOW YOU SHOULD HANDLE THIS!
        // Only setting this up to ensure OscillatorButton continues to work before it's deprecated.
        if(osc_type == 0)
        {
            synthManager.setParameter(Parameter.SINE, true); 
        } else if (osc_type == 1)
        {
            synthManager.setParameter(Parameter.SAW, true);
        } else
        {
            synthManager.setParameter(Parameter.NOISE, true);
        }
    }

    public void turnOn()
    {
        toggled = true;
    }

    public void turnOff()
    {
        toggled = false;
    }

    void Update() { GetComponent<Outline>().enabled = toggled; }


    // Turns off outlines on other oscillator buttons.
    // Returns whether or not a different oscillator was selected (to avoid .
    bool turnOffAllButtons()
    {
        bool prevSelected = false;
        GameObject[] others = GameObject.FindGameObjectsWithTag("Oscillators");

        foreach (GameObject other in others)
        {
            other.GetComponent<OscillatorButton>().toggled = false;
            prevSelected = true;
        }

        return prevSelected;
    }
}
