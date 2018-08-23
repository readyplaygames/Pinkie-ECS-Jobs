using UnityEngine;
public class CircleBounceMono : MonoBehaviour {

	[System.NonSerialized]
	public SpawnVariables vars;
	[System.NonSerialized]
	public float bounceOffset;
	[System.NonSerialized]
	public float movementSpeed;
	[System.NonSerialized]
	public Vector3 target;

	void Update () {
		float sinTime = Mathf.Sin(Time.timeSinceLevelLoad + bounceOffset);
		float movementSpeedAdjusted = movementSpeed * Time.deltaTime;
		Vector3 prevPosition = transform.position;

		// Movement in circles
		prevPosition.x += sinTime * movementSpeedAdjusted;
		prevPosition.z += Mathf.Cos(Time.timeSinceLevelLoad + bounceOffset) * movementSpeedAdjusted;
		prevPosition.y = Mathf.Abs(sinTime) * vars.bounceHeight * vars.bounceSpeed;
		transform.position = prevPosition;
				
	}	
}
