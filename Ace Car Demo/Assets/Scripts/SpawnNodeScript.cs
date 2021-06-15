using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodeScript : MonoBehaviour {
    public GameColorSO SpawnerColorSO;
    public SpawnerScript Spawner;

    public CarScript WaitingCar;
    public CarNodeScript FirstNodeAhead;
    public CarNodeScript AlternateNodeAhead;

    public void GateRaised(int colorid) {
        if (WaitingCar == null) return;

        if (SpawnerColorSO.ColorID == colorid) {
            List<CarNodeScript> StraightPath = FirstNodeAhead.GetStraightPath();

            if (StraightPath == null && AlternateNodeAhead != null) {
                StraightPath = AlternateNodeAhead.GetStraightPath();
            }

            if (StraightPath != null) {
                WaitingCar.SetMoveDestination(StraightPath[StraightPath.Count - 1].transform);
                WaitingCar = Spawner.SpawnByColorID(colorid);
                WaitingCar?.SetMoveDestination(this.transform);

            }

        }
    }

    public void InitiliazeWaitNode() {
        WaitingCar = Spawner.SpawnByColorID(SpawnerColorSO.ColorID);
        WaitingCar.SetMoveDestination(this.transform);

    }

}
