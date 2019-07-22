﻿using UnityEngine;

public class CheckPointBarrel : MonoBehaviour
{
    #region State Variables

    /// <summary>
    /// A flag that represents if the barrel is spawning the Player
    /// </summary>
    [HideInInspector]
    public bool Spawn
    {
        get
        {
            return animator.GetBool("Spawn");
        }
        set
        {
            animator.SetBool("Spawn", value);
        }
    }

    #endregion

    #region Public Properties

    public GameObject DustEffect;

    public GameObject CrackedEffect;

    #endregion

    #region Private Methods

    /// <summary>
    /// Instance for the game object animator
    /// </summary>
    private Animator animator;

    #endregion

    #region Unity Events

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    #endregion

    #region Public Methods

    public void OnSpawnStart()
    {
        Spawn = true;
    }

    public void OnSpawnFinished()
    {
        Debug.Log("Deu bom");
        
        // Destroy it self
        Destroy(gameObject);

        // Show player
        KongController.Instance.Spawn = false;

        // TODO: Spawn effects
    }

    #endregion
}
