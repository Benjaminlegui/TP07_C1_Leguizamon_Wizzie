using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimationsSO", menuName = "Game/Data/PlayerAnimations")]
public class PlayerAnimationsSO : ScriptableObject
{
    [SerializeField] private PlayerAnimationClipSO idle;
    [SerializeField] private PlayerAnimationClipSO jump;
    [SerializeField] private PlayerAnimationClipSO fall;
    [SerializeField] private PlayerAnimationClipSO land;
    [SerializeField] private PlayerAnimationClipSO run;
    [SerializeField] private PlayerAnimationClipSO throwball;

    public PlayerAnimationClipSO Idle => idle;
    public PlayerAnimationClipSO Jump => jump;
    public PlayerAnimationClipSO Fall => fall;
    public PlayerAnimationClipSO Land => land;
    public PlayerAnimationClipSO Run => run;
    public PlayerAnimationClipSO Throw => throwball;
}
