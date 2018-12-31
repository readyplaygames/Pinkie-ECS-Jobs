using UnityEngine;
public class MonoSpawner : MonoBehaviour {

	protected SpawnVariables vars;
	public CircleBounceMono spawnPrefab;

	protected void Start() {
		vars = GameObject.FindObjectOfType<SpawnVariables>();
		AddObjects(vars.objectCount);
	}

	private void AddObjects(int amount){
		for(int i = 0; i < amount; i++){
			Vector2 pos = Random.insideUnitCircle;
			float bounceOffset = (float)i;
			float y = bounceOffset * vars.bounceHeight * vars.bounceSpeed;
			Vector3 startingPosition = new Vector3(pos.x, y, pos.y) * vars.placementRadius;

			// Set parent, position, and rotation all in one!
			// But setting the parent is actually not a good idea, having them in the root is faster
			CircleBounceMono obj = Instantiate<CircleBounceMono>(spawnPrefab, startingPosition, Quaternion.identity);
				
			obj.vars = vars;
				
			obj.bounceOffset = bounceOffset;
			obj.movementSpeed = vars.movementSpeed + (float)(i % Constants.randomMod);
			obj.transform.position = startingPosition;

			// Have it default to move toward its spawn point
			obj.target = startingPosition;
		}
	}
}
