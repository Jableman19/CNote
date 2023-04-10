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

    public List<GameObject> visualizations;
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

    Parameter nameToEffect(string name)
    {
        switch (name) {
            case "Effect_Reverb":
                return Parameter.REVERB;
            case "Base_Sine":
                return Parameter.SINE;
            case "Effect_Distortion":
                return Parameter.DISTORTION;
            case "Effect_Chorus":
                return Parameter.CHORUS;
            case "Base_Saw":
                return Parameter.SAW;   
            default:
                throw new System.ArgumentException("Name is not an effect.", nameof(name));
         }

    }

    GameObject nameToViz(string name)
    {
        switch (name)
        {
            case "Effect_Reverb":
                return visualizations[1];
            case "Base_Sine":
                return visualizations[0];
            case "Effect_Distortion":
                return visualizations[0];
            case "Effect_Chorus":
                return visualizations[0];
            case "Base_Saw":
                return visualizations[2];
            default:
                throw new System.ArgumentException("Name is not an effect.", nameof(name));
        }
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) 
    {
        // Enable all seen images
        foreach (ARTrackedImage trackedImage in eventArgs.added) 
        {
            string imageName = trackedImage.referenceImage.name;

            GameObject new_piece; 
            if(activePieces.TryGetValue(imageName, out new_piece))
            {
                Debug.Log("Piece is already detected within the scene.");
            } else
            {
                new_piece = Instantiate(puzzlePiece, trackedImage.transform);
                new_piece.GetComponentInChildren<Effect>().root_parameter = nameToEffect(imageName);
                new_piece.transform.parent = trackedImage.transform;
                activePieces.Add(imageName, new_piece);
                GameObject new_viz = Instantiate(nameToViz(imageName), new_piece.transform);
                //new_viz.transform.localPosition = new_piece.transform.position;
                //new_viz.transform.localScale = new Vector3(.1f, .1f, .1f);
                //new_viz.transform.rotation = new_piece.transform.rotation;
                //new_viz.transform.parent = new_piece.transform;
                new_viz.SetActive(false);
                new_piece.GetComponentInChildren<puzzleBase>().visual = new_viz;


            }
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated) {
            string imageName = trackedImage.referenceImage.name;

            // Detecting when a piece is no longer connected is super hard... This should work okay though.
            if (trackedImage.trackingState == TrackingState.None || trackedImage.trackingState == TrackingState.Limited)
            {
                Destroy(activePieces[imageName]);
                activePieces.Remove(imageName);
            }

            GameObject new_piece;
            // If we lost the image, but find it again we need to add it back.
            if (trackedImage.trackingState == TrackingState.Tracking && !activePieces.TryGetValue(imageName, out new_piece))
            {
                new_piece = Instantiate(puzzlePiece, trackedImage.transform);
                new_piece.GetComponentInChildren<Effect>().root_parameter = nameToEffect(imageName);
                new_piece.transform.parent = trackedImage.transform;
                activePieces.Add(imageName, new_piece);
                GameObject new_viz = Instantiate(nameToViz(imageName), new_piece.transform);
                new_viz.transform.localPosition = new_piece.transform.position;
                new_viz.transform.localScale = new Vector3(.05f, .05f, .05f);
                new_viz.transform.rotation = new_piece.transform.rotation;
                new_viz.transform.parent = new_piece.transform;
                new_viz.SetActive(false);
                new_piece.GetComponentInChildren<puzzleBase>().visual = new_viz;
            }
        }

        foreach(ARTrackedImage trackedImage in eventArgs.removed) {
            string imageName = trackedImage.referenceImage.name;
            Destroy(activePieces[imageName]);
            activePieces.Remove(imageName);
        }
    }
}
