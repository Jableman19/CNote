using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceTrackedImages : MonoBehaviour
{
    [Serializable]
    public struct Function {
        public string name;
        public string type;
        public UnityEvent OnTracked;
    }

    public Function[] callOnTracked;

    //public GameObject[] ArPrefabs;
    //private readonly Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImagesManager;
    private Dictionary<string, bool> soundsActive = new Dictionary<string, bool>();

    void Awake() {
        trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }
    void OnEnable() { 
        trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged; 
    } 
    void OnDisable() { 
        trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) 
    {
        // Enable all seen images
        foreach (ARTrackedImage trackedImage in eventArgs.added) 
        { 
            // Get the name of the reference image
            string imageName = trackedImage.referenceImage.name; 
            
            //Search for prefab to spawn based on name
            /*foreach (GameObject prefab in ArPrefabs) 
            { 
                if (string.CompareOrdinal(prefab.name, imageName) == 0 
                    && !instantiatedPrefabs.ContainsKey(imageName)) 
                { 
                    GameObject newPrefab = Instantiate(prefab, trackedImage.transform); 
                    instantiatedPrefabs[imageName] = newPrefab;
                } 
            } */

            // Toggle corresponding sound
            foreach(Function f in callOnTracked) {
                if(string.Compare(f.type + '_' + f.name, imageName, true) == 0) {
                    f.OnTracked.Invoke();
                    soundsActive[imageName] = true;
                }
            }
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated) {
            string imageName = trackedImage.referenceImage.name; 
            if(trackedImage.trackingState == TrackingState.Limited && soundsActive[imageName]) {
                foreach(Function f in callOnTracked) {
                    if(string.Compare(f.type + '_' + f.name, imageName, true) == 0) {
                        if(f.type == "Effect") {
                            f.OnTracked.Invoke();
                        }
                        soundsActive[imageName] = false;
                    }
                }
            } else if (trackedImage.trackingState == TrackingState.Tracking && !soundsActive[imageName]) {
                foreach(Function f in callOnTracked) {
                    if(string.Compare(f.type + '_' + f.name, imageName, true) == 0) {
                        f.OnTracked.Invoke();
                        soundsActive[imageName] = true;
                    }
                }
            }
        }
    }
}
