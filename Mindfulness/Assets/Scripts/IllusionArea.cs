using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionArea : MonoBehaviour
{
    [SerializeField()]
    GameObject Focus;

    Animator anim;

    private void Start()
    {
        anim = Focus.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            //var player = col.gameObject.GetComponent<PlayerController>();
            //if (player != null)
            //{
            //    //czy ma wolne rece i moze uzyc/podniesc
            //    if (player.CanPickup)
            //    {
            //        Highlight.enabled = true;

            //        player.RegisterAsAvailableObject(this);
            //    }
            //}
            if (anim != null)
                anim.SetBool(Constants.Focus_Animator_Parameter, true);
            
        }
        Debug.Log("OnIllusionShow");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Highlight.enabled = false;
            //var player = col.gameObject.GetComponent<PlayerController>();
            //if (player != null)
            //{
            //    player.UnregisterAsAvailableObject(this);
            //}

            if (anim != null)
                anim.SetBool(Constants.Focus_Animator_Parameter, false);
        }
        Debug.Log("OnIllusionHidden");
    }
}
