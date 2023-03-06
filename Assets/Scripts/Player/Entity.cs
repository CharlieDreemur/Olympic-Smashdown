using UnityEngine;

public class Entity : MonoBehaviour
{
    
}

public static class VectorHelperExtensions
{
    public static Vector3 Flatten(this Vector3 vector)
    {
        vector.y = 0.0f;
        return vector;
    }

    public static float Distance2D(this Vector3 from, in Vector3 to)
    {
        return Vector3.Distance(Flatten(from), Flatten(to));
    }

    public static Vector3 Direction2D(this Vector3 from, in Vector3 to)
    {
        return (Flatten(to) - Flatten(from)).normalized;
    }

    public static Vector3 RotateAboutUp(this Vector3 currentRot, in float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.up) * currentRot;
    }
}
