using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
    [SerializeField] private Image buffIconImagePrefab;
    private Dictionary<BuffType, Image> buffIconImages = new();

    public void AddIcon(BuffType buffType)
    {
        Image buffIcon = PoolManager.Instance.GetGameObject(buffIconImagePrefab,transform);
        buffIcon.sprite = BuffManager.Instance.GetRelevantBuffSprite(buffType);
        buffIconImages.Add(buffType,buffIcon);
    }

    public void RemoveIcon(BuffType buffType)
    {
        if (!buffIconImages.ContainsKey(buffType)) return;
        
        PoolManager.Instance.PushGameObject(buffIconImages[buffType]);
        buffIconImages.Remove(buffType);
    }
}



