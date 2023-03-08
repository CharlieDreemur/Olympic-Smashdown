using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskDescription("Only return Success, always timer")]
public class RunningTimer : Action
{
    public SharedFloat leftTime = 5;
    [SerializeField]
    public float time = 0;
    public override TaskStatus OnUpdate()
    {
        //LeftTime will not be reset when the task is runninh
        if (leftTime.Value > 0)
        {
            leftTime.SetValue(leftTime.Value - Time.deltaTime);
            time = leftTime.Value;
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}