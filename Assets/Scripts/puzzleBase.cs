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
    //public bool animating = true;
    [HideInInspector] public Animator anim;
    public Effect effect;


    // Start is called before the first frame update
    void Start()
    {
        // Get local effect component.
        effect = GetComponent<Effect>();
        anim = visual.GetComponent<Animator>();
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

    private void OnDisable()
    {
        effect.setParameter(effect.root_parameter, false);
    }

    // Update is called once per frame
    void Update()
    {
        /*print("Visual:" + visual.name);
        print("Visual Active:" + anim.enabled);*/
        //print("Connected: " + hasRoot());
        // If root and not active set off
        if (isRoot() && !anim.enabled)
        {
            Animator anim = visual.GetComponent<Animator>();
            anim.enabled = true;
            // anim.Rebind();
            // anim.Update(0f);
            effect.setParameter(effect.root_parameter, true);
            //animating = true;
        }
        else if(hasRoot() && !anim.enabled)
        {
            Animator anim = visual.GetComponent<Animator>();
            anim.enabled = true;
            // anim.Rebind();
            // anim.Update(0f);
            visual.GetComponent<ParticleSystem>().Play();
            effect.setParameter(effect.root_parameter, true);
            //animating = true;
        }
        else if(!hasRoot() && anim.enabled)
        {
            visual.GetComponent<Animator>().enabled = false;
            effect.setParameter(effect.root_parameter, false);
            //animating = false;
        }
    }
}
