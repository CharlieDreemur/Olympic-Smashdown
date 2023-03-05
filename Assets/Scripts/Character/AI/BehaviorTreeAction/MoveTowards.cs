using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Move the object towards the target. Returns success always.")]
public class MoveTowards: Action
{
    // The speed of the object
    public SharedFloat speed = 0;
    // The transform that the object is moving towards
    public SharedTransform target;

    public override TaskStatus OnUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.Value.position, speed.Value * Time.deltaTime);
        return TaskStatus.Success;
    }
}