using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameColor",menuName ="GameColor/New Color")]
public class GameColorSO : ScriptableObject
{
    public Material baseMaterial;


    public Color GetColor() {
        return baseMaterial.color;
    }
}
