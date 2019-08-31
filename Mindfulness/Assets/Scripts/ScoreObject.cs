using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    public int BonusScore;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {

            var player = col.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddScore(BonusScore);
                gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        Debug.Log("OnTrigerEnter");
    }
}
