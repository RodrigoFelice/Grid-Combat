using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_Tiles")]
public class Data_Tiles : ScriptableObject
{
    public Sprite tileImage;
    public Color _baseColor, _offsetColor;
}
