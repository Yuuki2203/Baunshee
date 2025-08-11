using UnityEngine;
using Spine.Unity;

namespace TwoBitMachines.SpineEngine
{
    [System.Serializable]
    public class SpinePlayer
    {
        [System.NonSerialized] private Transform transform;
        [System.NonSerialized] private SkeletonAnimation skeletonAnimation;
        [System.NonSerialized] private SpineAnimationPacket currentAnimation;
        [System.NonSerialized] private SpineAnimationPacket nextAnimation;
        [System.NonSerialized] private bool isTransitioning = false;
        [System.NonSerialized] private float transitionTime = 0f;
        [System.NonSerialized] private float transitionDuration = 0.2f;

        public void Initialize(Transform target)
        {
            transform = target;
            skeletonAnimation = target.GetComponent<SkeletonAnimation>();
            if (skeletonAnimation == null)
            {
                Debug.LogError("SpinePlayer: SkeletonAnimation component not found on " + target.name);
            }
        }

        public void SetAnimation(SpineAnimationPacket animation)
        {
            if (skeletonAnimation == null || animation == null)
            {
                return;
            }

            currentAnimation = animation;
            isTransitioning = false;
            nextAnimation = null;

            // Spineアニメーションの設定
            if (!string.IsNullOrEmpty(animation.animationName))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, animation.animationName, animation.loop);
                skeletonAnimation.AnimationState.GetCurrent(0).TimeScale = animation.speed;
            }

            // フリップ設定
            if (animation.flipX)
            {
                skeletonAnimation.Skeleton.FlipX = true;
            }
            if (animation.flipY)
            {
                skeletonAnimation.Skeleton.FlipY = true;
            }

            // エクストラプロパティの適用
            animation.ApplyExtraProperties(transform.GetComponent<SpineEngine>());
        }

        public void SetAnimationSync(SpineAnimationPacket animation)
        {
            if (skeletonAnimation == null || animation == null)
            {
                return;
            }

            currentAnimation = animation;
            isTransitioning = false;
            nextAnimation = null;

            // 同期アニメーションの設定
            if (!string.IsNullOrEmpty(animation.animationName))
            {
                var currentTrack = skeletonAnimation.AnimationState.GetCurrent(0);
                if (currentTrack != null)
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, animation.animationName, animation.loop);
                    var newTrack = skeletonAnimation.AnimationState.GetCurrent(0);
                    newTrack.TimeScale = animation.speed;
                    newTrack.TrackTime = currentTrack.TrackTime; // 時間を同期
                }
            }

            // エクストラプロパティの適用
            animation.ApplyExtraProperties(transform.GetComponent<SpineEngine>());
        }

        public void SetNextAnimation(SpineAnimationPacket animation)
        {
            nextAnimation = animation;
            isTransitioning = true;
            transitionTime = 0f;
        }

        public void Play()
        {
            if (skeletonAnimation == null)
            {
                return;
            }

            // トランジション処理
            if (isTransitioning && nextAnimation != null)
            {
                transitionTime += Time.deltaTime;
                if (transitionTime >= transitionDuration)
                {
                    SetAnimation(nextAnimation);
                }
            }

            // 現在のアニメーションの更新
            if (currentAnimation != null)
            {
                currentAnimation.isPlaying = skeletonAnimation.AnimationState.GetCurrent(0) != null;
                currentAnimation.currentTime = skeletonAnimation.AnimationState.GetCurrent(0)?.TrackTime ?? 0f;
            }
        }

        public void FlipAnimation(bool flip)
        {
            if (skeletonAnimation != null)
            {
                skeletonAnimation.Skeleton.FlipX = flip;
            }
        }

        public void Pause()
        {
            if (skeletonAnimation != null)
            {
                skeletonAnimation.AnimationState.TimeScale = 0f;
            }
        }

        public void Resume()
        {
            if (skeletonAnimation != null && currentAnimation != null)
            {
                skeletonAnimation.AnimationState.TimeScale = currentAnimation.speed;
            }
        }

        public void Stop()
        {
            if (skeletonAnimation != null)
            {
                skeletonAnimation.AnimationState.ClearTracks();
            }
        }

        public bool IsPlaying()
        {
            if (skeletonAnimation == null)
            {
                return false;
            }

            var currentTrack = skeletonAnimation.AnimationState.GetCurrent(0);
            return currentTrack != null && !currentTrack.IsComplete;
        }

        public float GetCurrentTime()
        {
            if (skeletonAnimation == null)
            {
                return 0f;
            }

            var currentTrack = skeletonAnimation.AnimationState.GetCurrent(0);
            return currentTrack?.TrackTime ?? 0f;
        }

        public float GetDuration()
        {
            if (skeletonAnimation == null)
            {
                return 0f;
            }

            var currentTrack = skeletonAnimation.AnimationState.GetCurrent(0);
            return currentTrack?.Animation.Duration ?? 0f;
        }

        public void SetSpeed(float speed)
        {
            if (skeletonAnimation != null)
            {
                skeletonAnimation.AnimationState.TimeScale = speed;
            }
        }

        public void SetLoop(bool loop)
        {
            if (skeletonAnimation != null)
            {
                var currentTrack = skeletonAnimation.AnimationState.GetCurrent(0);
                if (currentTrack != null)
                {
                    currentTrack.Loop = loop;
                }
            }
        }
    }
} 