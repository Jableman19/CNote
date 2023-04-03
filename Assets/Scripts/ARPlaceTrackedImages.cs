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

    Effect nameToEffect(string name)
    {
        switch (name) {
            case "Base_Saw":
                return new Effect(Parameter.SAW);
            case "Base_Sine":
                return new Effect(Parameter.SINE);
            case "Effect_Distortion":
                return new Effect(Parameter.DISTORTION);
            default:
                throw new System.ArgumentException("Name is not an effect.", nameof(name));
         }

    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) 
    {
        // Enable all seen images
        foreach (ARTrackedImage trackedImage in eventArgs.added) 
        { 
            // Get the name of the reference image
            string imageName = trackedImage.referenceImage.name; 
            // GameObject newPrefab = Instantiate(puzzlePiece, trackedImage.transform);
            // newPrefab.GetComponent<puzzleBase>().puzzleName = imageName;
            // newPrefab.GetComponent<puzzleBase>().effect = nameToEffect(imageName);

            // activePieces[imageName] = newPrefab;
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
                if (soundsActive[imageName])
                {
                    // Destroy(activePieces[imageName]);
                    foreach (Function f in callOnTracked)
                    {
                        if (string.Compare(f.type + '_' + f.name, imageName, true) == 0)
                        {
                            if (f.type == "Base")
                            {
                                f.OnTracked.Invoke();
                                soundsActive[imageName] = false;
                            }
                            
                        }
                    }
                }
                
            } else if (trackedImage.trackingState == TrackingState.Tracking) {
                if (!soundsActive[imageName])
                {
                    foreach (Function f in callOnTracked)
                    {
                        if (string.Compare(f.type + '_' + f.name, imageName, true) == 0)
                        {
                            if (f.type == "Base")
                            {
                                f.OnTracked.Invoke();
                                soundsActive[imageName] = true;
                            }
                        }
                    }
                    // GameObject newPrefab = Instantiate(puzzlePiece, trackedImage.transform);
                    // newPrefab.GetComponent<puzzleBase>().puzzleName = imageName;
                    // newPrefab.GetComponent<puzzleBase>().effect = nameToEffect(imageName);
                    // activePieces[imageName] = newPrefab;
                }
                //foreach (Function f in callOnTracked) {
                //    if(string.Compare(f.type + '_' + f.name, imageName, true) == 0) {
                //        f.OnTracked.Invoke();
                //        soundsActive[imageName] = true;
                //    }
                //}
            }
        }

        /*foreach(ARTrackedImage trackedImage in eventArgs.removed) {
            string imageName = trackedImage.referenceImage.name; 
            if (soundsActive[imageName]) {
                Destroy(activePieces[imageName]);
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
        }*/
    }
}
