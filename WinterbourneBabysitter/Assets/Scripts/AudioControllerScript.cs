using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerScript : MonoBehaviour
{

    [Header("Audio Manager")]
    [SerializeField] AudioSource audioSource;

    private float musicVolume = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void updateVolume(float volume) {
        musicVolume = volume;
    }
}
