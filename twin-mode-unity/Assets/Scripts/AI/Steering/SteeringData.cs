using UnityEngine;

public class SteeringData
{
    public Vector2 linear;
    public float angular;

    public static readonly SteeringData Empty = new(Vector2.zero, 0f);

    public SteeringData(Vector2 linear, float angular) {
        this.linear = linear;
        this.angular = angular;
    }
}
