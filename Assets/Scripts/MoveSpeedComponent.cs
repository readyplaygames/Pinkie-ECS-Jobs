using System;
using Unity.Entities;

[Serializable]
public struct MoveSpeed : IComponentData {
    [NonSerialized] public float speed;

}

public class MoveSpeedComponent : ComponentDataWrapper<MoveSpeed> { }