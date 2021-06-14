using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNodeScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SRenderer;
    public GameColorSO NodeColorSO;

    public CarNodeScript NextNodeMain;
    public CarNodeScript NextNodeAlt;

    private bool isOccupied = false;

    private void Start() {
        SRenderer.color = NodeColorSO.GetColor();
    }

    public CarNodeScript GetNextNode() {
        if (NextNodeMain != null && !NextNodeMain.isOccupied) {
            return NextNodeMain;
        } else if (NextNodeAlt != null && !NextNodeAlt.isOccupied) {
            return NextNodeAlt;
        } else return null; 
    }

}
