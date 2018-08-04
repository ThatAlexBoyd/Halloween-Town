using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GamePath : MonoBehaviour {

    public Transform player;

    public GameObject[] waypoints;


    public void StartPath()
    {
         Sequence path = DOTween.Sequence();
         path.Append(player.DOMove(waypoints[0].transform.localPosition, 10).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[1].transform.localPosition, 9).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[2].transform.localPosition, 12).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[3].transform.localPosition, 17).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[4].transform.localPosition, 18).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[5].transform.localPosition, 10).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[6].transform.localPosition, 9).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[7].transform.localPosition, 33).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[8].transform.localPosition, 7).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[9].transform.localPosition, 7).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[10].transform.localPosition, 40).SetEase(Ease.Linear))
             .Append(player.DOMove(waypoints[11].transform.localPosition, 10).SetEase(Ease.Linear));
        
         path.PlayForward();
    }
	
}
