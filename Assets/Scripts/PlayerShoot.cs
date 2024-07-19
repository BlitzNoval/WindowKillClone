using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject p_bullet;
    public Transform shootPoint_parent;
    public GameObject[] bulletSpawn;

    public float bulletSpeed;
    public float radius = 0.5f;
    public float a;

    private void Start()
    {
        SortShootPos();

        foreach (GameObject p in bulletSpawn)
        {
            GetShootAngle(p);
        }
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < bulletSpawn.Length; i++)
            {
                GameObject bullet = Instantiate(p_bullet, bulletSpawn[i].transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = GetShootAngle(bulletSpawn[i]) * bulletSpeed;
            }
        }

        AimDirection();
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

    public Vector2 GetShootAngle(GameObject point)
    {
        Vector3 angle = point.transform.position - shootPoint_parent.position;
        Debug.DrawLine(shootPoint_parent.position, point.transform.position * 2);
        return Vector2.ClampMagnitude(angle, 1);
    }

    public void AimDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2)shootPoint_parent.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        shootPoint_parent.transform.rotation = rotation;
    }

}
