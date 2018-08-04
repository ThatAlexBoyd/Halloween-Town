using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Candy : MonoBehaviour
{
    public int value;
    public AudioClip pickupSound;
    public AudioSource audioSource;
    public bool toStartPath = false;
    //public GameObject thatParticle;

    void Start()
    {
       transform.DOScale(new Vector3(transform.localScale.x * 0.66f, transform.localScale.y * 0.66f, transform.localScale.z * 0.66f), 1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
       // GameObject holder = Instantiate(thatParticle, transform.position, transform.rotation) as GameObject;
        //holder.transform.parent = transform;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.L))
        {
            Collected();
        }
    }

    public void Collected()
    {
        if (toStartPath)
            GameObject.FindObjectOfType<MagicManager>().StartPath();
        gameObject.GetComponent<Collider>().enabled = false;
        audioSource.PlayOneShot(pickupSound);
        GameObject.FindObjectOfType<MagicManager>().CandyCollected(value);
        transform.DORotate(new Vector3(0, 720, 0), 2f,RotateMode.FastBeyond360);
        transform.DOScale(new Vector3(0, 0, 0), 1.5f).OnComplete(Die);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
