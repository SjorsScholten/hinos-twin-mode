# Game Mechanic: Movement

The player can move the character.

The movement feels different depending on what character is controlling the body.
Type-A is fast and responsive, Type-B is slow and floaty.

How does the player move the character?
- On game-pad the player will use the left analog stick.
- On M&K the player will use the 'WASD'-keys.

How can we modify our behavior to make the character feel floaty or responsive?
- We can achieve this effect by fiddling with the acceleration. A high acceleration will make the character feel responsive. A low acceleration will make the character feel more floaty.

```C#

public class MovementType : ScriptableObject
{
    public float baseMoveSpeed;
    public float baseAcceleration;
}

public class MovementBehaviour : MonoBehaviour
{
    public MovementType movementType;

    class MovementInfo
    {
        public Vector2 velocity;
        public Vector2 heading, crossHeading;
    }

    private void FixedUpdate() {

    }

    private Vector2 CalculateVelocityChange() {

    }

    public void MoveTowards(Vector2 direction) {

    }

    public void StopMoving() {

    }

    public void SwitchMovementType(MovementType movementType) {
        
    }
}

public class MovementController : MonoBehaviour
{
    public void MoveTowards(Vector2 direction) {

    }

    public void StopMoving() {

    }

    public void SwitchMovementType(MovementType movementType) {

    }
}

```