using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUIRotation : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * -Vector3.back, cam.transform.rotation * Vector3.up);
    }
}
