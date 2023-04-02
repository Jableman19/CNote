using System;

public interface SynthController
{
    /* Toggles an effect given a bool whether to turn it on or off*/
    public void setParameter(Parameter effect, bool status);
    /* Sets an effect that takes a value such as high/low pass filters */
    public void setParameter(Parameter effect, float value);
    /* Returns true if given effect is a base wave */
    public bool isBaseEffect(Parameter effect);
}
