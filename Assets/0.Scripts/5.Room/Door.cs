using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        //TODO:这里不知道为什么碰撞的消息不会向上层传递？？？
        GetComponentInParent<Room>().OnExitDoor(other);
    }
}
