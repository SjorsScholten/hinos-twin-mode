using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 Cross(this Vector2 vector) {
        return new Vector2(vector.y, -vector.x);
    }
}