using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource dead;
    public AudioSource boost;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unPause;
    public AudioSource destroyAsteroid;
    public AudioSource hitRock;
    public AudioSource shoot;
    public AudioSource score;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
}
