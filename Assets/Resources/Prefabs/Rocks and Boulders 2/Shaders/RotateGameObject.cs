using UnityEngine;
using System.Collections;

public class RotateGameObject : MonoBehaviour {
	public float rot_speed_x=0;
	public float rot_speed_y=0;
	public float rot_speed_z=0;
	
	void FixedUpdate () {
        transform.Rotate(Time.fixedDeltaTime * new Vector3(rot_speed_x, rot_speed_y, rot_speed_z), Space.World);
    }
}
