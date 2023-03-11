using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Returns a direction vector from self to target")]
public class SetDirection : Action
{
    public SharedTransform target;
    public SharedTransform self;
    public SharedVector3 direction;

    public override TaskStatus OnUpdate()
    {
        direction.SetValue(new Vector3(target.Value.position.x - self.Value.position.x, target.Value.position.y - self.Value.position.y, 0).normalized);
        return TaskStatus.Success;
    }
}