using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject p_bullet;
    public Transform shootPoint_parent;
    public GameObject[] bulletSpawn;

    public float radius = 0.5f;
    public float a;

    private void Start()
    {
    }
    private void Update()
    {
        SortShootPos();

    }

    public void ShootPointPos(GameObject point, float angle)
    {
        Vector2 newPos;
        newPos.x = shootPoint_parent.position.x + (radius * Mathf.Cos(angle / (180f / Mathf.PI)));
        newPos.y = shootPoint_parent.position.y + (radius * Mathf.Sin(angle / (180f / Mathf.PI)));

        point.transform.position = newPos;

    }
    public void SortShootPos()
    {
        float angle = 90 / (bulletSpawn.Length + 1);
        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            ShootPointPos(bulletSpawn[i], (angle * (i + 1)) + a);

            Debug.Log((angle * (i + 1)) + a);
        }
    }
}
