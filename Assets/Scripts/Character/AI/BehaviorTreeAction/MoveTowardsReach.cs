using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Move the object towards the target. Returns success once the object reached the target.")]
public class MoveTowardsReach: Action
{
    // The speed of the object
    public SharedFloat speed = 0;
    // The transform that the object is moving towards
    public SharedTransform target;

    public override TaskStatus OnUpdate()
    {
        // Return a task status of success once we've reached the target
        if (Vector3.SqrMagnitude(transform.position - target.Value.position) < 0.1f)
        {
            return TaskStatus.Success;
        }
        // We haven't reached the target yet so keep moving towards it
        transform.position = Vector3.MoveTowards(transform.position, target.Value.position, speed.Value * Time.deltaTime);
        return TaskStatus.Running;
    }
}