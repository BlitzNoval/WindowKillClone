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
        //Debug.Log("WHOAAA");
        other.gameObject.GetComponent<Enemy>()?.TakeDamage(parentBehaviour.CalculateDamage());
    }

    private IEnumerator DoSweep()
    {
        float animationTime = parentBehaviour.CalculateCooldown();
        //Dividing animation into parts of 10 to make it easily divisible
        //Adding another part for grace period - half the cooldown is the attack, the other half is idle
        float extensionTime = animationTime * (2f / 30f);
        float swingTime = animationTime * (5f / 30f);
        float returnTime = animationTime * (1f / 30f);

        Transform parentTransform = transform.parent;

        hitBox.SetActive(true);
        
        //Extension Phase
        float currentTime = 0;
        do
        {
            Vector2 movePos = Vector2.Lerp(parentTransform.position, parentTransform.position+transform.right * parentBehaviour.CalculateRange(),
                currentTime / extensionTime);
            transform.position = movePos;
            Debug.Log($"Extend. base is {parentTransform.position} and new is {movePos}");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < extensionTime);

        //Swing phase
        //Resetting time so that we can keep the phase lengths discreet
        currentTime = 0;
        float swingSize = -80;
        do
        {
            //Base angle * Time.deltaTime gives that degree per second
            //We want it to rotate that much over the duration of the phase
            transform.RotateAround(parentTransform.position, Vector3.forward, swingSize/swingTime*Time.deltaTime);
            //Weapon Swing code here
            Debug.Log($"Swing. base is {parentTransform.position} and new is {false}");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < swingTime);

        GameObject temp = Instantiate(new GameObject("Tester"), transform.position, Quaternion.identity,
            parentTransform);
        //Return phase
        currentTime = 0;
        do
        {
            Vector2 movePos = Vector2.Lerp(temp.transform.position, parentTransform.position,currentTime / returnTime);
            transform.position = movePos;
            //Weapon Return code here
            Debug.Log("Return");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < returnTime);
        
        Destroy(temp);
        hitBox.SetActive(false);
    }
}
