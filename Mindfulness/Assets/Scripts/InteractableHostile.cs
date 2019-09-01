using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHostile : MonoBehaviour
{
    public int Damage = 1;

    float AnotherDamageCooldown = 1;
    float damageTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (damageTimer > 0)
            damageTimer -= Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (damageTimer <= 0)
        {
            if (col.gameObject.CompareTag("Player"))
            {

                var player = col.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.Damage(Damage);
                    damageTimer = AnotherDamageCooldown;
                }
            }

        }
        Debug.Log("OnTrigerEnter");
    }
}
