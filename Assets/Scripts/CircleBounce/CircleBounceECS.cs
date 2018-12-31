using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Collections;

public class CircleBounceECS : ComponentSystem {

	// Find everything with Position, Rotation, and MoveSpeed
	struct ComponentGroup{
		public ComponentDataArray<Position> positions;
		[ReadOnly] public ComponentDataArray<MoveSpeed> moveSpeeds;
		[ReadOnly] public ComponentDataArray<CircleBounceECSData> bounce;
		public readonly int Length;
	}

	// Take the results of that query, and put it into this struct
	[Inject]
	ComponentGroup components;

	protected override void OnUpdate() {
		float deltaTime = Time.deltaTime;
		float totalTime = Time.timeSinceLevelLoad;

		for(int i = 0; i < components.Length; i++) {
			Position position = components.positions[i];			
			MoveSpeed movementSpeed = components.moveSpeeds[i];
			CircleBounceECSData bounce = components.bounce[i];

			float sinTime = math.sin(totalTime + bounce.bounceOffset);
			float movementSpeedAdjusted = movementSpeed.speed * deltaTime;
			float3 prevPosition = position.Value;

			prevPosition.x += sinTime * movementSpeedAdjusted;
			prevPosition.z += Mathf.Cos(totalTime + bounce.bounceOffset) * movementSpeedAdjusted;
			prevPosition.y = math.abs(sinTime) * bounce.height * bounce.speed;

			position.Value = prevPosition;
			components.positions[i] = position;
		}
	}
}

