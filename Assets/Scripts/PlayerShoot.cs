using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject p_bullet;
    public GameObject p_point;
    public Transform shootPoint_parent;
    public List<GameObject> bulletSpawnPos;

    public float bulletSpeed;
    public float radius = 0.5f;
    public float a;

    private void Start()
    {
        GetShootPositions();
        SortShootPos();

        foreach (GameObject p in bulletSpawnPos)
        {
            GetShootPointAngle(p);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Shoot();
        if (Input.GetKeyDown(KeyCode.P)) Upgrade_MulltiShot();
        AimDirection();
    }

    public void Upgrade_MulltiShot()
    {
        GameObject newPos = Instantiate(p_point, shootPoint_parent);
        bulletSpawnPos.Add(newPos);
        SortShootPos();

        Debug.Log("Multi Shot: " + bulletSpawnPos.Count);
    }

    //shoot function
    private void Shoot()
    {
        for (int i = 0; i < bulletSpawnPos.Count; i++)
        {
            GameObject bullet = Instantiate(p_bullet, bulletSpawnPos[i].transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = GetShootPointAngle(bulletSpawnPos[i]) * bulletSpeed;
        }
    }

    //gets active shoot positions
    private void GetShootPositions()
    {
        List<GameObject> newShotPos = new List<GameObject>(); 
        for (int i = 0;i < shootPoint_parent.childCount; i++)
        {
            newShotPos.Add(shootPoint_parent.GetChild(i).gameObject);
        }

        bulletSpawnPos = newShotPos;
    }

    //finds position around a radius
    private void ShootPointPos(GameObject point, float angle)
    {
        Vector2 newPos;
        newPos.x = shootPoint_parent.position.x + (radius * Mathf.Cos(angle / (180f / Mathf.PI)));
        newPos.y = shootPoint_parent.position.y + (radius * Mathf.Sin(angle / (180f / Mathf.PI)));

        point.transform.localPosition = newPos;

    }

    //sorts each shooting position evenly
    private void SortShootPos()
    {
        float angle = 90 / (bulletSpawnPos.Count + 1);
        for (int i = 0; i < bulletSpawnPos.Count; i++)
        {
            ShootPointPos(bulletSpawnPos[i], (angle * (i + 1)) + a);

        }
    }

    //gets the shooting angle by subtracting the parents position (Vector.zero) from the shootingPoint
    private Vector2 GetShootPointAngle(GameObject point)
    {
        Vector3 angle = point.transform.position - shootPoint_parent.position;
        Debug.DrawLine(shootPoint_parent.position, point.transform.position * 2);
        return Vector2.ClampMagnitude(angle, 1);
    }

    //lets the player aim in the direction of thier mouse
    private void AimDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2)shootPoint_parent.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        shootPoint_parent.transform.rotation = rotation;
    }

}
