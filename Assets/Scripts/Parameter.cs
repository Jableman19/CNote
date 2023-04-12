using System;
public enum Parameter
{
    // Base
    SAW,
    SINE,
    SQUARE,
    TRIANGLE,
    NOISE,

    // EFFECTS
    DISTORTION,
    REVERB,
    ECHO,
    CHORUS,

    // CONTROLLS
    HIGH_PASS, // Enable/Disable Low-Pass
    HIGH_PASS_FREQUENCY,
    HIGH_PASS_RESONANCE,
    LOW_PASS, // Enable/Disable High-Pass
    LOW_PASS_FREQUENCY,
    LOW_PASS_RESONANCE,
    NONE,
}
