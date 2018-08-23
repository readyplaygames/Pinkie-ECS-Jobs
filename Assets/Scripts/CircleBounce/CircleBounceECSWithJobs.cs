using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Collections;

public class CircleBounceECSWithJobs : JobComponentSystem {

	// This can only have three!!
	struct MovementCurveJob : IJobProcessComponentData<Position, MoveSpeed, CircleBounceECSWithJobsData>{
		public float totalTime;
		public float deltaTime;

		public void Execute(ref Position position, [ReadOnly] ref MoveSpeed movementSpeed, ref CircleBounceECSWithJobsData bounce) {

			float sinTime = math.sin(totalTime + bounce.bounceOffset);
			float movementSpeedAdjusted = movementSpeed.speed * deltaTime;
			float3 prevPosition = position.Value;

			prevPosition.x += sinTime * movementSpeedAdjusted;
			prevPosition.z += Mathf.Cos(totalTime + bounce.bounceOffset) * movementSpeedAdjusted;
			prevPosition.y = math.abs(sinTime) * bounce.height * bounce.speed;
				
			position.Value = prevPosition;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies) {

		MovementCurveJob moveJob = new MovementCurveJob() {
			totalTime = Time.timeSinceLevelLoad,
			deltaTime = Time.deltaTime,
		};
		JobHandle moveHandle = moveJob.Schedule(this, 64, inputDependencies);

		return moveHandle;
	}
	
}
