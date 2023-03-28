using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LibPdInstance))]
public class SynthManager : MonoBehaviour
{
    public bool isBaseWaveform(Effect e)
    {
        return e.isBaseWaveform();
    }

    public void setParameter(Parameter p, bool status)
    {
        switch(p)
        {
            case (Parameter.SINE):
                gameObject.GetComponent<LibPdInstance>().SendFloat("oscillator_type", 0);
                break;
            case (Parameter.SAW):
                gameObject.GetComponent<LibPdInstance>().SendFloat("oscillator_type", 1);
                break;
            case (Parameter.DISTORTION):
                Debug.Log(boolToFloat(status));
                Debug.Log(status);
                gameObject.GetComponent<LibPdInstance>().SendFloat("distortion", boolToFloat(status));
                break;
            case (Parameter.CHORUS):
                throw new System.NotImplementedException("Chorus is not implemented.");
            case (Parameter.REVERB):
                throw new System.NotImplementedException("Reverb is not implemented.");
            default:
                throw new System.ArgumentException("Cannot pass a boolean to effect.", nameof(p));
        }
    }

    public void setParameter(Parameter p, float value)
    {
        switch (p)
        {
            case (Parameter.LOW_PASS_FREQUENCY):
                gameObject.GetComponent<LibPdInstance>().SendFloat("low_pass_frequency", value);
                break;
            case (Parameter.LOW_PASS_RESONANCE):
                gameObject.GetComponent<LibPdInstance>().SendFloat("low_pass_resonance", value);
                break;
            default:
                throw new System.ArgumentException("Cannot pass a float to effect.", nameof(p));

        }
    }

    // Helper function that allows booleans to be sent via PureData 'message' function.
    private float boolToFloat(bool b) { return b ? 1.0f : 0f; }

}
