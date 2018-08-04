using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

    public Light lightning;
    public bool continueLightning = false;
    

    public void ToggleLightning(string coltag)
    {
        if ("WaxOn" == coltag)
        {
            continueLightning = true;
            StartCoroutine(StartLightning());
        }
        else if( "WaxOff" == coltag)
        {
            continueLightning = false;
        }
    }

    public IEnumerator StartLightning()
    {
        lightning.intensity = 1;
        yield return new WaitForSeconds(0.2f);
        lightning.intensity = 0;
        yield return new WaitForSeconds(0.05f);
        lightning.intensity = 0.5f;
        yield return new WaitForSeconds(0.1f);
        lightning.intensity = 0;
        yield return new WaitForSeconds(0.1f);
        lightning.intensity = 1;
        yield return new WaitForSeconds(0.15f);
        lightning.intensity = 0;
        yield return new WaitForSeconds(0.2f);
        lightning.intensity = 1;
        yield return new WaitForSeconds(0.1f);
        lightning.intensity = 0;
        yield return new WaitForSeconds(1.2f);

        if (continueLightning)
        {
            StartCoroutine(StartLightning());
        }
    }
}
