using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public enum MoveType { Distance, Time }
    public GameColorSO ColorSO;
    public VoidEvent CarCollisionEvent;
    public MoveType MovementType;
    [Header("Distance based Movement")]
    public float MovementDurationPerUnitDistance = 0.04f;
    [Header("Time based Movement")]
    public float MovementTotalDuration = 1f;
    public float RotationDuration = 0.6f;


    [SerializeField] MeshRenderer MRenderer_;

    public void SetColorSO(GameColorSO so) {
        ColorSO = so;
    }

    public void InitializeCar() {
        var models = GetComponentsInChildren<MeshRenderer>();
        var pickedModel = models[Random.Range(0,models.Length)];
        foreach (var model in models) {
            if (model != pickedModel) {
                Destroy(model.gameObject);
            } else {
                model.gameObject.SetActive(true);
                MRenderer_ = model;
                MRenderer_.material = ColorSO.baseMaterial;
            }
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.GetComponent<CarScript>() != null) {
            Debug.Log("Collision!");
            CarCollisionEvent?.Raise(new Void());
        }
    }

    public void SetMoveDestination(Transform Node) {
        StartCoroutine(MoveToTargetXZ(Node));
    }

    public void RotateToTarget(Transform Node) {
        StartCoroutine(RotateOnY(Node));

    }

    private IEnumerator RotateOnY(Transform Node) {
        var startRot = this.transform.rotation;
        var euler = Node.rotation.eulerAngles;
        var target = Quaternion.Euler(0, euler.y, 0);
        
        float timer = RotationDuration;
        float timer_counter = timer;
        while (timer_counter > 0) {
            timer_counter -= Time.deltaTime;
            float CompletionPercent = 1 - timer_counter / timer;
            float easedValue = easeInOutSine(CompletionPercent);

            this.transform.rotation = Quaternion.Slerp(startRot,target,easedValue);
            yield return new WaitForEndOfFrame();
        }

    }


    private IEnumerator MoveToTargetXZ(Transform Node) {
        var distance = Vector3.Distance(this.transform.position,Node.position);
        float timer = 0f;

        if (MovementType == MoveType.Distance) {
            timer = MovementDurationPerUnitDistance * distance;
        }else if(MovementType == MoveType.Time) {
            timer = MovementTotalDuration;
        }

        var carNode = Node.GetComponent<CarNodeScript>();
        //Prevents collisions
        //if(carNode!=null) carNode.isOccupied = true;

        float timer_counter = timer;
        Vector3 firstPos = this.transform.position;

        while (timer_counter > 0) {
            timer_counter -= Time.deltaTime;
            float CompletionPercent = 1 - timer_counter / timer;
            float easedValue = easeInOutSine(CompletionPercent);

            this.transform.position = Vector3.Lerp(firstPos,Node.position, easedValue);
            

            yield return new WaitForEndOfFrame();
        }

        carNode?.Occupy(this);

    }


    private float easeInOutSine(float percent) {
        return (Mathf.Cos(Mathf.PI * percent) - 1f) / -2f;

    }

}
