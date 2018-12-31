using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Mathematics;

public class CircleBounceJobs : MonoBehaviour {
	protected SpawnVariables vars;
	public GameObject spawnPrefab;
	protected Transform[] transforms;

	private TransformAccessArray transformsAccessArray;

	private NativeArray<float> movementSpeedArray;
	private NativeArray<float> offsetArray;

	private PositionUpdateJob positionJob;
	private JobHandle positionJobHandle;

	void Start() {
		vars = GameObject.FindObjectOfType<SpawnVariables>();
		transforms = new Transform[vars.objectCount];

		int amount = vars.objectCount;

		movementSpeedArray = new NativeArray<float>(amount, Allocator.Persistent);

		MovementSpeedCreationJob movementJob = new MovementSpeedCreationJob() {
			speeds = movementSpeedArray,
			movementSpeed = vars.movementSpeed,
		};

		// 64: Higher values work best when there's less work in the job to do
		JobHandle movementJobHandle = movementJob.Schedule(amount, 64);
		movementJobHandle.Complete();

		offsetArray = new NativeArray<float>(amount, Allocator.Persistent);

		OffsetCreationJob offsetJob = new OffsetCreationJob() {
			offset = offsetArray,
		};

		// 64: Higher values work best when there's less work in the job to do
		JobHandle offsetJobHandle = offsetJob.Schedule(amount, 64);
		offsetJobHandle.Complete();
			
		for(int i = 0; i < amount; i++) {
			Vector2 pos = UnityEngine.Random.insideUnitCircle;
			float y = offsetArray[i] * vars.bounceHeight * vars.bounceSpeed;
			Vector3 startingPosition = new Vector3(pos.x, y, pos.y) * vars.placementRadius;
			GameObject obj = Instantiate<GameObject>(spawnPrefab, startingPosition, Quaternion.identity);
				
			transforms[i] = obj.transform;
		}

		transformsAccessArray = new TransformAccessArray(transforms);
	}

	// Make the initial bounce height offsets
	struct OffsetCreationJob : IJobParallelFor {
		public NativeArray<float> offset;
		public void Execute(int i) {
			// Want: Random.value
			offset[i] = (float)i;
		}
	}

	// Make the initial movement speed amounts
	struct MovementSpeedCreationJob : IJobParallelFor {
		[ReadOnly] public float movementSpeed;
		public NativeArray<float> speeds;

		public void Execute(int i) {
			// Want: Random.value
			speeds[i] = movementSpeed + (float)(i % Constants.randomMod);
		}
	}

	struct PositionUpdateJob : IJobParallelForTransform {

		[ReadOnly] public NativeArray<float> offset;
		[ReadOnly] public NativeArray<float> movementSpeeds;
		[ReadOnly] public float bounceSpeed;
		[ReadOnly] public float bounceHeight;

		[ReadOnly] public float deltaTime;
		[ReadOnly] public float totalTime;

		public void Execute(int i, TransformAccess transform) {
			float bounceOffset = offset[i];
			float sinTime = Mathf.Sin(totalTime + bounceOffset);
			float movementSpeedAdjusted = movementSpeeds[i] * deltaTime;
			Vector3 prevPosition = transform.position;

			prevPosition.x += sinTime * movementSpeedAdjusted;
			prevPosition.z += Mathf.Cos(totalTime + bounceOffset) * movementSpeedAdjusted;

			prevPosition.y = Mathf.Abs(sinTime) * bounceHeight * bounceSpeed;
			transform.position = prevPosition;
		}
	}

	public void Update() {

		positionJob = new PositionUpdateJob() {
			offset = offsetArray,
			movementSpeeds = movementSpeedArray,
			bounceSpeed = vars.bounceSpeed,
			bounceHeight = vars.bounceHeight,
			deltaTime = Time.deltaTime,
			totalTime = Time.timeSinceLevelLoad,
		};

		positionJobHandle = positionJob.Schedule(transformsAccessArray);
	}

	public void LateUpdate() {
		positionJobHandle.Complete();
	}

	private void OnDestroy() {
		transformsAccessArray.Dispose();
		offsetArray.Dispose();
		movementSpeedArray.Dispose();
	}
}