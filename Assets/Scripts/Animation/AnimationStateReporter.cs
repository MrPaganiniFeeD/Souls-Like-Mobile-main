using System.Collections;
using System.Collections.Generic;
using DefaultNarmespace.Player.AnimatorReporter;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationStateReporter : StateMachineBehaviour
{
    private IAnimationStateReader _animationStateReader;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex, controller);
        FindReader(animator);
        
        _animationStateReader.EnteredState(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        base.OnStateExit(animator, stateInfo, layerIndex, controller);
        FindReader(animator);
        
        _animationStateReader.ExitedState(stateInfo.shortNameHash);
    }

    private void FindReader(Animator animator)
    {
        if(animator == null)
            return;

        _animationStateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
    }
}
