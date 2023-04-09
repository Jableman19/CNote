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
    bool playing;

    void Awake()
    {
        synthManager = GameObject.FindGameObjectWithTag("SynthManager").GetComponent<SynthManagerFMOD>();
        playing = true;
    }

    public void PlayPause()
    {
        if (playing) { synthManager.pause(); }
        else { synthManager.resume(); }
        if (MIDIManager.Instance.isPlaying())
        {
            MIDIManager.Instance.PauseSong();
        }
        else
        {
            MIDIManager.Instance.ResumeSong();
        }

        playing = !playing;
    }



}
