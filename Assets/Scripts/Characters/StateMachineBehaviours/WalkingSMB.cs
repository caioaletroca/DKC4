﻿using UnityEngine;

/// <summary>
/// The walking state for the player controller
/// </summary>
public class WalkingSMB : SceneSMB<KongController>
{
    #region State Methods

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(KongController.Instance.MovementSettings.WalkSpeed);
    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(KongController.Instance.MovementSettings.WalkSpeed);
    }

    #endregion
}