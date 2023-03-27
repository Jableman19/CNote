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

        print(collision);

        if(collision.gameObject.tag == "puzzleMale")
        {
            print(collision.gameObject.transform.parent.gameObject);
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
