using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SynthManagerFMOD : MonoBehaviour
{

    private FMOD.ChannelGroup group; // Audio Signal Chain.
    private FMOD.DSP osc; // Base Waveform-Oscillator.



    private FMOD.DSP distortion;
    private FMOD.DSP chorus;
    private FMOD.DSP reverb;

    private FMOD.DSP low_pass;
    private FMOD.DSP high_pass;

    public bool isBaseWaveform(Effect e)
    {
        return e.isBaseWaveform();
    }

    void Start()
    {
        // Automatically pick out master channel
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out group));

        // Initialize oscillator object.
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.OSCILLATOR, out osc));

        FMOD.Channel channel; // Not sure if this variable should be kept.
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.playDSP(osc, group, false, out channel));

        // Create effects.
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.DISTORTION, out distortion));
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.CHORUS, out chorus));
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.SFXREVERB, out reverb));

        // Create filters.
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.LOWPASS, out low_pass));
        CHECK_OK(FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.HIGHPASS, out high_pass));

    }

    public void setParameter(Parameter p, bool status)
    {
        switch (p)
        {
            case (Parameter.SINE):
                CHECK_OK(osc.setParameterInt((int)FMOD.DSP_OSCILLATOR.TYPE, 0));
                break;
            case (Parameter.SQUARE):
                CHECK_OK(osc.setParameterInt((int)FMOD.DSP_OSCILLATOR.TYPE, 1));
                break;
            case (Parameter.SAW):
                CHECK_OK(osc.setParameterInt((int)FMOD.DSP_OSCILLATOR.TYPE, 2));
                break;
            case (Parameter.TRIANGLE):
                CHECK_OK(osc.setParameterInt((int)FMOD.DSP_OSCILLATOR.TYPE, 4));
                break;
            case (Parameter.NOISE):
                CHECK_OK(osc.setParameterInt((int)FMOD.DSP_OSCILLATOR.TYPE, 5));
                break;
            case (Parameter.DISTORTION):
                if(!status) { CHECK_OK(group.removeDSP(distortion)); }
                else { CHECK_OK(group.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, distortion)); }
                break;
            case (Parameter.CHORUS):
                if (!status) { CHECK_OK(group.removeDSP(chorus)); }
                else { CHECK_OK(group.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, chorus)); }
                break;
            case (Parameter.REVERB):
                if (!status) { CHECK_OK(group.removeDSP(reverb)); }
                else { CHECK_OK(group.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, reverb)); }
                break;
            case (Parameter.LOW_PASS):
                if (!status) { CHECK_OK(group.removeDSP(low_pass)); }
                else { CHECK_OK(group.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, low_pass)); }
                break;
            case (Parameter.HIGH_PASS):
                if (!status) { CHECK_OK(group.removeDSP(high_pass)); }
                else { CHECK_OK(group.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, high_pass)); }
                break;
            default:
                throw new System.ArgumentException("Cannot pass a boolean to effect.", nameof(p));
        }
    }

    public void setParameter(Parameter p, float value)
    {
        switch (p)
        {
            case (Parameter.LOW_PASS_FREQUENCY):
                osc.setParameterFloat((int)FMOD.DSP_LOWPASS.CUTOFF, value);
                break;
            case (Parameter.LOW_PASS_RESONANCE):
                osc.setParameterFloat((int)FMOD.DSP_LOWPASS.RESONANCE, value);
                break;
            default:
                throw new System.ArgumentException("Cannot pass a float to effect.", nameof(p));

        }
    }

    public void playNote(float pitch)
    {
        CHECK_OK(osc.setParameterFloat((int) FMOD.DSP_OSCILLATOR.RATE, pitch));
    }

    public void pause() { CHECK_OK(group.setPaused(true)); }
    public void resume() { CHECK_OK(group.setPaused(false)); }

    public bool getPaused()
    {
        bool result;
        CHECK_OK(group.getPaused(out result));
        return result;
    }

    private void CHECK_OK(FMOD.RESULT result)
    {
        if(result != FMOD.RESULT.OK)
        {
            throw new UnityException("SynthManager Error: " + result);
        }
    }
}
