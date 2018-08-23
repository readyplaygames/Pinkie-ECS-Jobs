using System;
using Unity.Entities;
using Unity.Mathematics;

public struct CircleBounceECSData : IComponentData {
	// We all set these at runtime, so [NonSerialized]
	[NonSerialized] public float speed;
	[NonSerialized] public float height;
	[NonSerialized] public float bounceOffset;
	[NonSerialized] public float3 target;
}
// Set it in the inspector
public class CircleBounceECSComponent : ComponentDataWrapper<CircleBounceECSData>{ }

