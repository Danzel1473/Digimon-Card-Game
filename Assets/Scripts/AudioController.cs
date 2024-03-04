using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    void Awake(){
        instance = this;
    }

    [SerializeField] AudioSource drawSound;

    public void PlayDrawSound(){
        drawSound.Play();
    }
}
