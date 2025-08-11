using System.Collections.Generic;
using UnityEngine;

namespace TwoBitMachines.SpineEngine
{
    public abstract class SpineEngineBase : MonoBehaviour
    {
        [SerializeField] public bool pause = false;
        [SerializeField] public bool setToFirst = true;
        [SerializeField] public SpineTree tree = new SpineTree();
        [SerializeField] public bool enableFlip = true;

        [System.NonSerialized] public string currentAnimation = "";
        [System.NonSerialized] public bool directionChanged = false;

        public abstract void SetFirstAnimation();
        public abstract void Play();
        public abstract void SetNewAnimation(string newAnimation);
        public abstract bool FlipAnimation(Dictionary<string, bool> signal, string signalName, string direction);

        public virtual void Pause(bool value)
        {
            pause = value;
        }

        public virtual void SetDirection(bool flip)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = flip;
            }
        }

        public virtual void SetDirection(int direction)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = direction < 0;
            }
        }

        public virtual void SetDirection(Transform target)
        {
            if (enableFlip && target != null)
            {
                directionChanged = true;
                tree.direction = target.position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(Vector2 position)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(Vector3 position)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(float x)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = x < 0;
            }
        }

        public virtual void SetDirection(string signalName, bool value)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = value;
            }
        }

        public virtual void SetDirection(string signalName, int value)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = value < 0;
            }
        }

        public virtual void SetDirection(string signalName, Transform target)
        {
            if (enableFlip && target != null)
            {
                directionChanged = true;
                tree.direction = target.position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(string signalName, Vector2 position)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(string signalName, Vector3 position)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(string signalName, float x)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = x < 0;
            }
        }

        public virtual void SetDirection(string signalName, string value)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = value == "left";
            }
        }

        public virtual void SetDirection(string signalName, bool value, bool useSignal)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = value;
            }
        }

        public virtual void SetDirection(string signalName, int value, bool useSignal)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = value < 0;
            }
        }


        public virtual void SetDirection(string signalName, Transform target, bool useSignal)
        {
            if (enableFlip && target != null)
            {
                directionChanged = true;
                tree.direction = target.position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(string signalName, Vector2 position, bool useSignal)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(string signalName, Vector3 position, bool useSignal)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = position.x < transform.position.x;
            }
        }

        public virtual void SetDirection(string signalName, float x, bool useSignal)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = x < 0;
            }
        }

        public virtual void SetDirection(string signalName, string value, bool useSignal)
        {
            if (enableFlip)
            {
                directionChanged = true;
                tree.direction = value == "left";
            }
        }
    }
} 