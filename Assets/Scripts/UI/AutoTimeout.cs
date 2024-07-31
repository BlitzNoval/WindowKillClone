using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTimeout : MonoBehaviour
{
    [SerializeField] private float timeoutTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoTimeout(timeoutTime));
    }

    private IEnumerator DoTimeout(float seconds)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
