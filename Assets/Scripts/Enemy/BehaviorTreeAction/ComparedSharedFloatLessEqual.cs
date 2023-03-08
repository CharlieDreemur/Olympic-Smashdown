using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Returns Success if float a is less or equal than float b, else return failure.")]
public class CompareSharedFloatLessEqual : Conditional
{
    public SharedFloat a;
    public SharedFloat b;

    public override TaskStatus OnUpdate()
    {
        return a.Value <= b.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}