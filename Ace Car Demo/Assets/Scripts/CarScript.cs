using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public GameColorSO ColorSO;
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

    public void SetMoveDestination(Transform Node) {
        StartCoroutine(MoveToTargetXZ(Node));

    }


    private IEnumerator MoveToTargetXZ(Transform Node) {
        float timer = 2f;
        float timer_counter = timer;
        Vector3 firstPos = this.transform.position;

        while (timer_counter > 0) {
            timer_counter -= Time.deltaTime;
            this.transform.position = Vector3.Lerp(firstPos,Node.position,1-timer_counter/timer);
            

            yield return new WaitForEndOfFrame();
        }

    }

}
