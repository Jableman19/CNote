using UnityEngine;
using System.Collections;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

public class MIDIManager : MonoBehaviour
{
    private MidiFile song;
    private static Playback _playback;
    SynthManagerFMOD synthManager;
    public static MIDIManager Instance;
    public void Awake()
    {
        synthManager = gameObject.GetComponent<SynthManagerFMOD>();
        
        Instance = this;

        /*song = MidiFile.Read(Application.streamingAssetsPath + "/MIDI/Mario64.mid");
        _playback = song.GetPlayback(new PlaybackSettings
        {
            ClockSettings = new MidiClockSettings
            {
                CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator() // Cannot use HighPrecisionTickGenerator due to iOS. 
            }
        });
        _playback.EventPlayed += OnNotePlayed;
        _playback.Start();*/
    }

    public void ChangeSong(string filepath) {
        if(_playback != null) {
            _playback.Stop();
        }
        song = MidiFile.Read(filepath);
        _playback = song.GetPlayback(new PlaybackSettings
        {
            ClockSettings = new MidiClockSettings
            {
                CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator() // Cannot use HighPrecisionTickGenerator due to iOS. 
            }
        });
        _playback.EventPlayed += OnNotePlayed;
        _playback.Start();
    }

    private void OnNotePlayed(object sender, MidiEventPlayedEventArgs e)
    {
        NoteOnEvent note_on = e.Event as NoteOnEvent;
        synthManager.playNote(midiToFrequency(note_on.NoteNumber));
        //Debug.Log(midiToFrequency(note_on.NoteNumber));
        
    }


    private float midiToFrequency(int note)
    {
        return 440f * Mathf.Pow(2f, (note - 69f) / 12f); // Standard conversion formula found online.
    }

}
