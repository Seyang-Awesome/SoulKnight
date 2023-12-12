using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SetGameObjectCommand : ICommand
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 generateOffset;
    public void OnInvoke(GameObject gameObject)
    {
        PoolManager.Instance.GetGameObject(prefab).transform.position = gameObject.transform.position + generateOffset;
    }
}

