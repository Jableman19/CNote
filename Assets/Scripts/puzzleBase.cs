using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

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
    public Effects effect;
    public MockSynthManager synthController;


    // Start is called before the first frame update
    void Start()
    {
        
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
        return puzzleName.StartsWith("Base");
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
        // if root and not active set off
        if (isRoot() && !visual.activeSelf)
        {
            visual.SetActive(true);
            synthController.setEffect(effect, true);
        }
        else if(hasRoot() && !visual.activeSelf)
        {
            visual.SetActive(true);
            synthController.setEffect(effect, true);
            Debug.Log("here1");
        }
        else if(!hasRoot() && visual.activeSelf)
        {
            visual.SetActive(false);
            synthController.setEffect(effect, false);
            Debug.Log("here2");
        }
    }
}
