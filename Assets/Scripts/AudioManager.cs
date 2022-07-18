using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//audio manager object with this script plays all sound clips, so they run for full duration
public class AudioManager : MonoBehaviour
{
    //sound and visual effects
    private AudioSource audioPlayer;
    public AudioClip explosionSound, rocketLaunchSound;
    public float soundVolume = 0.5f;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void ExplosionSound()
    {
        audioPlayer.PlayOneShot(explosionSound, soundVolume);
    }

    public void RocketLaunchSound()
    {
        audioPlayer.PlayOneShot(rocketLaunchSound, soundVolume);
    }
}
