using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UIPlayerManager : MonoBehaviour
{
    public UnityEvent ItemAdded;
    public UnityEvent ItemRemoved;

    public UnityEvent BerryAdded;
    public UnityEvent RockAdded;
    public UnityEvent StickAdded;
    public UnityEvent FocusAdded;

    public UnityEvent PlayerDied;


    public UILifeControl[] Lives = new UILifeControl[3];
    public int InitialLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<InitialLives;++i)
        {
            Lives[i].Add();
        }
        //SubstractLives(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLives(int lives)
    {
        int livesToAdd = lives;
        while(livesToAdd > 0)
        {
            var found = Lives.FirstOrDefault(l => l.Active == false);
            if(found == null)
            {

            }
            else
                found.Add();
            --livesToAdd;
        }
        foreach (var l in Lives)
            l.UpdateLife();
        Debug.Log($"Added {lives} lives");
    }

    public void SubstractLives (int lives)
    {
        int livesToSubstract = lives;
        while (livesToSubstract > 0)
        {
            var found = Lives.LastOrDefault(l => l.Active == true);
            if(found != null)
                found.Remove();
            --livesToSubstract;
        }

        if (Lives.Where(l => l.Active).Count() <= 0)
            PlayerDied?.Invoke();
    }

    public void SetItem(InteractableObject obj)
    {
        if (obj == null)
        {
            ItemRemoved?.Invoke();
        }
        else
        {
            ItemAdded.Invoke();
            switch (obj.PickupType)
            {
                case PickupType.Berry:
                    BerryAdded?.Invoke();
                    break;
                case PickupType.Rock:
                    RockAdded?.Invoke();
                    break;
                case PickupType.Stick:
                    StickAdded?.Invoke();
                    break;
                case PickupType.PieceOfMind:
                    FocusAdded?.Invoke();
                    break;
                default:
                    ItemRemoved?.Invoke();
                    break;
            }

        }
    }

    
}
