using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MeshRenderer MRenderer_;
    public GameColorSO GameColorSO;


    private void Start() {
        MRenderer_.material = GameColorSO.baseMaterial;
    }

    public void OnPointerDown(PointerEventData eventData) {
        
    }
}
