using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace TwoBitMachines.SpineEngine
{
    [System.Serializable]
    public class SpineSkinSwap
    {
        [SerializeField] public List<SpineSkinData> skins = new List<SpineSkinData>();
        [SerializeField] public string defaultSkin = "";

        [System.NonSerialized] private Transform transform;
        [System.NonSerialized] private SkeletonAnimation skeletonAnimation;
        [System.NonSerialized] private string currentSkin = "";

        public void Initialize(List<SpineAnimationPacket> animations)
        {
            // 初期化処理
        }

        public void SwapSkin(string skinName)
        {
            if (skeletonAnimation == null)
            {
                return;
            }

            SpineSkinData skinData = GetSkin(skinName);
            if (skinData != null)
            {
                currentSkin = skinName;
                skeletonAnimation.Skeleton.SetSkin(skinData.skinName);
                skeletonAnimation.Skeleton.SetToSetupPose();
            }
        }

        public void SwapSkin(int skinIndex)
        {
            if (skinIndex >= 0 && skinIndex < skins.Count)
            {
                SwapSkin(skins[skinIndex].name);
            }
        }

        public void SetDefaultSkin()
        {
            if (!string.IsNullOrEmpty(defaultSkin))
            {
                SwapSkin(defaultSkin);
            }
        }

        public SpineSkinData GetSkin(string skinName)
        {
            for (int i = 0; i < skins.Count; i++)
            {
                if (skins[i].name == skinName)
                {
                    return skins[i];
                }
            }
            return null;
        }

        public SpineSkinData GetSkin(int index)
        {
            if (index >= 0 && index < skins.Count)
            {
                return skins[index];
            }
            return null;
        }

        public void AddSkin(SpineSkinData skin)
        {
            if (!skins.Contains(skin))
            {
                skins.Add(skin);
            }
        }

        public void RemoveSkin(SpineSkinData skin)
        {
            if (skins.Contains(skin))
            {
                skins.Remove(skin);
            }
        }

        public void RemoveSkin(string skinName)
        {
            for (int i = skins.Count - 1; i >= 0; i--)
            {
                if (skins[i].name == skinName)
                {
                    skins.RemoveAt(i);
                }
            }
        }

        public void RemoveSkin(int index)
        {
            if (index >= 0 && index < skins.Count)
            {
                skins.RemoveAt(index);
            }
        }

        public void ClearSkins()
        {
            skins.Clear();
        }

        public int GetSkinCount()
        {
            return skins.Count;
        }

        public string GetCurrentSkin()
        {
            return currentSkin;
        }

        public void SetSkeletonAnimation(SkeletonAnimation animation)
        {
            skeletonAnimation = animation;
        }
    }

    [System.Serializable]
    public class SpineSkinData
    {
        [SerializeField] public string name = "";
        [SerializeField] public string skinName = "";
        [SerializeField] public List<SpineAttachmentData> attachments = new List<SpineAttachmentData>();
        [SerializeField] public List<SpineSlotData> slots = new List<SpineSlotData>();

        public void Apply(SkeletonAnimation skeletonAnimation)
        {
            if (skeletonAnimation == null)
            {
                return;
            }

            // スキンの適用
            if (!string.IsNullOrEmpty(skinName))
            {
                skeletonAnimation.Skeleton.SetSkin(skinName);
            }

            // アタッチメントの適用
            for (int i = 0; i < attachments.Count; i++)
            {
                attachments[i].Apply(skeletonAnimation);
            }

            // スロットの適用
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].Apply(skeletonAnimation);
            }

            skeletonAnimation.Skeleton.SetToSetupPose();
        }
    }

    [System.Serializable]
    public class SpineAttachmentData
    {
        [SerializeField] public string slotName = "";
        [SerializeField] public string attachmentName = "";
        [SerializeField] public AttachmentType attachmentType = AttachmentType.Region;

        public enum AttachmentType
        {
            Region,
            Mesh,
            BoundingBox,
            Path,
            Point
        }

        public void Apply(SkeletonAnimation skeletonAnimation)
        {
            if (skeletonAnimation == null || string.IsNullOrEmpty(slotName) || string.IsNullOrEmpty(attachmentName))
            {
                return;
            }

            var slot = skeletonAnimation.Skeleton.FindSlot(slotName);
            if (slot != null)
            {
                var attachment = skeletonAnimation.Skeleton.GetAttachment(slotName, attachmentName);
                if (attachment != null)
                {
                    slot.Attachment = attachment;
                }
            }
        }
    }

    [System.Serializable]
    public class SpineSlotData
    {
        [SerializeField] public string slotName = "";
        [SerializeField] public Color color = Color.white;
        [SerializeField] public bool visible = true;

        public void Apply(SkeletonAnimation skeletonAnimation)
        {
            if (skeletonAnimation == null || string.IsNullOrEmpty(slotName))
            {
                return;
            }

            var slot = skeletonAnimation.Skeleton.FindSlot(slotName);
            if (slot != null)
            {
                slot.A = visible ? 1f : 0f;
            }
        }
    }
} 