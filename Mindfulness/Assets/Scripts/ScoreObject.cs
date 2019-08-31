using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    public int BonusScore;

    bool added;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!added)
        {
            if (col.gameObject.CompareTag("Player"))
            {

                var player = col.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    added = true;
                    player.AddScore(BonusScore);
                    gameObject.SetActive(false);
                    Destroy(this.gameObject);
                }
            }
        }
        Debug.Log("OnTrigerEnter");
    }
}
