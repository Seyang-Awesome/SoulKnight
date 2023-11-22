using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEditor.BehaviourTree;

public class BtRunner : MonoBehaviour
{
    public BehaviourTreeConfig bt;

    private void Start()
    {
        bt = bt.CloneBehaviourTree();
    }
    private void Update()
    {
        bt.Update();
    }
}
