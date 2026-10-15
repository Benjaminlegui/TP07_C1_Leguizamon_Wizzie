using UnityEngine;

public abstract class StateConfigSO : ScriptableObject
{
    [SerializeField] private AnimationClip animationClip;

    public AnimationClip AnimationClip => animationClip;
}
