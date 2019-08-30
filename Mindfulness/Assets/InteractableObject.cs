using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField()]
    SpriteRenderer Highlight;

    public UnityEvent UseObject;

    public PickupType PickupType;
    public int Amount;

    SpriteRenderer sRenderer;
    bool defaultIsKinematic;
    Rigidbody2D rigidB;
    int DefaultLayer;

    //public InteractableObjectEvent UseObject;

    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        rigidB = GetComponent<Rigidbody2D>();
        if (rigidB != null)
            defaultIsKinematic = rigidB.isKinematic;
        DefaultLayer = sRenderer.sortingLayerID;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanPickup;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {

            var player = col.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                //czy ma wolne rece i moze uzyc/podniesc
                if (player.CanPickup)
                {
                    Highlight.enabled = true;

                    player.RegisterAsAvailableObject(this);
                }
            }
        }
        Debug.Log("OnTrigerEnter");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Highlight.enabled = false;
            var player = col.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.UnregisterAsAvailableObject(this);
            }
        }
        Debug.Log("OnTrigerExit");
    }

    public void Use()
    {
        if (UseObject != null)
            UseObject.Invoke();

        this.gameObject.SetActive(false);
        DestroyImmediate(this);//TODO:nie wiem czemu sie nie usuwaja
    }

    public void PickedUp(IPlayerController player)
    {
        var allColliders = this.GetComponents<Collider2D>();
        foreach (var col in allColliders)
            col.enabled = false;
        this.transform.SetParent(player.GetPointToAttach());
        this.transform.localPosition = Vector3.zero;

        var playerItemsLayerId = SortingLayer.NameToID("PlayerItems");
        sRenderer.sortingLayerID = playerItemsLayerId;


        if (rigidB != null)
            rigidB.isKinematic = true;
    }

    public void Drop()
    {
        var allColliders = this.GetComponents<Collider2D>();
        foreach (var col in allColliders)
            col.enabled = true;
        this.transform.SetParent(null);

        sRenderer.sortingLayerID = DefaultLayer;
        if (rigidB != null)
            rigidB.isKinematic = defaultIsKinematic;
    }
}
