using UnityEngine;

[CreateAssetMenu(fileName = "new characterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public string displayName;
    public string description;
    public Color color;

    public float maxSpeed;
    public float maxAcceleration;
    public float maxDeceleration;
    public float maxCorrection;
}
