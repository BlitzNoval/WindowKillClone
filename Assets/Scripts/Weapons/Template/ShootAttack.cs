using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootAttack : ShootingSuperclass
{
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private GameObject projectileToShoot;
    //This is an accuracy cone, using degrees
    [SerializeField] private float coneAngle;
    [SerializeField] private float minimumSpacingAngle;
    [SerializeField] private Transform projectilePlug;
    [SerializeField] private float projectileSpeed;

    protected override void DoShootingEffect(Vector2 direction)
    {
        //Updating the number of projectiles in case the weapon has been upgraded
        numberOfProjectiles = parentBehaviour.WeaponData.NumberOfProjectilesPerTier[(int)parentBehaviour.CurrentTier];
        
        #region Direction Calculations

        Vector2[] bulletDirections = new Vector2[numberOfProjectiles];
        List<float> allAngles = new List<float>();
        direction.Normalize();
        //Randomize spread
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            //Checking if the minimum spacing angle is going to cause an infinite loop
            if (minimumSpacingAngle * numberOfProjectiles > coneAngle)
            {
                bulletDirections[i] = direction;
            } else if (minimumSpacingAngle * numberOfProjectiles == coneAngle)
            {
                float initialAngle = -coneAngle / 2;
                float offsetAngle = initialAngle + (coneAngle / numberOfProjectiles * i);
                
                // Convert the angle to radians
                float angleInRadians = offsetAngle * Mathf.Deg2Rad;

                // Calculate the rotated vector
                float rotatedX = direction.x * Mathf.Cos(angleInRadians) - direction.y * Mathf.Sin(angleInRadians);
                float rotatedY = direction.x * Mathf.Sin(angleInRadians) + direction.y * Mathf.Cos(angleInRadians);

                Vector2 newAngle = new Vector2(rotatedX, rotatedY).normalized;

                bulletDirections[i] = newAngle;
            }
            else
            {
                float offsetAngle = 0;
                //Random gen
                while (true)
                {
                    //Offset angle chosen randomly within the cone
                    offsetAngle = Random.Range(-coneAngle / 2, coneAngle / 2);
                    bool tripFlag = false;
                    foreach (var chosenAngle in allAngles)
                    {
                        if (Mathf.Abs(offsetAngle - chosenAngle) < minimumSpacingAngle) tripFlag = true;
                    }

                    if (!tripFlag)
                    {
                        allAngles.Add(offsetAngle);
                        break;
                    }
                }
                
                // Convert the angle to radians
                float angleInRadians = offsetAngle * Mathf.Deg2Rad;

                // Calculate the rotated vector
                float rotatedX = direction.x * Mathf.Cos(angleInRadians) - direction.y * Mathf.Sin(angleInRadians);
                float rotatedY = direction.x * Mathf.Sin(angleInRadians) + direction.y * Mathf.Cos(angleInRadians);

                Vector2 newAngle = new Vector2(rotatedX, rotatedY).normalized;

                bulletDirections[i] = newAngle;
            }
        }

        #endregion
        
        float damage = parentBehaviour.CalculateDamage();
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            
            //Instantiate a new bullet
            GameObject newProj = Instantiate(projectileToShoot, projectilePlug.position, transform.rotation);
            
            //Accessing the bullet's script
            ProjectileBehaviour projBehaviour = newProj.GetComponent<ProjectileBehaviour>();

            //Throwing an exception if the bullet has no behaviour script
            //This is so we don't spawn useless projectiles that will exist forever
            if (!projBehaviour)
            {
                throw new UnassignedReferenceException(
                    $"The bullet prefab attached to {parentBehaviour.gameObject} has no projectile behaviour script");
            }
            
            //Putting in the information the bullet needs
            projBehaviour.damage = damage;
            projBehaviour.maxPierce = parentBehaviour.CalculatePierce();
            projBehaviour.maxRange = parentBehaviour.CalculateRange();
            projBehaviour.DoSetup();
            
            //assigning speed to the bullet
            newProj.GetComponent<Rigidbody2D>().AddForce(bulletDirections[i]*projectileSpeed, ForceMode2D.Impulse);
        }
    }
}
