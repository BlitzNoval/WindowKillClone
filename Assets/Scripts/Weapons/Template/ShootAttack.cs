using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : ShootingSuperclass
{
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private GameObject projectileToShoot;
    //This is an accuracy cone
    [SerializeField] private GameObject coneAngle;
    [SerializeField] private float minimumSpacingAngle;
    [SerializeField] private Transform projectilePlug;
    [SerializeField] private float projectileSpeed;
    
    protected override void DoShootingEffect(Vector2 direction)
    {
        float damage = parentBehaviour.CalculateDamage();
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            //Instantiate a new bullet
            GameObject newProj = Instantiate(projectileToShoot, projectilePlug.position, Quaternion.identity);
            
            //Accessing the bullet's script
            ProjectileBehaviour projBehaviour = newProj.GetComponent<ProjectileBehaviour>();

            //Throwing an exception if the bullet has no behaviour script
            //This is so we don't spawn useless projectiles that will exist forever
            if (!projBehaviour)
            {
                throw new UnassignedReferenceException(
                    $"The bullet prefab attached to {parentBehaviour.gameObject} has no projectile behaviour script");
                return;
            }
            //Putting in the information the bullet needs

        }
    }
}
