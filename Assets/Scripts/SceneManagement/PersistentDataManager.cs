﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the global game persistence data
/// </summary>
public class PersistentDataManager : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// Internal singleton access
    /// </summary>
    protected static PersistentDataManager mInstance;

    /// <summary>
    /// A singleton access for this manager
    /// </summary>
    public static PersistentDataManager Instance
    {
        get
        {
            if (mInstance != null)
                return mInstance;

            mInstance = FindObjectOfType<PersistentDataManager>();

            if (mInstance != null)
                return mInstance;

            Create();
            return mInstance;
        }
    }

    /// <summary>
    /// Creates a new instance on the scene
    /// </summary>
    /// <returns></returns>
    public static PersistentDataManager Create()
    {
        var dataManagerGameObject = new GameObject("PersistentDataManager");
        DontDestroyOnLoad(dataManagerGameObject);
        mInstance = dataManagerGameObject.AddComponent<PersistentDataManager>();
        return mInstance;
    }

    #endregion

    #region Private Properties

    protected HashSet<IDataPersister> mDataPersisters = new HashSet<IDataPersister>();

    protected Dictionary<string, Data> mStore = new Dictionary<string, Data>();

    protected event Action schedule = null;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if(schedule != null)
        {
            schedule();
            schedule = null;
        }
    }

    #endregion

    #region Public Methods

    public static void RegisterPersister(IDataPersister persister)
    {
        var ds = persister.GetDataSettings();
        if (!string.IsNullOrEmpty(ds.dataTag))
            Instance.Register(persister);
    }

    public static void UnregisterPersister(IDataPersister persister)
    {

    }

    /// <summary>
    /// Clear all the data on the persisters
    /// </summary>
    public static void ClearPersisters() => Instance.mDataPersisters.Clear();

    public static void SaveAllData() => Instance.SaveAllDataInternal();

    public static void LoadAllData() => Instance.LoadAllDataInternal();

    #endregion

    #region Private Methods

    protected void Register(IDataPersister persister) => schedule += () => mDataPersisters.Add(persister);

    protected void Unregister(IDataPersister persister) => schedule += () => mDataPersisters.Remove(persister);

    protected void SaveAllDataInternal()
    {
        foreach (var dp in mDataPersisters)
            Save(dp);
    }

    protected void LoadAllDataInternal()
    {
        foreach (var dp in mDataPersisters)
            Load(dp);
    }

    protected void Save(IDataPersister dp)
    {
        var dataSettings = dp.GetDataSettings();
        if (dataSettings.persistenceType == DataSettings.PersistenceType.ReadOnly ||
            dataSettings.persistenceType == DataSettings.PersistenceType.NotPersist)
            return;
        if (!string.IsNullOrEmpty(dataSettings.dataTag))
            mStore[dataSettings.dataTag] = dp.SaveData();
    }

    protected void Load(IDataPersister dp)
    {
        var dataSettings = dp.GetDataSettings();
        if (dataSettings.persistenceType == DataSettings.PersistenceType.WriteOnly ||
            dataSettings.persistenceType == DataSettings.PersistenceType.NotPersist)
            return;
        if (!string.IsNullOrEmpty(dataSettings.dataTag))
            if (mStore.ContainsKey(dataSettings.dataTag))
                dp.LoadData(mStore[dataSettings.dataTag]);
    }

    #endregion
}
