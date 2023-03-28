using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LibPdInstance))]
public class MockSynthManager : MonoBehaviour, SynthController
{
    public bool isBaseEffect(Effects effect)
    {
        return (effect == Effects.SAW || effect == Effects.SINE);
    }

    public void setEffect(Effects effect, bool status)
    {
        switch(effect)
        {
            case (Effects.DISTORTION):
                gameObject.GetComponent<LibPdInstance>().SendBang("distortion"); // TODO: This should follow 'status' boolean.
                break;
            default:
                Debug.Log("Not a valid effect.");
                break;
        }
    }

    public void setEffect(Effects effect, float value)
    {
        switch (effect)
        {
            case (Effects.LOW_PASS_FREQUENCY):
                gameObject.GetComponent<LibPdInstance>().SendFloat("low_pass_frequency", value);
                break;
            case (Effects.LOW_PASS_RESONANCE):
                gameObject.GetComponent<LibPdInstance>().SendFloat("low_pass_resonance", value);
                break;
            default:
                Debug.Log("Not a valid effect.");
                break;
        }
    }

}
