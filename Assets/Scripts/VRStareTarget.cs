using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VRStareTarget : MonoBehaviour {
    public Camera myCam;
    public float lookDuration;
    float lookTime;
    public Image toggleImage;
    public string SearchTag;
    public float distance = 1000;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray(myCam.transform.position, myCam.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, distance))
        {
            if(hit.collider.tag == SearchTag)
            {
                lookTime += Time.deltaTime;
            }
            else
            {
                lookTime = 0;
            }
        }
        else
        {
            lookTime = 0;
         
        }
        toggleImage.fillAmount = lookTime / lookDuration;
        if(toggleImage.fillAmount >= 1)
        {
            lookTime = 0;
            //SEND MESSAGE;
            hit.collider.SendMessage("Collected");
        }
    }
}
