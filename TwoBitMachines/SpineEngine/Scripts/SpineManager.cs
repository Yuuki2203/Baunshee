using System.Collections.Generic;
using UnityEngine;

namespace TwoBitMachines.SpineEngine
{
    public class SpineManager
    {
        [System.NonSerialized] public static List<SpineEngine> engines = new List<SpineEngine>();
        [System.NonSerialized] public static SpineManager get = new SpineManager();

        public void Register(SpineEngine engine)
        {
            if (!engines.Contains(engine))
            {
                engines.Add(engine);
            }
        }

        public void Unregister(SpineEngine engine)
        {
            if (engines.Contains(engine))
            {
                engines.Remove(engine);
            }
        }

        public void PlayAll()
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.Play();
            }
        }

        public void PauseAll()
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.Pause(true);
            }
        }

        public void ResumeAll()
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.Pause(false);
            }
        }

        public void SetAnimationAll(string animationName)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetNewAnimation(animationName);
            }
        }

        public void SetDirectionAll(bool flip)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(flip);
            }
        }

        public void SetDirectionAll(int direction)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(direction);
            }
        }

        public void SetDirectionAll(Transform target)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(target);
            }
        }

        public void SetDirectionAll(Vector2 position)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(position);
            }
        }

        public void SetDirectionAll(Vector3 position)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(position);
            }
        }

        public void SetDirectionAll(float x)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(x);
            }
        }

        public void SetDirectionAll(string signalName, bool value)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, value);
            }
        }

        public void SetDirectionAll(string signalName, int value)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, value);
            }
        }


        public void SetDirectionAll(string signalName, Transform target)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, target);
            }
        }

        public void SetDirectionAll(string signalName, Vector2 position)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, position);
            }
        }

        public void SetDirectionAll(string signalName, Vector3 position)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, position);
            }
        }

        public void SetDirectionAll(string signalName, float x)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, x);
            }
        }

        public void SetDirectionAll(string signalName, string value)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, value);
            }
        }

        public void SetDirectionAll(string signalName, bool value, bool useSignal)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, value, useSignal);
            }
        }

        public void SetDirectionAll(string signalName, int value, bool useSignal)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, value, useSignal);
            }
        }


        public void SetDirectionAll(string signalName, Transform target, bool useSignal)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, target, useSignal);
            }
        }

        public void SetDirectionAll(string signalName, Vector2 position, bool useSignal)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, position, useSignal);
            }
        }

        public void SetDirectionAll(string signalName, Vector3 position, bool useSignal)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, position, useSignal);
            }
        }

        public void SetDirectionAll(string signalName, float x, bool useSignal)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, x, useSignal);
            }
        }

        public void SetDirectionAll(string signalName, string value, bool useSignal)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i]?.SetDirection(signalName, value, useSignal);
            }
        }

        public void ClearAll()
        {
            engines.Clear();
        }

        public int GetEngineCount()
        {
            return engines.Count;
        }

        public SpineEngine GetEngine(int index)
        {
            if (index >= 0 && index < engines.Count)
            {
                return engines[index];
            }
            return null;
        }

        public SpineEngine GetEngine(string name)
        {
            for (int i = 0; i < engines.Count; i++)
            {
                if (engines[i] != null && engines[i].name == name)
                {
                    return engines[i];
                }
            }
            return null;
        }
    }
} 