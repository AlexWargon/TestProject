using System;

[EcsComponent] public struct StaticTag{}
[EcsComponent] public struct TransformComponent : IEquatable<TransformComponent> {
    public UnityEngine.Vector3 Position;
    public UnityEngine.Vector3 Scale;
    public UnityEngine.Quaternion Rotation;

    public UnityEngine.Vector3 right => Rotation * UnityEngine.Vector3.right;
    public UnityEngine.Vector3 down => Rotation * UnityEngine.Vector3.right;

    public UnityEngine.Vector3 up {
        get => Rotation * UnityEngine.Vector3.up;
        set => Rotation = UnityEngine.Quaternion.FromToRotation(UnityEngine.Vector3.up, value);
    }
    public void Rorate(UnityEngine.Vector3 eulers) {
        UnityEngine.Quaternion quaternion = UnityEngine.Quaternion.Euler(eulers.x, eulers.y, eulers.z);
        Rotation *= UnityEngine.Quaternion.Inverse(this.Rotation) * quaternion * this.Rotation;
    }

    public void RotateAround(UnityEngine.Vector3 pos, UnityEngine.Vector3 dir, float angle) {
        
    }

    public bool Equals(TransformComponent other) {
        return Position == other.Position && Scale == other.Scale && Rotation == other.Rotation;
    }
    public static UnityEngine.Vector3 Down(UnityEngine.Quaternion rotation) => rotation * UnityEngine.Vector3.right;
}
[EcsComponent] public struct TransformRef {
    public UnityEngine.Transform Value;
}
[EcsComponent] public struct NotSync{}