using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipmentUi : MonoBehaviour
{

    public PlayerMovement player;

    public Sprite selectedBorder;
    public Sprite border;


    public Image wiperPanel;
    public Image sprayPanel;
    public Image mopPanel;

    private ItemHeld currentItem;

    void Update()
    {
        SetSelectedEquipment();
    }

    void SetSelectedEquipment()
    {
        currentItem = player.itemHeld;
        switch (currentItem)
        {
            case ItemHeld.Wiper:
                ResetSeletedItem();
                wiperPanel.sprite = selectedBorder;
                break;
            case ItemHeld.Spray:
                ResetSeletedItem();
                sprayPanel.sprite = selectedBorder;

                break;
            case ItemHeld.Broom:
                ResetSeletedItem();
                mopPanel.sprite = selectedBorder;

                break;
            case ItemHeld.Nothing:
                ResetSeletedItem();

                break;
            default:
                break;
        }
    }

    void ResetSeletedItem()
    {
        wiperPanel.sprite = border;
        sprayPanel.sprite = border;
        mopPanel.sprite = border;
    }

}
