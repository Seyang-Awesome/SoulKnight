using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffIcon : MonoBehaviour
{
    public struct BuffIconConfig
    {
        public BuffType buffType;
        public Sprite buffSprite;
    }

    public List<BuffIconConfig> buffIconConfigs;
    private Dictionary<BuffType, Sprite> buffIcons = new();

    private void Start()
    {
        buffIconConfigs.ForEach(buffIconConfig => buffIcons.Add(buffIconConfig.buffType, buffIconConfig.buffSprite));
    }

    public void AddIcon(BuffType buffType)
    {
        
    }

    public void RemoveIcon(BuffType buffType)
    {
        
    }
}



