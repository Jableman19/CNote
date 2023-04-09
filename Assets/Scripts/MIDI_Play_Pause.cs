using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MIDI_Play_Pause : MonoBehaviour
{

    //// Start is called before the first frame update
    //void Start()
    //{
    //    Button midi_play_pause = transform.Find("UI").Find("MidiPlayPause").GetComponent<Button>();
    //    TextMeshProUGUI play_pause_text = midi_play_pause.transform.Find("TextPlayPause").GetComponent<TextMeshProUGUI>();
    //    play_pause_text.text = "play".ToString();
    //    midi_play_pause.onClick.AddListener(OnPressedMidiPlayPause);
    //}

    //public void OnPressedMidiPlayPause()
    //{
    //    Button midi_play_pause = transform.Find("UI").Find("MidiPlayPause").GetComponent<Button>();
    //    TextMeshProUGUI play_pause_text = midi_play_pause.transform.Find("TextPlayPause").GetComponent<TextMeshProUGUI>();
    //    if (MIDIManager.Instance != null)
    //    {
    //        if (MIDIManager.Instance.isPlaying())
    //        {
    //            MIDIManager.Instance.PauseSong();
    //            play_pause_text.text = "Play".ToString();
    //        }
    //        else
    //        {
    //            MIDIManager.Instance.ResumeSong();
    //            play_pause_text.text = "Pause".ToString();
    //        }
    //    }
    //}
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
