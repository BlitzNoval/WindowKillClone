using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTimeout : MonoBehaviour
{
    [SerializeField] private float timeoutTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoTimeout(timeoutTime));
    }

    private IEnumerator DoTimeout(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
