using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected GameObject light;

    public virtual void Init(BulletInfo info)
    {
        light = transform.GetChild(0).gameObject;
    }
}