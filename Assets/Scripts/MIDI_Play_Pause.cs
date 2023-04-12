using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MIDI_Play_Pause : MonoBehaviour
{

    SynthManagerFMOD synthManager;
    [HideInInspector] public bool playing;
    [HideInInspector] public static MIDI_Play_Pause instance;
    public Sprite pause;
    public Sprite play;

    public Image img;

    void Awake()
    {
        synthManager = GameObject.FindGameObjectWithTag("SynthManager").GetComponent<SynthManagerFMOD>();
        playing = true;
        instance = this;
    }

    public void PlayPause()
    {
        if (MIDIManager.Instance.isPlaying() || playing)
        {
            MIDIManager.Instance.PauseSong();
            synthManager.pause(); 
            img.sprite = play;
        }
        else
        {
            MIDIManager.Instance.ResumeSong();
            synthManager.resume(); 
            img.sprite = pause;
        }

        playing = !playing;
    }



}
