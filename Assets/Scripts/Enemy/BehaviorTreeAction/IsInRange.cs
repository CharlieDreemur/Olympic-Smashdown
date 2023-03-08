using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Returns success if the target is within the specified range, otherwise failure.")]
public class IsInRange : Conditional
{
    public SharedTransform targetTransform;
    public SharedTransform selfTransform;
    public SharedFloat range;

    public override TaskStatus OnUpdate()
    {
        return Vector3.Distance(targetTransform.Value.position, selfTransform.Value.position) < range.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}