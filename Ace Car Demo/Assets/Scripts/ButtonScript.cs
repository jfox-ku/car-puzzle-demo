using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer MRenderer_;
    [SerializeField] private Animator Animator_;
    public IntEvent ButtonClickedEvent;

    public IntEvent InputLockEvent; //1 is locked, 0 is unlocked
    public GameColorSO GameColorSO;

    public bool isLocked = false;


    private void Start() {
        MRenderer_.material = GameColorSO.baseMaterial;
    }

    public void OnMouseDown() {
        if (!isLocked) {
            Animator_.SetTrigger("ButtonClick");
        }
    }



    //Called in the animation clip
    public void PublishButtonClicked() {
        ButtonClickedEvent?.Raise(GameColorSO.ColorID);
    }
    //Called in the animation clip
    public void LockInput() {
        isLocked = true;
        //InputLockEvent?.Raise(1);
    }

    //Called in the animation clip
    public void ReleaseInput() {
        isLocked = false;
        //InputLockEvent?.Raise(0);
    }

    //Unused for now
    public void LockEvent(int lockState) {
        if (lockState == 0) {
            isLocked = false;
        } else if (lockState == 1) {
            isLocked = true;
        }
    }


}
