using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[CreateAssetMenu(fileName = "ProjectileData", menuName = "GameBuilders/ScriptableObject/ProjectileSystem/ProjectileData", order = 0)]

[System.Serializable]
public class ProjectileData : ScriptableObject
{
    public string Name;
    
    public GameObject prefab;
    //! public AttackFilter attackObjectType;

    [Min(0.0f)] [Tooltip("The projectile will be deleted after the lifeCycle run out")]
    public float lifeCycle = 5.0f; //The projectile will destory itself after the lifeCycle
    [Min(0.0f)]
    public float maxDistance = 10.0f;

    public float damage;
    public float knockback;
    public ProjectileTrackType trackType;
    public Vector3 scale = Vector3.one; //Scale of Projectile
    public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe(1, 1), new Keyframe(1, 1));
    public float speedMultipler = 1;
    public AnimationCurve speedCurve = new AnimationCurve(new Keyframe(1, 1), new Keyframe(1, 1));
    //! public AnimationCurve heightCurve = new AnimationCurve(new Keyframe(1, 1), new Keyframe(1, 1));
    public float maxArcHeight = 5f;
    //The final speed will be the speed*speedCurve

    [Min(0.0f)]
    public float attackDuration;
    [Min(0.0f)]
    public float cooldown;
    public bool stun;

    [Tooltip("If yes, the projectile will call trigger() when it is released")]
    public bool isTriggerWhenRelease = true;
    //! public AttackCategory castType = AttackCategory.Cast;
    //! public List<ProjectileAction> actionList;
    
    [Space(10)]
    [Header("TrackParabola Setup")]
    public float gravityScale;
    public bool isGreaterAngle;
    public float indicatorRadius;

}
