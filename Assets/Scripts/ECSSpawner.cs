using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ECSSpawner : MonoBehaviour {

	public EntityManager manager;
	public GameObject spawnPrefab;

	protected SpawnVariables vars;

	void Start(){
		vars = GameObject.FindObjectOfType<SpawnVariables>();
		manager = World.Active.GetOrCreateManager<EntityManager>();
		AddObjects(vars.objectCount);
	}

	private void AddObjects(int amount){
		// This just stores references
		NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
		
		// This strips all of the components FROM a game object to make a new Entity 
		manager.Instantiate(spawnPrefab, entities);
		
		for(int i = 0; i < amount; i++){
			Vector2 pos = UnityEngine.Random.insideUnitCircle;

			float offset = (float)i;
			float y = offset * vars.bounceHeight * vars.bounceSpeed;

			manager.SetComponentData(entities[i], new Position { Value = new float3(pos.x, y, pos.y) * vars.placementRadius });
			manager.SetComponentData(entities[i], new MoveSpeed { speed = (vars.movementSpeed + (float)(i % Constants.randomMod)) });
				
			// Yuck!
			if(manager.HasComponent<CircleBounceECSWithJobsData>(entities[i])){
				manager.SetComponentData(entities[i], new CircleBounceECSWithJobsData{ speed = vars.bounceSpeed, height = vars.bounceHeight, bounceOffset = offset });

			} else if(manager.HasComponent<CircleBounceECSData>(entities[i])){
				manager.SetComponentData(entities[i], new CircleBounceECSData{ speed = vars.bounceSpeed, height = vars.bounceHeight, bounceOffset = offset });
	
			}
		}
		// If you need to keep the object references, don't dispose of this
		entities.Dispose();
	}
}
