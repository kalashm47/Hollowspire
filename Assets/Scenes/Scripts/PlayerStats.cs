using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Moving")]
    [Range(1, 20)] public float moveSpeed = 6;
    [Range(1, 100)] public float maxFallSpeed = 25;
    [Range(0, 1)] public float wallSlideSlow = 0.85f;
    [Range(0, 1)] public float airAdjustMultiplier = 0.2f;
    [Range(0, 1)] public float momentumResist = 0.1f;
    [Header("Jumping")]
    [Range(1, 10)] public float jumpHeight = 2;
    [Range(0, 10)] public int numberOfExtraJumps = 0;
    [Range(1, 20)] public int jumpBuffer = 5;
    [Range(1, 20)] public int coyoteTime = 5;
    [Range(0, 5)] public float airHangThreshold = 0.3f;
    [Range(0.001f, 0.01f)] public float wallJumpLerp = 0.009f;
    [Range(0, 1)] public float wallJumpRecoveryThreshold = 0.06f;
    [Header("Gravity")]
    [Range(1, 10)] public float baseGravity = 2;
    [Range(1, 10)] public float fallGravityMultiplier = 3;
}
