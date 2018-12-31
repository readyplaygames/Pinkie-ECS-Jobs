using System;
using Unity.Entities;
using Unity.Mathematics;

// For some reason, there's only 3 parameters for IJobProcessComponentData
// so we can't share the Data component
[Serializable]
public struct CircleBounceECSWithJobsData : IComponentData {
	// We all set these at runtime, so [NonSerialized]
	[NonSerialized] public float speed;
	[NonSerialized] public float height;
	[NonSerialized] public float bounceOffset;
	[NonSerialized] public float3 target;
}
// Set it in the inspector
public class CircleBounceECSWithJobsComponent : ComponentDataWrapper<CircleBounceECSWithJobsData> { }