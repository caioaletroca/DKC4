﻿using UnityEditor;
using UnityEngine;

/// <summary>
/// Controls the default blast barrel
/// </summary>
[RequireComponent(typeof(Animator))]
public class BlastBarrel : MonoBehaviour
{
    #region State Variable

    /// <summary>
    /// A flag that represents if the player is inside the barrel
    /// </summary>
    [HideInInspector]
    public bool Full
    {
        get => animator.GetBool("Full");
        set => animator.SetBool("Full", value);
    }

    /// <summary>
    /// The idle normalized time
    /// </summary>
    [HideInInspector]
    public float NormalizedTime
    {
        get => animator.GetFloat("NormalizedTime");
        set => animator.SetFloat("NormalizedTime", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The power the barrel will blast the player
    /// </summary>
    [Tooltip("The power the barrel will blast the player.")]
    public float Force = 1;

    /// <summary>
    /// The amount of time the gravity will be off after the barrel blast
    /// </summary>
    [Tooltip("The amount of time the gravity will be off after the barrel blast.")]
    public float PhysicsTime = 1;

    /// <summary>
    /// The start direction the barrel will face
    /// </summary>
    [Tooltip("Defines the default barrel facing side.")]
    public int StartFrame = 0;

    /// <summary>
    /// The Direction the player will be blasted
    /// </summary>
    [HideInInspector]
    public Vector2 BlastDirection = Vector2.up;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the animator
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// The used animation layer
    /// </summary>
    protected int AnimationLayer = 0;

    #endregion

    #region Unity Methods

    protected void Awake() => animator = GetComponent<Animator>();

    private void Start()
    {
        PerformFrame(StartFrame);
        PerformBlastDirection(StartFrame);
    }

    protected void OnDrawGizmosSelected()
    {
        var direction = Vector2.zero;
        
        // Calculate points
        if(Application.isPlaying)
            direction = Force * PhysicsTime * BlastDirection;
        else
            direction = Force * PhysicsTime * GetBlastDirection(StartFrame);

        var point = direction + (Vector2)transform.position;

        // Draw the gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, point);
        Gizmos.DrawWireSphere(point, 0.1f);
        Handles.Label(point, "Start Blast");
    }

    #endregion

    #region Update Methods

    /// <summary>
    /// Updates the blaster direction using the current frame
    /// </summary>
    public void UpdateBlastDiretion()
    {
        // Update blast direction with the current animation frame
        BlastDirection = GetBlastDirection(animator.GetCurrentFrame(AnimationLayer));
    }

    #endregion

    #region Perform Methods

    /// <summary>
    /// Performs the normalized time using frames
    /// </summary>
    /// <param name="frame"></param>
    public void PerformFrame(int frame) => PerformNormalizedTime(GetNormalizedTime(frame));

    /// <summary>
    /// Performs a new normalized time
    /// </summary>
    /// <param name="normalizedTime"></param>
    public void PerformNormalizedTime(float normalizedTime) => NormalizedTime = normalizedTime;

    /// <summary>
    /// Performs the new blast direction using a frame
    /// </summary>
    /// <param name="frame"></param>
    public void PerformBlastDirection(int frame) => BlastDirection = GetBlastDirection(frame);

    /// <summary>
    /// Performs the player executed blast, when player hits blast befores the timer runs out
    /// </summary>
    public virtual void PerformPlayerBlast() { }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets the Barrel state
    /// </summary>
    /// <param name="name">The name of the next state</param>
    /// <param name="frame">The frame to start</param>
    protected void SetState(string name, int frame = 0) => animator.Play(name, AnimationLayer, GetNormalizedTime(frame));

    /// <summary>
    /// Get the current blast direction with the current animation frame
    /// </summary>
    /// <param name="frame">The current animation frame</param>
    /// <returns></returns>
    protected Vector2 GetBlastDirection(int frame)
    {
        // Calculate direction
        var directionX = Mathf.Sin(frame * Mathf.PI / 8);
        var directionY = Mathf.Cos(frame * Mathf.PI / 8);

        return new Vector2(directionX, directionY);
    }

    /// <summary>
    /// Gets the normalized time for a specified frame with the current animation length
    /// </summary>
    /// <param name="frame">The specified frame</param>
    /// <returns></returns>
    protected float GetNormalizedTime(int frame) => frame / (float)animator.GetCurrentTotalFrames(AnimationLayer);

    /// <summary>
    /// Calculates the shortest direction rotation to arrive a target frame
    /// </summary>
    /// <param name="startFrame">The start frame</param>
    /// <param name="targetFrame">The target frame</param>
    /// <returns></returns>
    protected int GetShortestDirection(int startFrame, int targetFrame)
    {
        // Get the current frame
        var animationLength = animator.GetCurrentTotalFrames(AnimationLayer);

        // Updates the shortest direction
        var distance = (targetFrame - startFrame + 2 * animationLength) % animationLength - animationLength / 2;

        // Return the direction
        return distance >= 0 ? -1 : 1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    protected int NormalizeFrame(int frame) => frame >= 0 ? frame : frame + animator.GetCurrentTotalFrames(AnimationLayer);

    #endregion
}
