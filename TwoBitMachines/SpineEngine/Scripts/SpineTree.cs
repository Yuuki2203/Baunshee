using System.Collections.Generic;
using UnityEngine;

namespace TwoBitMachines.SpineEngine
{
    [System.Serializable]
    public class SpineTree
    {
        [SerializeField] public List<SpineState> states = new List<SpineState>();
        [SerializeField] public bool directionChanged = false;
        [SerializeField] public bool direction = false;

        [System.NonSerialized] public SpineEngine engine;
        [System.NonSerialized] public int currentStateIndex = -1;
        [System.NonSerialized] public SpineState currentState;

        public void Initialize(SpineEngine target)
        {
            engine = target;
        }

        public void FindNextAnimation()
        {
            if (currentState != null)
            {
                currentState.Execute(engine);
            }
        }

        public void ClearSignals()
        {
            if (currentState != null)
            {
                currentState.ClearSignals();
            }
        }

        public void SetState(string stateName)
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].name == stateName)
                {
                    currentStateIndex = i;
                    currentState = states[i];
                    currentState.Initialize(engine);
                    return;
                }
            }
        }

        public void SetState(int index)
        {
            if (index >= 0 && index < states.Count)
            {
                currentStateIndex = index;
                currentState = states[index];
                currentState.Initialize(engine);
            }
        }

        public SpineState GetState(string stateName)
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].name == stateName)
                {
                    return states[i];
                }
            }
            return null;
        }

        public SpineState GetState(int index)
        {
            if (index >= 0 && index < states.Count)
            {
                return states[index];
            }
            return null;
        }

        public void AddState(SpineState state)
        {
            if (!states.Contains(state))
            {
                states.Add(state);
            }
        }

        public void RemoveState(SpineState state)
        {
            if (states.Contains(state))
            {
                states.Remove(state);
            }
        }

        public void RemoveState(string stateName)
        {
            for (int i = states.Count - 1; i >= 0; i--)
            {
                if (states[i].name == stateName)
                {
                    states.RemoveAt(i);
                }
            }
        }

        public void RemoveState(int index)
        {
            if (index >= 0 && index < states.Count)
            {
                states.RemoveAt(index);
            }
        }

        public void ClearStates()
        {
            states.Clear();
            currentStateIndex = -1;
            currentState = null;
        }

        public int GetStateCount()
        {
            return states.Count;
        }

        public string GetCurrentStateName()
        {
            return currentState?.name ?? "";
        }

        public int GetCurrentStateIndex()
        {
            return currentStateIndex;
        }
    }

    [System.Serializable]
    public class SpineState
    {
        [SerializeField] public string name = "";
        [SerializeField] public string animationName = "";
        [SerializeField] public List<SpineCondition> conditions = new List<SpineCondition>();
        [SerializeField] public List<SpineAction> actions = new List<SpineAction>();

        [System.NonSerialized] public bool isActive = false;
        [System.NonSerialized] public float activeTime = 0f;

        public void Initialize(SpineEngine engine)
        {
            isActive = true;
            activeTime = 0f;

            // アニメーションの設定
            if (!string.IsNullOrEmpty(animationName))
            {
                engine.SetNewAnimation(animationName);
            }

            // アクションの実行
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Execute(engine);
            }
        }

        public void Execute(SpineEngine engine)
        {
            if (!isActive)
            {
                return;
            }

            activeTime += Time.deltaTime;

            // 条件のチェック
            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i].Check(engine))
                {
                    // 条件が満たされた場合の処理
                    conditions[i].Execute(engine);
                }
            }
        }

        public void ClearSignals()
        {
            for (int i = 0; i < conditions.Count; i++)
            {
                conditions[i].ClearSignals();
            }
        }

        public void Deactivate()
        {
            isActive = false;
            activeTime = 0f;
        }
    }

    [System.Serializable]
    public class SpineCondition
    {
        [SerializeField] public string name = "";
        [SerializeField] public ConditionType conditionType = ConditionType.Always;
        [SerializeField] public float value = 0f;
        [SerializeField] public string targetAnimation = "";
        [SerializeField] public float duration = 0f;
        [SerializeField] public bool useSignal = false;
        [SerializeField] public string signalName = "";

        public enum ConditionType
        {
            Always,
            Time,
            AnimationComplete,
            Signal,
            Custom
        }

        public bool Check(SpineEngine engine)
        {
            switch (conditionType)
            {
                case ConditionType.Always:
                    return true;
                case ConditionType.Time:
                    return engine.player.GetCurrentTime() >= value;
                case ConditionType.AnimationComplete:
                    return engine.player.IsPlaying() == false;
                case ConditionType.Signal:
                    return useSignal && !string.IsNullOrEmpty(signalName);
                case ConditionType.Custom:
                    return CheckCustom(engine);
                default:
                    return false;
            }
        }

        public void Execute(SpineEngine engine)
        {
            if (!string.IsNullOrEmpty(targetAnimation))
            {
                engine.SetNewAnimation(targetAnimation);
            }
        }

        public void ClearSignals()
        {
            // 信号のクリア処理
        }

        private bool CheckCustom(SpineEngine engine)
        {
            // カスタム条件のチェック
            return false;
        }
    }

    [System.Serializable]
    public class SpineAction
    {
        [SerializeField] public string name = "";
        [SerializeField] public ActionType actionType = ActionType.SetAnimation;
        [SerializeField] public string targetAnimation = "";
        [SerializeField] public float value = 0f;
        [SerializeField] public bool boolValue = false;
        [SerializeField] public string stringValue = "";

        public enum ActionType
        {
            SetAnimation,
            SetSpeed,
            SetLoop,
            SetDirection,
            Pause,
            Resume,
            Stop,
            Custom
        }

        public void Execute(SpineEngine engine)
        {
            switch (actionType)
            {
                case ActionType.SetAnimation:
                    if (!string.IsNullOrEmpty(targetAnimation))
                    {
                        engine.SetNewAnimation(targetAnimation);
                    }
                    break;
                case ActionType.SetSpeed:
                    engine.player.SetSpeed(value);
                    break;
                case ActionType.SetLoop:
                    engine.player.SetLoop(boolValue);
                    break;
                case ActionType.SetDirection:
                    engine.SetDirection(boolValue);
                    break;
                case ActionType.Pause:
                    engine.player.Pause();
                    break;
                case ActionType.Resume:
                    engine.player.Resume();
                    break;
                case ActionType.Stop:
                    engine.player.Stop();
                    break;
                case ActionType.Custom:
                    ExecuteCustom(engine);
                    break;
            }
        }

        private void ExecuteCustom(SpineEngine engine)
        {
            // カスタムアクションの実行
        }
    }
} 