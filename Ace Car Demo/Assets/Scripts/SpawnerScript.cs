using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject CarPrefab;
    public GameColorSO Color1SO;
    public GameColorSO Color2SO;

    [SerializeField] private Transform SpawnPos1;
    [SerializeField] private Transform SpawnPos2;
    [SerializeField] private Transform WaitNode1;
    [SerializeField] private Transform WaitNode2;

    private Stack<GameObject> Color1Stack;
    private Stack<GameObject> Color2Stack;

    public void InitiliazeSpawnerCars(int Color1Count,int Color2Count) {
        Color1Stack = new Stack<GameObject>();
        Color2Stack = new Stack<GameObject>();

        for (int i = 0; i < Color1Count+Color2Count; i++) {
            var obj = Instantiate(CarPrefab,this.transform).GetComponent<CarScript>();

            if (i < Color1Count) {
                obj.SetColorSO(Color1SO);
                obj.transform.position = SpawnPos1.position;
                Color1Stack.Push(obj.gameObject);
            } else {
                obj.SetColorSO(Color2SO);
                obj.transform.position = SpawnPos2.position;
                Color2Stack.Push(obj.gameObject);
            }

            obj.gameObject.SetActive(false);
        }


        WaitNode1.GetComponent<SpawnNodeScript>()?.InitiliazeWaitNode();
        WaitNode2.GetComponent<SpawnNodeScript>()?.InitiliazeWaitNode();
    }



    public CarScript SpawnByColorID(int id) {
        if(id == Color1SO.ColorID) {
            if (Color1Stack.Count > 0) {
                var car = Color1Stack.Pop().GetComponent<CarScript>();
                car.gameObject.SetActive(true);
                car.InitializeCar();
                //car.SetMoveDestination(WaitNode1);
                return car;
            }

        }else if (id == Color2SO.ColorID) {
            if (Color2Stack.Count > 0) {
                var car = Color2Stack.Pop().GetComponent<CarScript>();
                car.gameObject.SetActive(true);
                car.InitializeCar();
                //car.SetMoveDestination(WaitNode2);
                return car;
            }
        }

        return null;
    }


}
