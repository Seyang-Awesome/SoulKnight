using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected BulletInfo info;

    public virtual void Init(BulletInfo info)
    {
        gameObject.layer = info.Team.GetRelevantBulletLayer();
        this.info = info;
    }
}