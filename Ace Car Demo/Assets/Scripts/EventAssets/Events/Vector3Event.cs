using UnityEngine;
using System.Collections;
using UnityEngine.Events;

//Event
[CreateAssetMenu(fileName = "New Vector3 Event", menuName = "Game Event/Vector3 Event")]
[System.Serializable] public class Vector3Event : BaseGameEvent<Vector3> { }


//Unity Event 
[System.Serializable] public class UnityVector3Event : UnityEvent<Vector3> { }

