using UnityEngine;
using System.Collections;
using System.Linq;

public class Effect : MonoBehaviour
{
    public Parameter root_parameter; // The ROOT NAME OF THE EFFECT (i.e LOW_PASS, DISTORT, SINE).
    public Parameter[] addl_parameters; // Additional parameters (i.e LOW_PASS_FREQUENCY, LOW_PASS_RESONANCE).

    SynthManagerFMOD synthManager;

    public Effect(Parameter p)
    {
        root_parameter = p;
    }

    public Effect(Parameter root, Parameter[] addl)
    {
        root_parameter = root;
        addl_parameters = addl;
    }

    public bool isBaseWaveform()
    {
        return (root_parameter == Parameter.SAW || root_parameter == Parameter.SINE);
    }

    public void Start()
    {
        // Automatically find SynthManager instance.
        try
        {
            synthManager = GameObject.FindGameObjectWithTag("SynthManager").GetComponent<SynthManagerFMOD>();
        } catch (System.Exception e) {
            throw new System.NullReferenceException("A gameobject tagged 'SynthManager' with a <SynthManager> component must be present within the scene.");
        }

        // Start up effect.
        // setParameter(root_parameter, true);
    }

    public void setParameter(Parameter p, bool status)
    {
        if(!addl_parameters.Contains(p) && root_parameter != p)
        {
            throw new System.ArgumentException("Effect doesn't have parameter.", nameof(p));
        } else
        {
            Debug.Log(synthManager);
            synthManager.setParameter(p, status);
        }
    }

    public void setParameter(Parameter p, float value)
    {
        if (!addl_parameters.Contains(p) && root_parameter != p)
        {
            throw new System.ArgumentException("Effect doesn't have parameter.", nameof(p));
        }
        else
        {
            synthManager.setParameter(p, value);
        }
    }

    public void OnDestroy()
    {
        setParameter(root_parameter, false);
    }
}
