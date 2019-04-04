using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSound : MonoBehaviour
{
    public AudioSource SoundSource;
    public AudioClip DroneMain;
    public AudioClip DroneIdle;
    // public AudioClip ;
    
    void PlayDroneMain()
    {
        SoundSource.PlayOneShot(DroneMain);
    }

    void PlayDroneIdle()
    {
        SoundSource.PlayOneShot(DroneIdle);
    }
}
