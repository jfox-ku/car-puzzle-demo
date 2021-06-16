using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarNodeScript : MonoBehaviour
{
    public enum NodeType { MovementNode, GameNode }
    [SerializeField] private SpriteRenderer SRenderer;

    [Header("SO Events")]
    [SerializeField] private Vector3Event MatchedPositionEvent;
    [SerializeField] private IntEvent CarSettledEvent; //Unlocks input
    [SerializeField] private VoidEvent WrongMatchEvent; //Game over

    [Header("Node related")]
    public GameColorSO NodeColorSO;

    public CarNodeScript NextNodeMain;
    public CarNodeScript NextNodeAlt;
    public NodeType NodeBehaviour;

    private CarScript CurrentCar;

    public bool isOccupied = false;



    private void Start() {
        if(NodeBehaviour == NodeType.GameNode) {
            SRenderer.color = NodeColorSO.GetColor();
        } else {
            Destroy(SRenderer);
        }
        
    }

    public CarNodeScript GetNextNode() {
        if (NextNodeMain != null && !NextNodeMain.isOccupied) {
            return NextNodeMain;
        } else if (NextNodeAlt != null && !NextNodeAlt.isOccupied) {
            return NextNodeAlt;
        } else return null; 
    }

    public CarNodeScript GetNextMainNode() {
        if (NextNodeMain != null && !NextNodeMain.isOccupied) {
            return NextNodeMain;
        } else return null;
    }

    public List<CarNodeScript> GetStraightPath() {
        List<CarNodeScript> Path = new List<CarNodeScript>();
        if (isOccupied) return null;
        if (NextNodeMain == null || NextNodeMain.isOccupied) {
            Path.Add(this);
            return Path;
        }

        var node = GetNextMainNode();
        while (node != null && !node.isOccupied) {
            Path.Add(node);
            node = node.GetNextMainNode();
        }

        
        if (Path.Count != 0) {
            return Path;
        } else return null;
    }

    public CarNodeScript GetPathEnd() {
        var Path = GetStraightPath();
        if (Path != null) {
            return Path[Path.Count - 1];
        } else return null;

    }

    public void Occupy(CarScript car) {
        CurrentCar = car;
        isOccupied = true;

        //Take main path
        if(NextNodeMain!=null && !NextNodeMain.isOccupied) {
            car.SetMoveDestination(NextNodeMain.GetPathEnd().transform);
            isOccupied = false;
            return;
        }

        //Take Alternate path
        if(NextNodeAlt!=null && !NextNodeAlt.isOccupied) {
            car.SetMoveDestination(NextNodeAlt.transform);
            isOccupied = false;
            //Set rotation here
            car.RotateToTarget(NextNodeAlt.transform);
            return;

        }

        //Settle
        if(NodeBehaviour == NodeType.GameNode) {
            if(NodeColorSO.ColorID == car.ColorSO.ColorID) {
                MatchedPositionEvent?.Raise(this.transform.position);
            } else {
                WrongMatchEvent.Raise(new Void());
            }


        }

    }


    private void OnDrawGizmos() {
        if (NextNodeMain != null) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position,NextNodeMain.transform.position);
        }

        if (NextNodeAlt != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, NextNodeAlt.transform.position);
        }
    }



}
