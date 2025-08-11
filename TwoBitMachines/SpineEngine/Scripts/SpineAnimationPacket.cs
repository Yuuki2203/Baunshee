using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace TwoBitMachines.SpineEngine
{
    [System.Serializable]
    public class SpineAnimationPacket
    {
        [SerializeField] public string name = "";
        [SerializeField] public string animationName = "";
        [SerializeField] public bool loop = true;
        [SerializeField] public float speed = 1f;
        [SerializeField] public bool isRandom = false;
        [SerializeField] public List<string> randomAnimations = new List<string>();
        [SerializeField] public bool useTransition = false;
        [SerializeField] public string transitionAnimation = "";
        [SerializeField] public bool canSync = false;
        [SerializeField] public string syncID = "";
        [SerializeField] public bool flipX = false;
        [SerializeField] public bool flipY = false;
        [SerializeField] public List<SpineExtraProperty> extraProperties = new List<SpineExtraProperty>();

        [System.NonSerialized] public bool isPlaying = false;
        [System.NonSerialized] public float currentTime = 0f;

        public bool Transition(List<SpineAnimationPacket> animations, SpineTree tree, string currentAnimation, out SpineAnimationPacket transitionAnimation)
        {
            transitionAnimation = null;
            if (!useTransition)
            {
                return false;
            }

            for (int i = 0; i < animations.Count; i++)
            {
                if (animations[i].name == this.transitionAnimation)
                {
                    transitionAnimation = animations[i];
                    return true;
                }
            }

            return false;
        }

        public void ApplyExtraProperties(SpineEngine engine)
        {
            for (int i = 0; i < extraProperties.Count; i++)
            {
                extraProperties[i].Apply(engine);
            }
        }

        public void ResetExtraProperties(SpineEngine engine)
        {
            for (int i = 0; i < extraProperties.Count; i++)
            {
                extraProperties[i].Reset(engine);
            }
        }
    }

    [System.Serializable]
    public class SpineExtraProperty
    {
        [SerializeField] public string propertyName = "";
        [SerializeField] public PropertyType propertyType = PropertyType.Float;
        [SerializeField] public float floatValue = 0f;
        [SerializeField] public int intValue = 0;
        [SerializeField] public bool boolValue = false;
        [SerializeField] public string stringValue = "";
        [SerializeField] public Vector2 vector2Value = Vector2.zero;
        [SerializeField] public Vector3 vector3Value = Vector3.zero;
        [SerializeField] public Color colorValue = Color.white;

        public enum PropertyType
        {
            Float,
            Int,
            Bool,
            String,
            Vector2,
            Vector3,
            Color
        }

        public void Apply(SpineEngine engine)
        {
            // プロパティの適用ロジック
            // 実際の実装では、SpineのSkeletonAnimationにプロパティを適用
        }

        public void Reset(SpineEngine engine)
        {
            // プロパティのリセットロジック
        }
    }
} 