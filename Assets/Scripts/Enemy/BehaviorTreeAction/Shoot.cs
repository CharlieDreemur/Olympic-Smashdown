using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Returns success if the target is within the specified range, otherwise failure.")]
public class Shoot : Action
{
    public SharedProjectileData projectileData;
    public SharedTransform target;
    public SharedTransform self;
    [SerializeField]
    private Vector3 _direction;

    public override TaskStatus OnUpdate()
    {
        _direction= new Vector2(target.Value.position.x - self.Value.position.x, target.Value.position.y - self.Value.position.y).normalized;
        Projectile.InstantiateProjectile(projectileData.Value, self.Value.position, ProjectileOwnerType.enemy, _direction, target.Value.gameObject.GetComponent<Entity>());
        return TaskStatus.Success;
    }
}