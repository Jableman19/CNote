using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleFemale : MonoBehaviour
{

    public puzzleBase pBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "puzzleMale")
        {
            pBase.prevChain = collision.gameObject.transform.parent.gameObject;
            collision.gameObject.transform.parent.gameObject.GetComponent<puzzleBase>().nextChain = this.gameObject;
        }
    }
}
