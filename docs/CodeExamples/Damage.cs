using UnityEngine;

namespace hinos.damage
{
    public interface IDamageHandler
    {
        void HandleDamage(DamageInfo damageInfo);
    }

    public struct DamageInfo
    {
        public float damageAmount;
    }

    public enum DamageType
    {
        NONE,
        // ...
    }

    [System.Serializable]
    public class Resource {
        [SerializeField] private float _value;

        public event Action OnValueChangedEvent;

        public float Value {
            get { return _value; }
            set { SetValue(value); }
        }

        public Resource(float value) {
            _value = value;
        }

        public void SetValue(float value) {
            _value = value;
            OnValueChangedEvent?.Invoke();
        }
    }

    public class HealthComponent : MonoBehaviour
    {
        public float maxHealth;
        public float initialHealth;

        private Resource _healthResource
    }
}