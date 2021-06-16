using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMarks : MonoBehaviour
{
    public GameObject CheckMarkPrefab;

    private void Start() {
        
    }


    public void AddCheckMarkToWorldPos(Vector3 wpos) {
        var pos = Camera.main.WorldToScreenPoint(wpos);
        var check = Instantiate(CheckMarkPrefab, this.transform);
        check.transform.position = pos;

    }


}
