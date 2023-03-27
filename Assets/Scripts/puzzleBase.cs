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

    bool hasRoot()
    {
        Debug.Log("hasRootCheck");
        if (puzzleName.StartsWith("Base"))
        {
            Debug.Log("RootFound");
            return true;
        }
        else if(prevChain == null)
        {
            return false;
        }
        else
        {
            return prevChain.GetComponent<puzzleBase>().hasRoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((prevChain != null || nextChain != null) && !visual.activeSelf)
        {
            visual.SetActive(true);
        }
        else if((prevChain == null && nextChain == null) && visual.activeSelf)
        {
            visual.SetActive(false);
        }
    }
}
