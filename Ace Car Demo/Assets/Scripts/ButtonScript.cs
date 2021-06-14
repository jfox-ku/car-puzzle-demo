using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer MRenderer_;
    [SerializeField] private Animator Animator_;
    public IntEvent ButtonClickedEvent;
    public GameColorSO GameColorSO;

    private bool isLocked = false;


    private void Start() {
        MRenderer_.material = GameColorSO.baseMaterial;
    }

    public void OnMouseDown() {
        if (!isLocked) {
            Animator_.SetTrigger("ButtonClick");
        }
    }

    //Temporary
    //Called in the animation clip
    public void PublishButtonClicked() {
        ButtonClickedEvent?.Raise(GameColorSO.ColorID);
    }
    //Called in the animation clip
    public void LockInput() {
        isLocked = true;
    }
    //Called in the animation clip
    public void ReleaseInput() {
        isLocked = false;
    }


}
