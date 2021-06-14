using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControllerScript : MonoBehaviour
{
    public GameColorSO GateColor;
    [SerializeField] private Animator Animator_; 

    public void RaiseGateByColorID(int ColorID) {
        if(ColorID == GateColor.ColorID) {
            Animator_.SetTrigger("open_trigger");
        }

    }

}
