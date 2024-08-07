﻿using System.Collections;
using UnityEngine;

public class BarrelBreakXF : MonoBehaviour
{
    #region Public Properties

    public GameObject[] Planks;

    /// <summary>
    /// Up force applied on explosion
    /// </summary>
    [Tooltip("Up force applied on explosion.")]
    public float Force = 8;

    /// <summary>
    /// Amount of random spread in X force direction
    /// </summary>
    [Tooltip("Amount of random spread in X force direction.")]
    public float SpreadRangeForce = 2;

    #endregion

    #region Unity Events

    private void Start()
    {
        SpawnPlanks();
    }

    #endregion

    #region Private Methods

    private void SpawnPlanks() {
        foreach(var plank in Planks) {
            var sp = plank.GetComponent<SpriteRenderer>();
            sp.enabled = true;

            var rb = plank.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.Range(-SpreadRangeForce, SpreadRangeForce), Force), ForceMode2D.Impulse);
        }
    }

    #endregion
}
