using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepAttack : ShootingSuperclass
{
    protected override void DoShootingEffect(Vector2 direction)
    {
        //base.DoShootingEffect(direction);
        StartCoroutine(DoSweep());
    }

    public override void OnHitboxHit(Collider2D other)
    {
        Debug.Log("WHOAAA");
        other.gameObject.GetComponent<Enemy>().TakeDamage(parentBehaviour.CalculateDamage());
    }

    private IEnumerator DoSweep()
    {
        float animationTime = parentBehaviour.CalculateCooldown();
        //Dividing animation into parts of 10 to make it easily divisible
        //Adding another part for grace period
        float extensionTime = animationTime * (2f / 11f);
        float swingTime = animationTime * (7f / 11f);
        float returnTime = animationTime * (1f / 11f);

        hitBox.SetActive(true);
        float currentTime = 0;
        do
        {
            //Weapon Extension code here
            Debug.Log("Extension");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < extensionTime);

        currentTime = 0;
        do
        {
            //Weapon Swing code here
            Debug.Log("Swing");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < swingTime);

        currentTime = 0;
        do
        {
            //Weapon Return code here
            Debug.Log("Return");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < returnTime);
        
        hitBox.SetActive(false);
    }
}
