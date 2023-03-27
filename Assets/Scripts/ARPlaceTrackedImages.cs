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

    public GameObject puzzlePiece;
    private ARTrackedImageManager trackedImagesManager;
    private Dictionary<string, bool> soundsActive = new Dictionary<string, bool>();
    private Dictionary<string, GameObject> activePieces = new Dictionary<string, GameObject>();
    void Awake() {
        Application.targetFrameRate = 60;
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
            GameObject newPrefab = Instantiate(puzzlePiece, trackedImage.transform);
            newPrefab.GetComponent<puzzleBase>().puzzleName = imageName;
            activePieces[imageName] = newPrefab;
            // Toggle corresponding sound
            foreach(Function f in callOnTracked) {
                if(string.Compare(f.type + '_' + f.name, imageName, true) == 0) {
                    if(f.type == "Base")
                    {
                        f.OnTracked.Invoke();
                        soundsActive[imageName] = true;
                    }
                }
            }
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated) {
            string imageName = trackedImage.referenceImage.name; 
            if(trackedImage.trackingState == TrackingState.Limited) {
                Destroy(activePieces[imageName]);
                if (soundsActive[imageName])
                {
                    foreach (Function f in callOnTracked)
                    {
                        if (string.Compare(f.type + '_' + f.name, imageName, true) == 0)
                        {
                            if (f.type == "Base")
                            {
                                f.OnTracked.Invoke();
                            }
                            soundsActive[imageName] = false;
                        }
                    }
                }
                
            } else if (trackedImage.trackingState == TrackingState.Tracking) {
                GameObject newPrefab = Instantiate(puzzlePiece, trackedImage.transform);
                activePieces[imageName] = newPrefab;
                newPrefab.GetComponent<puzzleBase>().puzzleName = imageName;
                //foreach (Function f in callOnTracked) {
                //    if(string.Compare(f.type + '_' + f.name, imageName, true) == 0) {
                //        f.OnTracked.Invoke();
                //        soundsActive[imageName] = true;
                //    }
                //}
            }
        }
    }
}
