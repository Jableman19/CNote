using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Effect))]
public class puzzleBase : MonoBehaviour
{
    public GameObject prevChain;
    public GameObject nextChain;
    public GameObject visual;
    public bool animating = false;
    private Effect effect;


    // Start is called before the first frame update
    void Start()
    {
        // Get local effect component.
        effect = GetComponent<Effect>();
    }

    bool isRoot()
    {
        return effect.isBaseWaveform();
    }

    bool hasRoot()
    {
        if (isRoot())
        {
            return true;
        } else if(prevChain == null)
        {
            return false;
        }
        else
        {
            return prevChain ? prevChain.GetComponent<puzzleBase>().hasRoot() : false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print("Visual:" + visual.name);
        print("Visual Active:" + visual.activeSelf);
        // If root and not active set off
        if (isRoot() && !animating)
        {
            visual.GetComponent<Animator>().enabled = true;
            effect.setParameter(effect.root_parameter, true);
            animating = true;
        }
        else if(hasRoot() && !animating)
        {
            visual.GetComponent<Animator>().enabled = true;
            effect.setParameter(effect.root_parameter, true);
            animating = true;
        }
        else if(!hasRoot() && animating)
        {
            visual.GetComponent<Animator>().enabled = false;
            effect.setParameter(effect.root_parameter, false);
            animating = false;
        }
    }
}
