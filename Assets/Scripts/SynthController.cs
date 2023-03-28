using System;

public interface SynthController
{
    /* Toggles an effect given a bool whether to turn it on or off*/
    public void setEffect(Effects effect, bool status);
    /* sets an effect that takes a value such as high/low pass filters */
    public void setEffect(Effects effect, float value);
    /* returns true if given effect is a base wave */
    public bool isBaseEffect(Effects effect);
}
