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
        Debug.Log("TRIGGER");
        // If two pieces slide too close together, somehow collisions occur within the same puzzle piece.
        // Therefore, check to make sure the parent gameobjects for both pieces are the same.
        if(collision.gameObject.tag == "puzzleMale" &&
            collision.gameObject.transform.parent.gameObject != this.gameObject.transform.parent.gameObject)
        {
            pBase.prevChain = collision.gameObject.transform.parent.gameObject;
            collision.gameObject.transform.parent.gameObject.GetComponent<puzzleBase>().nextChain = this.gameObject.transform.parent.gameObject;
        }
    }

    // called for both objects
    private void OnTriggerExit(Collider other)
    {
        // Find the male and female
        if (pBase.prevChain == other.gameObject.transform.parent.gameObject) { 
            pBase.prevChain = null;
        }
        if (other.gameObject.transform.parent.gameObject.GetComponent<puzzleBase>().nextChain == this.gameObject.transform.parent.gameObject)
        {
            other.gameObject.transform.parent.gameObject.GetComponent<puzzleBase>().nextChain = null;
        }

    }
}
