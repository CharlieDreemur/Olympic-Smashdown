using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Shoots a projectile in a specified direction")]
public class ShootWithDirection : Action
{
    public SharedProjectileData projectileData;
    public SharedTransform target;
    public SharedTransform self;
    public SharedVector3 direction;

    public override TaskStatus OnUpdate()
    {
        Projectile.InstantiateProjectile(projectileData.Value, self.Value.position, ProjectileOwnerType.enemy, direction.Value, target.Value.gameObject.GetComponent<Entity>());
        return TaskStatus.Success;
    }
}