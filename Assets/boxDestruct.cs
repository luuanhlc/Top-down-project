using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxDestruct : MonoBehaviour
{
    public GameObject destructPerfab;
    public GameObject originObj;
    public ParticleSystem explosionParticle;

    float exlosionMinForce = 5f;
    float exlosionMaxForce = 100f;
    float exlosionForceRadius = 10f;

    float fragScaleFactor = 1f;

    public GameObject fracObj;

    public void TakeHit()
    {
        if (originObj)
        {
            this.gameObject.GetComponent<MeshCollider>().enabled = false;
            originObj.GetComponent<MeshRenderer>().enabled = false;

            if (destructPerfab)
            {
                fracObj = Instantiate(destructPerfab, this.transform);
                foreach(Transform t in fracObj.transform)
                {
                    var _rb = t.GetComponent<Rigidbody>();

                    _rb.AddExplosionForce(Random.Range(exlosionMinForce, exlosionMaxForce), fracObj.transform.position, exlosionForceRadius);

                    StartCoroutine(Shrink(t, 2));
                }
            }
            
            
            if(explosionParticle)
            {
                explosionParticle.Play();
            }
            Destroy(this.gameObject, 5);


        }
    }
    
    private IEnumerator Shrink(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 newScale = t.localScale;

        while(newScale.x > 0)
        {
            newScale -= new Vector3(fragScaleFactor, fragScaleFactor, fragScaleFactor);
            t.localScale = newScale;
            yield return new WaitForSeconds(.05f);
        }
    }
}
