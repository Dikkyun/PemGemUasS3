using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePurpleImp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootImpact());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootImpact()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
