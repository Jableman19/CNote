using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Effect))]
public class puzzleBase : MonoBehaviour
{
    [Serializable]
    public struct Function
    {
        public string name;
        public string type;
        public UnityEvent On;
        public UnityEvent Off;
    }
    public Function[] callOnChange;

    public GameObject prevChain;
    public GameObject nextChain;
    public GameObject visual;
    public string puzzleName;
    public Effect effect;


    // Start is called before the first frame update
    void Start()
    {
        // Get local effect component.
        effect = GetComponent<Effect>();
    }

    void ToggleEffect(bool On)
    {
        foreach (Function f in callOnChange)
        {
            if (string.Compare(f.type + '_' + f.name, puzzleName, true) == 0)
            {
                if (f.type == "Effect")
                {
                    if(On && hasRoot())
                    {
                        f.On.Invoke();
                    }
                    else
                    {
                        f.Off.Invoke();
                    }
                    
                }
            }
        }
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
        // If root and not active set off
        if (isRoot() && !visual.activeSelf)
        {
            visual.SetActive(true);
            effect.setParameter(effect.root_parameter, true);
        }
        else if(hasRoot() && !visual.activeSelf)
        {
            visual.SetActive(true);
            effect.setParameter(effect.root_parameter, true);
        }
        else if(!hasRoot() && visual.activeSelf)
        {
            visual.SetActive(false);
            effect.setParameter(effect.root_parameter, false);
        }
    }
}
