using System.Collections.Generic;
using UnityEngine;

namespace TwoBitMachines.SpineEngine
{
    [System.Serializable]
    public class SpineStateSignals
    {
        [SerializeField] public List<SpineSignal> signals = new List<SpineSignal>();
        [SerializeField] public bool useGlobalSignals = true;

        [System.NonSerialized] public Dictionary<string, bool> globalSignals = new Dictionary<string, bool>();
        [System.NonSerialized] public Dictionary<string, float> globalValues = new Dictionary<string, float>();
        [System.NonSerialized] public Dictionary<string, string> globalStrings = new Dictionary<string, string>();

        public void Initialize()
        {
            globalSignals.Clear();
            globalValues.Clear();
            globalStrings.Clear();
        }

        public void SetSignal(string signalName, bool value)
        {
            if (useGlobalSignals)
            {
                globalSignals[signalName] = value;
            }
        }

        public void SetValue(string signalName, float value)
        {
            if (useGlobalSignals)
            {
                globalValues[signalName] = value;
            }
        }

        public void SetString(string signalName, string value)
        {
            if (useGlobalSignals)
            {
                globalStrings[signalName] = value;
            }
        }

        public bool GetSignal(string signalName)
        {
            if (useGlobalSignals && globalSignals.ContainsKey(signalName))
            {
                return globalSignals[signalName];
            }
            return false;
        }

        public float GetValue(string signalName)
        {
            if (useGlobalSignals && globalValues.ContainsKey(signalName))
            {
                return globalValues[signalName];
            }
            return 0f;
        }

        public string GetString(string signalName)
        {
            if (useGlobalSignals && globalStrings.ContainsKey(signalName))
            {
                return globalStrings[signalName];
            }
            return "";
        }

        public void ClearSignal(string signalName)
        {
            if (useGlobalSignals)
            {
                if (globalSignals.ContainsKey(signalName))
                {
                    globalSignals.Remove(signalName);
                }
                if (globalValues.ContainsKey(signalName))
                {
                    globalValues.Remove(signalName);
                }
                if (globalStrings.ContainsKey(signalName))
                {
                    globalStrings.Remove(signalName);
                }
            }
        }

        public void ClearAllSignals()
        {
            if (useGlobalSignals)
            {
                globalSignals.Clear();
                globalValues.Clear();
                globalStrings.Clear();
            }
        }

        public SpineSignal GetSignalData(string signalName)
        {
            for (int i = 0; i < signals.Count; i++)
            {
                if (signals[i].name == signalName)
                {
                    return signals[i];
                }
            }
            return null;
        }

        public void AddSignal(SpineSignal signal)
        {
            if (!signals.Contains(signal))
            {
                signals.Add(signal);
            }
        }

        public void RemoveSignal(SpineSignal signal)
        {
            if (signals.Contains(signal))
            {
                signals.Remove(signal);
            }
        }

        public void RemoveSignal(string signalName)
        {
            for (int i = signals.Count - 1; i >= 0; i--)
            {
                if (signals[i].name == signalName)
                {
                    signals.RemoveAt(i);
                }
            }
        }

        public void ClearSignals()
        {
            signals.Clear();
        }

        public int GetSignalCount()
        {
            return signals.Count;
        }
    }

    [System.Serializable]
    public class SpineSignal
    {
        [SerializeField] public string name = "";
        [SerializeField] public SignalType signalType = SignalType.Bool;
        [SerializeField] public bool boolValue = false;
        [SerializeField] public float floatValue = 0f;
        [SerializeField] public string stringValue = "";
        [SerializeField] public bool useGlobal = true;

        public enum SignalType
        {
            Bool,
            Float,
            String
        }

        public void SetValue(bool value)
        {
            if (signalType == SignalType.Bool)
            {
                boolValue = value;
            }
        }

        public void SetValue(float value)
        {
            if (signalType == SignalType.Float)
            {
                floatValue = value;
            }
        }

        public void SetValue(string value)
        {
            if (signalType == SignalType.String)
            {
                stringValue = value;
            }
        }

        public bool GetBoolValue()
        {
            return boolValue;
        }

        public float GetFloatValue()
        {
            return floatValue;
        }

        public string GetStringValue()
        {
            return stringValue;
        }

        public void Reset()
        {
            boolValue = false;
            floatValue = 0f;
            stringValue = "";
        }
    }
} 