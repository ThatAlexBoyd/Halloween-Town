using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MagicManager : MonoBehaviour {

    private int score;
    public GameObject reminderText;
    public Text[] scoreTxt;
    public Image fader;

	void Start ()
    {
        score = 0;
	}

    public void CandyCollected(int add)
    {
        score = score + add;
        for (int i = 0; i < scoreTxt.Length; i++)
        {
            scoreTxt[i].text = "Score " + score.ToString();
        }
    }

    public void StartPath()
    {
        GameObject.FindObjectOfType<GamePath>().StartPath();
        reminderText.SetActive(false);
    }

    public void EndGame()
    {
        StartCoroutine(IEendGame());
    }
    IEnumerator IEendGame()
    {
        yield return new WaitForSeconds(5);
        fader.DOFade(1, 2).OnComplete(FadeAndRestart);
    }

    public void FadeAndRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
}
