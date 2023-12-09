using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet_Laser : BulletBase
{
    // [SerializeField] private BoxCollider2DWire show;
    [SerializeField] private Transform head;
    [SerializeField] private Transform tail;
    [SerializeField] private Transform body;

    private float laserWidth;
    private int teamLayerMask;
    private Vector2 CurrentDirection => body.transform.right;
    private Vector2 LaserCenter => (Vector2)transform.position + CurrentDirection * body.localScale.x / 2;
    private Vector2 LaserDetectBox => new Vector2(body.transform.localScale.x,laserWidth);
    private float LaserDetectAngle => Vector2.SignedAngle(Vector2.right, CurrentDirection);

    public override void Init(BulletInfo info)
    {
        base.Init(info);
        teamLayerMask = info.Team.GetRelevantTargetLayerMask();
    }
    
    public void SetLaserWidth(float width)
    {
        this.laserWidth = width;
    }
    
    public void SetLaser(Vector3 origin,Vector2 direction)
    {
        transform.position = origin;
        body.localScale = new Vector3(direction.magnitude, 1f,1f);
        body.transform.right = direction;
        tail.localPosition = direction;
    }

    public void Hurt()
    {
        Collider2D[] detects = Physics2D.OverlapBoxAll(LaserCenter,LaserDetectBox, LaserDetectAngle,teamLayerMask);
        // show.Set(LaserCenter,LaserDetectBox, LaserDetectAngle);
        detects.ToList().ForEach(detect =>
        {
            Vector2 hurtSource =
                Vector2.Angle(CurrentDirection, (Vector2)detect.bounds.center - (Vector2)transform.position) > 0
                    ? Vector2.right
                    : Vector2.left;
            HurtInfo hurtInfo = new HurtInfo(info.Damage,hurtSource,0);
            detect.GetComponent<Hurtable>().Hurt(hurtInfo);
        });
    }

    private void Update()
    {
        Debug.Log(CurrentDirection);
        Debug.Log(LaserDetectAngle);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(LaserCenter, LaserDetectBox);
    }
#endif
}

