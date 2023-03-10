using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Set trigger for animator. Returns success always.")]
public class SetAnimatorTrigger: Action
{
    public string triggerName;
    public SharedTransform self;
    private Animator animator;
    public override void OnStart()
    {
        animator = self.Value.GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        animator.SetTrigger(triggerName);
        return TaskStatus.Success;
    }
}