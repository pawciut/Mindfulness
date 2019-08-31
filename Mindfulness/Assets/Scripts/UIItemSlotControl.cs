using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlotControl : MonoBehaviour
{
    public Behaviour[] Tooltips = new Behaviour[2];
    public GameObject BerryPresenter;
    public GameObject RockPresenter;
    public GameObject StickPresenter;
    public GameObject FocusPresenter;

    Dictionary<PickupType, GameObject> ItemDictionary;

    private void Start()
    {
        ItemDictionary = new Dictionary<PickupType, GameObject>();
        ItemDictionary.Add(PickupType.Berry, BerryPresenter);
        ItemDictionary.Add(PickupType.Rock, RockPresenter);
        ItemDictionary.Add(PickupType.Stick, StickPresenter);
        ItemDictionary.Add(PickupType.PieceOfMind, FocusPresenter);

        DisplayEmpty();

        HideTips();

    }

    public void ShowTips()
    {
        if (Tooltips != null)
            foreach (var img in Tooltips)
                img.enabled = true;
    }

    public void HideTips()
    {
        if (Tooltips != null)
            foreach (var img in Tooltips)
                img.enabled = false;
    }

    public void DisplayEmpty()
    {
        foreach(var beh in ItemDictionary.Values)
        {
            beh.SetActive(false);
        }
    }

    public void DisplayBerry()
    {
        DisplayKey(PickupType.Berry);
    }
    public void DisplayRock()
    {
        DisplayKey(PickupType.Rock);
    }
    public void DisplayStick()
    {
        DisplayKey(PickupType.Stick);
    }
    public void DisplayFocus()
    {
        DisplayKey(PickupType.PieceOfMind);
    }


    void DisplayKey(PickupType keyToDisplay)
    {
        foreach(var key in ItemDictionary.Keys)
        {
            ItemDictionary[key].SetActive( key == keyToDisplay);
        }
    }

}
