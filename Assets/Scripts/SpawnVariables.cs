using UnityEngine;

public class SpawnVariables : MonoBehaviour {
	[Tooltip("How many objects are spawned at launch")]
	public int objectCount = 10000;
	[Tooltip("How big of a radius are objects spawned")]
	public float placementRadius = 100f;
	[Tooltip("How fast that each object moves in x/z")]
	public float movementSpeed = 10f;
	[Tooltip("The maximum height of each bounce")]
	public float bounceHeight = 1f;
	[Tooltip("How fast that each object moves on the y axis")]
	public float bounceSpeed = 10f;
	
}
