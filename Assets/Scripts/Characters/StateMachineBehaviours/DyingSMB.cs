﻿using UnityEngine;

/// <summary>
/// Dying state for the player controller
/// </summary>
public class DyingSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(0);
        mMonoBehaviour.PerformDeathJump();
    }
}
