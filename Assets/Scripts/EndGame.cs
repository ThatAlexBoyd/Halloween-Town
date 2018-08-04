using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
       if(col.tag == "Player")
        {
            GameObject.FindObjectOfType<MagicManager>().EndGame();
        }
    }
}
