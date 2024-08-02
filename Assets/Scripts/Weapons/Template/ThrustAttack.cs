using System.Collections;
using UnityEngine;

public class ThrustAttack : ShootingSuperclass
{
    public float thrustRange = 1.0f;
    public float thrustCooldown = 1.0f;
    private bool isAttacking = false;
    private Vector3 originalPosition;
    
    void Start()
    {
        originalPosition = transform.position;
    }

    protected override void DoShootingEffect(Vector2 direction)
    {
        StartCoroutine(DoThrust());
    }
    
    public override void OnHitboxHit(Collider2D other)
    {
        other.gameObject.GetComponent<Enemy>()?.TakeDamage(parentBehaviour.CalculateDamage());
    }

    private IEnumerator DoThrust()
    {
        float animationTime = parentBehaviour.CalculateCooldown();
        //Dividing animation into parts of 10 to make it easily divisible
        //Adding another part for grace period - half the cooldown is the attack, the other half is idle
        float extensionTime = animationTime * (6f / 30f);
        //float swingTime = animationTime * (5f / 30f);
        float returnTime = animationTime * (4f / 30f);

        Transform parentTransform = transform.parent;

        hitBox.SetActive(true);
        
        //Extension Phase
        float currentTime = 0;
        do
        {
            Vector2 movePos = Vector2.Lerp(parentTransform.position, parentTransform.position+transform.right * parentBehaviour.CalculateRange(),
                currentTime / extensionTime);
            transform.position = movePos;
            //Debug.Log($"Extend. base is {parentTransform.position} and new is {movePos}");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < extensionTime);

    
        GameObject temp = new GameObject();
        temp.transform.position = transform.position;
        temp.transform.parent = parentTransform;
        //Return phase
        currentTime = 0;
        do
        {
            Vector2 movePos = Vector2.Lerp(temp.transform.position, parentTransform.position,currentTime / returnTime);
            transform.position = movePos;
            //Weapon Return code here
            //Debug.Log("Return");
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        } while (currentTime < returnTime);
        
        Destroy(temp);
        hitBox.SetActive(false);
    }
}