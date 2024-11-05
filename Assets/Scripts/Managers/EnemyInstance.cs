using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    public GameObject[] Enemies;
    public Transform EnemiesParent;
    public Transform[] InstancePosition;
    public float Frequency;

    private float Timer;
    private int Position = 0;


    private void Update()
    {
        if(Timer >= Frequency)
        {
            Position = Random.Range(0, InstancePosition.Length);
            int enemyIndex = Random.Range(0, Enemies.Length);
            Instantiate(Enemies[enemyIndex], InstancePosition[Position].position, Quaternion.identity, EnemiesParent);
            Timer = 0;
        }
    }

    private void FixedUpdate()
    {
        Timer += Time.deltaTime;
    }
}
