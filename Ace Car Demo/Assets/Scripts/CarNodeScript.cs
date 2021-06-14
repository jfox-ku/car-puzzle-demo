using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNodeScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SRenderer;
    public GameColorSO NodeColorSO;

    private void Start() {
        SRenderer.color = NodeColorSO.GetColor();
    }

}
