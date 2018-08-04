using UnityEngine;
using System.Collections;

public class Thunder : MonoBehaviour {

    public AudioClip lightningSound;
    public AudioSource audioSource;

    void OnTriggerEnter(Collider col)
    {
        audioSource.PlayOneShot(lightningSound);
    }
}
