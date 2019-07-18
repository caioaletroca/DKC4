﻿using UnityEngine;

/// <summary>
/// The running state for the player
/// </summary>
public class RunningSMB : SceneSMB<KongController>
{
    #region State Methods

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    #endregion
}