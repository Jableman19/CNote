using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    Camera arCam;
    GameObject tappedObject;

    // Start is called before the first frame update
    void Start()
    {
        tappedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0) {
            return;
        }
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if(m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits)) {
            if(Input.GetTouch(0).phase == TouchPhase.Ended) {
                if(Physics.Raycast(ray, out hit)) {
                    if(hit.collider.gameObject.tag == "puzzleFemale") {
                        tappedObject = hit.collider.gameObject;
                        puzzleBase b = tappedObject.transform.parent.GetComponent<puzzleBase>();
                        print(b.visual.name);
                        // Do ui stuff here
                    }
                }
            }
        }
    }
}
