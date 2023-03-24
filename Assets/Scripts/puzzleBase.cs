using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleBase : MonoBehaviour
{

    public GameObject prevChain;
    public GameObject nextChain;
    public GameObject visual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(prevChain != null || nextChain != null && !visual.activeSelf)
        {
            visual.SetActive(true);
        }
        else if(prevChain == null || nextChain == null && visual.activeSelf)
        {
            visual.SetActive(false);
        }
    }
}
