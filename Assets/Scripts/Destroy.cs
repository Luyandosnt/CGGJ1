using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private bool destroyOnStart = false;
    [SerializeField] private float timer = 0;

    private void Start()
    {
        if (destroyOnStart)
        {
            Destroy(gameObject, timer);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
