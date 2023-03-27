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

    LibPdInstance pdInstance;

    void Awake()
    {
        GetComponent<Outline>().enabled = toggled;
        pdInstance = GameObject.Find("SynthManager").GetComponent<LibPdInstance>();
    }

    public void OnClick() {
        turnOffAllButtons();
        toggled = true;
        pdInstance.SendFloat("oscillator_type", osc_type);
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
