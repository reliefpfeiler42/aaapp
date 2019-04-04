using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSound : MonoBehaviour
{
    public AudioSource SoundSource;
    // public AudioSource SoundSourceBody;
    public AudioClip ThreatenRoar;
    public AudioClip roar;
    public AudioClip knurr;
    // public AudioClip mechanic;
    
    void PlayThreatenRoar()
    {
        SoundSource.PlayOneShot(ThreatenRoar);
    }

    void PlayRoar()
    {
        SoundSource.PlayOneShot(roar);
    }

    void PlayKnurr()
    {
        SoundSource.PlayOneShot(knurr);
    }

    // void PlayMechanic()
    // {
    //     SoundSourceBody.PlayOneShot(mechanic);
    // }
}
