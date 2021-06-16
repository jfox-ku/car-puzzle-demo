using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    HashSet<CarNodeScript> AllCarNodes;
    private int checkCount;
    public VoidEvent AllNodesMatchedEvent;
    

    void Start()
    {
        InitliazeNodes();
        
    }

    //Called when a checkmark is created
    public void MatchAtPos(Vector3 pos) {
        checkCount++;
        if(checkCount >= AllCarNodes.Count) {
            Debug.Log("You win!");
            AllNodesMatchedEvent?.Raise(new Void());
        }

    }

    private void InitliazeNodes() {
        checkCount = 0;
        int Color1Count = 0;
        int Color2Count = 0;

        AllCarNodes = new HashSet<CarNodeScript>();
        foreach (var node in GetComponentsInChildren<CarNodeScript>()) {
            AllCarNodes.Add(node);
            if (node.NodeColorSO.ColorID == 1) Color1Count++;
            else if (node.NodeColorSO.ColorID == 2) Color2Count++;
        }

        FindObjectOfType<SpawnerScript>().InitiliazeSpawnerCars(Color1Count,Color2Count);
    }

}
