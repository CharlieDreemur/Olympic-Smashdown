using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Play animation. Returns success when animation is finished")]
public class PlayAnimation : Action
{
    public string animationName;
    public SharedTransform self;
    [SerializeField]
    private float leftTime = 0;
    public override void OnStart()
    {
        Animator animator = self.Value.GetComponent<Animator>();
        animator.Play(animationName);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        leftTime = stateInfo.length;
    }

    public override TaskStatus OnUpdate()
    {
        leftTime -= Time.deltaTime;
        if (leftTime <= 0)
            return TaskStatus.Success;
        return TaskStatus.Running;
    }
}