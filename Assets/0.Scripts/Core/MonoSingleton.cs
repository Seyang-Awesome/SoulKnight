using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this) Destroy(gameObject);
    }
}