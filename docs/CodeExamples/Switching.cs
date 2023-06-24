Using System;
Using UnityEngine;

namespace hinos.Modes
{
    public class Character : MonoBehaviour {
        // Meta data
        public string displayName;
        public string description;
        public Color color;

        public CharacterState stateObject;
    }

    public class RangedCharacter : MonoBehaviour {
        public float timeBetweenShots;

        private bool _fireHeld;
        private float _fireHoldTime;
        private float _timeSinceLastFire

        private void Update() {
            if(_fireHeld) {
                _fireHoldTime += Time.deltaTime;
            }

            var mouse = Mouse.current;
            if(mouse != null) {
                if(mouse.leftButton.wasPressedThisFrame) {
                    _fireHeld = true;
                    _fireHoldTime = 0;
                }

                if(mouse.leftButton.wasReleasedThisFrame) {
                    _fireHeld = false;
                }
            }

            if(_fireHeld && _timeSinceLastFire > timeBetweenShots) {
                
            }
        }
    }

    public class MeleeCharacter : MonoBehaviour {
        public HurtBox[] hurtboxes;
        private bool _attackRequested;

        private void Awake() {
            for(var i = 0; i < hurtboxes.Length; i++) {
                hurtboxes.OnHitEvent += OnHit;
            }
        }

        private float Update() {
            var mouse = Mouse.current;
            if(mouse != null) {
                if(mouse.leftButton.wasPressedThisFrame) {
                    _attackRequested |= true;
                }
            }

            if(_attackRequested) {
                //TODO: start attack animation
            }
        }

        private void OnHit(Collider2D other) {
            
        }
    }

    public class HurtBox : MonoBehaviour {
        public event Action<Collider2D> OnHitEvent;

        private void OnTriggerEnter2D(Collider2D other) {
            OnHitEvent?.Invoke(other);
        }
    }
}