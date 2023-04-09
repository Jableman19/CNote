using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using TMPro;

public struct Song {
    public string name;
    public string src;
}

public class Songbook : MonoBehaviour
{
    public GameObject button;
    private List<Song> songs = new List<Song>();

    private static Playback _playback;
    SynthManagerFMOD synthManager;

    void Awake() {
        synthManager = gameObject.GetComponent<SynthManagerFMOD>();
    }

    void Start()
    {
        // Generate list of songs
        string folderPath = Application.streamingAssetsPath + "/MIDI/";
        foreach (string file in System.IO.Directory.GetFiles(folderPath)) {
            if(Path.GetExtension(file) == ".mid") {
                Song s = new Song();
                //Debug.Log(file);
                s.name = Path.GetFileNameWithoutExtension(file);
                s.src = file;
                songs.Add(s);
            }  
        }

        // Generate buttons for songs
        foreach(Song s in songs) {
            Debug.Log(s.name);
            GameObject btn = Instantiate(button);
            btn.transform.parent = gameObject.transform;
            btn.GetComponent<Button>().onClick.AddListener(() => switchSong(s));
            TMP_Text txt = button.GetComponentInChildren<TMP_Text>();
            btn.transform.localScale = new Vector3(1, 1, 1);
            txt.text = s.name;
        }
    }

    // Update is called once per frame
    void switchSong(Song s)
    {
       MIDIManager.Instance.ChangeSong(s.src);
    }
}
