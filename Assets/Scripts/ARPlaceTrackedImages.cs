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
    public float timeToDelete = 1.5f;
    private ARTrackedImageManager trackedImagesManager;
    private Dictionary<string, bool> soundsActive = new Dictionary<string, bool>();
    private Dictionary<string, GameObject> activePieces = new Dictionary<string, GameObject>();
    private Dictionary<string, float> limitedPieces = new Dictionary<string, float>();
    private List<GameObject> activeBases = new List<GameObject>();
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

    public void Update()
    {
        List<string> imageNames = new List<string>(limitedPieces.Keys);
        foreach (string imageName in imageNames)
        {
            limitedPieces[imageName] += Time.deltaTime;

            if(limitedPieces[imageName] > timeToDelete)
            {
                if (imageName.StartsWith("Base"))
                {
                    activeBases.Remove(activePieces[imageName]);
                    //print("activeBases Count: " + activeBases.Count + ", activePieces[imageRemoved].activeSelf: " + activePieces[imageName].activeSelf);
                    if (activeBases.Count > 0 && activePieces[imageName].activeSelf)
                    {
                        activeBases[activeBases.Count - 1].SetActive(true);
                    }
                }
                Destroy(activePieces[imageName]);
                activePieces.Remove(imageName);
                limitedPieces.Remove(imageName);
            }

        }
    }

    Parameter nameToEffect(string name)
    {
        switch (name) {
            case "Base_Sine":
                return Parameter.SINE;
            case "Base_Saw":
                return Parameter.SAW;
            case "Base_Square":
                return Parameter.SQUARE;
            case "Effect_LowPassFilter":
                return Parameter.LOW_PASS;
            case "Effect_HighPassFilter":
                return Parameter.HIGH_PASS;
            case "Effect_Distortion":
                return Parameter.DISTORTION;
            case "Effect_Reverb":
                return Parameter.REVERB;
            case "Effect_Chorus":
                return Parameter.CHORUS;
            case "Effect_Echo":
                return Parameter.ECHO;
            default:
                throw new System.ArgumentException("Name is not an effect.", nameof(name));
         }

    }

    GameObject nameToViz(string name)
    {
        switch (name)
        {
            case "Base_Sine":
                return visualizations[0];
            case "Base_Saw":
                return visualizations[1];
            case "Base_Square":
                return visualizations[2];
            case "Effect_LowPassFilter":
                return visualizations[3];
            case "Effect_HighPassFilter":
                return visualizations[4];
            case "Effect_Distortion":
                return visualizations[5];
            case "Effect_Reverb":
                return visualizations[6];
            case "Effect_Chorus":
                return visualizations[7];
            case "Effect_Echo":
                return visualizations[8];
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
                new_piece.GetComponentInChildren<puzzleBase>().visual = new_viz;
                //new_piece.GetComponentInChildren<puzzleBase>().animating = true;
                if (imageName.StartsWith("Base"))
                {
                    activeBases.Add(new_piece);
                    string[] baseNames = { "Base_Sine" , "Base_Saw", "Base_Square" };
                    foreach(string name in baseNames)
                    {
                        if(name != imageName && activePieces.ContainsKey(name))
                        {
                            activePieces[name].SetActive(false);
                            activePieces[name].GetComponentInChildren<puzzleBase>().anim.enabled = false;
                        }
                    }
                }


            }
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated) {
            string imageName = trackedImage.referenceImage.name;

            // Detecting when a piece is no longer connected is super hard... This should work okay though.
            if (trackedImage.trackingState == TrackingState.None)
            {
                if (imageName.StartsWith("Base"))
                {
                    activeBases.Remove(activePieces[imageName]);
                    print("activeBases Count: " + activeBases.Count + ", activePieces[imageRemoved].activeSelf: " + activePieces[imageName].activeSelf);
                    if (activeBases.Count > 0 && activePieces[imageName].activeSelf)
                    {
                        activeBases[activeBases.Count - 1].SetActive(true);
                    }
                }
                Destroy(activePieces[imageName]);
                activePieces.Remove(imageName);
                limitedPieces.Remove(imageName);
            }

            if (trackedImage.trackingState == TrackingState.Limited)
            {
                float time;
                if(!limitedPieces.TryGetValue(imageName, out time)) {
                    limitedPieces.Add(imageName, 0f);
                }
            }

            GameObject piece;
            // If we lost the image, but find it again we need to add it back.
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!activePieces.TryGetValue(imageName, out piece))
                {
                    piece = Instantiate(puzzlePiece, trackedImage.transform);
                    piece.GetComponentInChildren<Effect>().root_parameter = nameToEffect(imageName);
                    piece.transform.parent = trackedImage.transform;
                    activePieces.Add(imageName, piece);
                    GameObject new_viz = Instantiate(nameToViz(imageName), piece.transform);
                    piece.GetComponentInChildren<puzzleBase>().visual = new_viz;
                    //piece.GetComponentInChildren<puzzleBase>().animating = true;
                    if (imageName.StartsWith("Base"))
                    {

                        activeBases.Add(piece);
                        string[] baseNames = { "Base_Sine", "Base_Saw", "Base_Square" };
                        foreach (string name in baseNames)
                        {
                            if (name != imageName && activePieces.ContainsKey(name))
                            {
                                activePieces[name].GetComponentInChildren<puzzleBase>().effect.setParameter(activePieces[name].GetComponentInChildren<puzzleBase>().effect.root_parameter, false);
                                activePieces[name].SetActive(false);
                                activePieces[name].GetComponentInChildren<puzzleBase>().anim.enabled = false;
                            }
                        }
                    }
                }

                float time;
                if(limitedPieces.TryGetValue(imageName, out time))
                {
                    limitedPieces.Remove(imageName);
                }


            }
        }

        foreach(ARTrackedImage trackedImage in eventArgs.removed) {
            string imageName = trackedImage.referenceImage.name;

            if (imageName.StartsWith("Base"))
            {
                activeBases.Remove(activePieces[imageName]);
                if (activeBases.Count > 0 && activePieces[imageName].activeSelf)
                {
                    activeBases[activeBases.Count - 1].SetActive(true);
                }
            }
            Destroy(activePieces[imageName]);
            activePieces.Remove(imageName);
        }
    }
}
