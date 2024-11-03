using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    public GameObject Enemy;
    public Transform[] InstancePosition;
    public float Frequency;

    private float Timer;
    private int Position = 0;


    private void Update()
    {
        if(Timer >= Frequency)
        {
            Position = Random.Range(0, InstancePosition.Length);
            Instantiate(Enemy, InstancePosition[Position].position, InstancePosition[Position].rotation);
            Timer = 0;
        }
    }

    private void FixedUpdate()
    {
        Timer += Time.deltaTime;
    }
}
