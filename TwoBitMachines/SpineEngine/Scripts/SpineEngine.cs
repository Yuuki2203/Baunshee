using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace TwoBitMachines.SpineEngine
{
    [AddComponentMenu("Flare Engine/SpineEngine/SpineEngine")]
    [DisallowMultipleComponent, RequireComponent(typeof(SkeletonAnimation))]
    public class SpineEngine : SpineEngineBase
    {
        [SerializeField] public List<SpineAnimationPacket> animations = new List<SpineAnimationPacket>();
        [SerializeField] public SpineSkinSwap skinSwap;

        [System.NonSerialized] public SpinePlayer player = new SpinePlayer();
        [System.NonSerialized] private int currentIndex = -1;
        private SpineAnimationPacket animation => animations[currentIndex];

        public void Awake()
        {
            player.Initialize(transform);
            SpineManager.get.Register(this);
            tree.Initialize(this);
            skinSwap?.Initialize(animations);

            if (setToFirst && animations.Count > 0)
            {
                SetNewAnimation(animations[0].name);
            }
        }

        public override void SetFirstAnimation()
        {
            currentIndex = -1;
            currentAnimation = "";
            gameObject.SetActive(true);
            if (animations.Count > 0)
            {
                SetNewAnimation(animations[0].name);
            }
        }

        public override void Play()
        {
            if (pause || !enabled)
            {
                return;
            }

            tree.FindNextAnimation();
            OnChangedDirection();
            player.Play();
            tree.ClearSignals();
        }

        public override void SetNewAnimation(string newAnimation)
        {
            if (currentAnimation == newAnimation)
            {
                return;
            }

            for (int i = 0; i < animations.Count; i++)
            {
                if (animations[i].name == newAnimation)
                {
                    SpineAnimationPacket newAnim = animations[i].isRandom ? GetRandom(animations[i], newAnimation) : animations[i];

                    int oldIndex = currentIndex;
                    currentIndex = i;
                    if (newAnim.useTransition && newAnim.Transition(animations, tree, currentAnimation, out SpineAnimationPacket transitionAnimation))
                    {
                        currentAnimation = newAnimation;
                        player.SetAnimation(transitionAnimation);
                        player.SetNextAnimation(newAnim);
                    }
                    else
                    {
                        currentAnimation = newAnimation;
                        if (oldIndex >= 0 && oldIndex < animations.Count && animations[oldIndex].canSync && animations[oldIndex].syncID == newAnim.syncID)
                        {
                            player.SetAnimationSync(newAnim);
                            return;
                        }
                        player.SetAnimation(newAnim);
                    }
                    return;
                }
            }
        }

        private SpineAnimationPacket GetRandom(SpineAnimationPacket currentAnim, string newAnimation)
        {
            string randomAnimation = null;// RandomAnimation.Get(currentAnim.randomAnimations, newAnimation);
            if (randomAnimation != newAnimation)
            {
                for (int i = 0; i < animations.Count; i++)
                {
                    if (animations[i].name == randomAnimation)
                    {
                        return animations[i];
                    }
                }
            }
            return currentAnim;
        }

        private void OnChangedDirection()
        {
            if (tree.directionChanged)
            {
                player.FlipAnimation(tree.direction);
                tree.directionChanged = false;
            }
        }

        public SpineAnimationPacket GetAnimation(string animationName)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                if (animations[i].name == animationName)
                {
                    return animations[i];
                }
            }
            return null;
        }

        public override bool FlipAnimation(Dictionary<string, bool> signal, string signalName, string direction)
        {
            if (signal.ContainsKey(signalName))
            {
                bool flip = signal[signalName];
                player.FlipAnimation(flip);
                return true;
            }
            return false;
        }

        public void SkinSwap(string skinName)
        {
            skinSwap?.SwapSkin(skinName);
        }

        private void OnDrawGizmosSelected()
        {
            // デバッグ用のギズモ表示
        }
    }
} 