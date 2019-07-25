﻿using UnityEngine;

/// <summary>
/// The attacking state for the player controller
/// </summary>
public class AttackingSMB : SceneSMB<KongController>
{
    #region State Methods

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.AttackDamager.Enable();
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.AttackDamager.Disable();
    }

    #endregion
}
