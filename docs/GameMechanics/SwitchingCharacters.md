# Game Mechanic: Switching Characters

The player can switch between two characters.

1. <font color="cyan">The player performs a switch action</font>
2. <font color="orange">The character is switched</font>




```C#
public class CharacterMode : ScriptableObject
{
    // Meta data
    public string displayName;
    public string description;

    public MovementType movementType;
}

public class CharacterManager : MonoBehaviour
{
    public CharacterMode typeA, typeB;
    public CharacterMode currentMode;

    public event Action<CharacterMode> OnModeChangedEvent;

    public void ToggleCharacter() {
        if(currentMode == typeA) {
            currentMode = typeB;
        }
        else {
            currentMode = typeA;
        }

        OnModeChangedEvent?.Invoke();
    }
}

```