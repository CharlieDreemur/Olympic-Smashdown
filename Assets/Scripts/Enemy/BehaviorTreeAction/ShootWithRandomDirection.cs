using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Shoots a projectile in a specified direction")]
public class ShootWithRandomDirection : Action
{
    public SharedProjectileData projectileData;
    public SharedTransform target;
    public SharedTransform self;

    public override TaskStatus OnUpdate()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Projectile.InstantiateProjectile(projectileData.Value, self.Value.position, ProjectileOwnerType.enemy, new Vector3(randomDirection.x,randomDirection.y, 0), target.Value.gameObject.GetComponent<Entity>());
        return TaskStatus.Success;
    }
}