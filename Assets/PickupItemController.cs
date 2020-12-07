using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemController : MonoBehaviour
{
    public ParticleSystem TorchLight;

    public float TimeToSnuffOut = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartFire());
        }
    }

    public IEnumerator StartFire()
    {
        TorchLight.Play();

        yield return new WaitForSeconds(TimeToSnuffOut);

        TorchLight.Stop();
    
    }

}
