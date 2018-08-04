using UnityEngine;
using System.Collections;

public class DetectLightning : MonoBehaviour {

    private Lightning lightMan;

    void Start()
    {
        lightMan = GameObject.FindObjectOfType<Lightning>();
    }

	void OnTriggerEnter(Collider col)
    {
        lightMan.ToggleLightning(col.tag);
    }

}
