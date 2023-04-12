using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class TapManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    Camera arCam;
    GameObject tappedObject;
    public GameObject songbook;

    // Start is called before the first frame update
    void Start()
    {
        tappedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0 || songbook.activeSelf) {
            return;
        }
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if(m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits)) {
            if(Input.GetTouch(0).phase == TouchPhase.Ended) {
                if(Physics.Raycast(ray, out hit)) {
                    print(hit.collider.gameObject.tag);
                    if(hit.collider.gameObject.tag == "puzzleFemale") {
                        tappedObject = hit.collider.gameObject;
                        puzzleBase b = tappedObject.transform.parent.GetComponent<puzzleBase>();
                        GameObject popup = b.visual.transform.Find("Popup").gameObject;
                        popup.SetActive(true);
                    } else if (hit.collider.gameObject.tag == "Popup") {
                        return;
                    } else {
                        GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
                        foreach(GameObject popup in popups) {
                            popup.SetActive(false);
                        }
                    }
                } else {
                    GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
                    foreach(GameObject popup in popups) {
                        popup.SetActive(false);
                    }
                }
            }
        }
    }
}
