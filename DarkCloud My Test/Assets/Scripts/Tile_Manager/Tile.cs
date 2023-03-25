using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Settings")]
    [SerializeField] SpriteRenderer renderer;
    [HideInInspector] public Data_Tiles dataTile_choosen;

    [Header("Highlighted Reference")]
    [SerializeField] GameObject highLighted;

    [Header("Path Tile Reference")]
    [SerializeField] GameObject tilePossiblePath;

    [Header("Select Tile Settings")]
    [HideInInspector] public bool canBeSelected;


    public void Init(bool isOffset)
    {
        renderer.sprite = dataTile_choosen.tileImage;

        if (isOffset) renderer.color = dataTile_choosen._offsetColor;

        else renderer.color = dataTile_choosen._baseColor;
    }


    void OnMouseEnter() => highLighted.SetActive(true); 


    void OnMouseExit() => highLighted.SetActive(false);

    //MOUSE CLICK
    void OnMouseDown()
    {
        if(canBeSelected)
        {
            Player.singleton.ChangePlayerPosition(this);
        }
    }

    //CALLED WHEN THE PLAYER CAN GO TO THIS TILE
    public void AppearPossiblePath(bool state)
    {
        tilePossiblePath.SetActive(state);
        canBeSelected = state;
    }
}
