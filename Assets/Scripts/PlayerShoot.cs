using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerInput pInput;

    [SerializeField] private GameObject p_bullet;
    [SerializeField] private GameObject p_point;
    [SerializeField] private Transform shootPoint_parent;
    [SerializeField] private List<GameObject> bulletSpawnPos;

    public float bulletSpeed;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float startAngle;
    [SerializeField] private float angleSpread = 90f;
    #endregion

    private void OnEnable()
    {
        pInput.actions.Enable();
    }

    private void OnDisable()
    {
        pInput.actions.Disable();
    }

    private void Start()
    {
        startAngle = 90f - (angleSpread / 2f);

        GetShootPositions();
        SortShootPos();

        foreach (GameObject p in bulletSpawnPos)
        {
            GetShootPointAngle(p);
        }
    }
    private void Update()
    {
        //new input system
        if (pInput.actions.FindAction("Fire").WasPressedThisFrame()) Shoot();
        //if (Input.GetMouseButtonDown(0)) Shoot();
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
        for (int i = 0; i < shootPoint_parent.childCount; i++)
        {
            newShotPos.Add(shootPoint_parent.GetChild(i).gameObject);
        }

        bulletSpawnPos = newShotPos;
    }

    //finds position around a radius
    private void ShootPointPos(GameObject point, float angle)
    {
        Vector2 newPos;
        newPos.x = shootPoint_parent.localPosition.x + (radius * Mathf.Cos(angle / (180f / Mathf.PI)));
        newPos.y = shootPoint_parent.localPosition.y + (radius * Mathf.Sin(angle / (180f / Mathf.PI)));

        point.transform.localPosition = newPos;

    }

    //sorts each shooting position evenly
    private void SortShootPos()
    {
        float angle = angleSpread / (bulletSpawnPos.Count + 1);
        for (int i = 0; i < bulletSpawnPos.Count; i++)
        {
            ShootPointPos(bulletSpawnPos[i], (angle * (i + 1)) + startAngle);
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
        //Vector2 mousePos = pInput.actions.FindAction("Look").ReadValue<Vector2>();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(pInput.actions.FindAction("Look").ReadValue<Vector2>());
        Vector2 lookDir = mousePos - (Vector2)shootPoint_parent.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        shootPoint_parent.transform.rotation = rotation;
    }

}
