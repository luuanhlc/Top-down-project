
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    WeaponIK weaponIK;

    public Transform leftHandHandle;
    public Transform rightHandHandle;

    public Transform aimPos;
    //public Transform lookPos;
    
    public AudioClip shootingAudioClip;
    public AudioClip reloadNeedAudioClip;
    public AudioClip reloadAudioClip;

    public float maxRange;

    RaycastHit hit;

    public GameObject impactEffectPerfab;
    private ParticleSystem impactEffect;
    private ParticleSystem bloodImpact;
    public ParticleSystem shootParticle;

    private void Start()
    {
        weaponIK = GetComponentInParent<WeaponIK>();

        impactEffect = Instantiate(impactEffectPerfab).GetComponent<ParticleSystem>();
        bloodImpact = Instantiate(PlayerStateManager.Ins.BloodImpactEffectPerfab).GetComponent<ParticleSystem>();
        weaponIK.rightHandTarget= rightHandHandle;
        weaponIK.leftHandTarget= leftHandHandle;
    }

#if UNITY_EDITOR
    private void Update()
    {
            Debug.DrawRay(aimPos.position, aimPos.transform.forward * 100f, Color.red);
    }

#endif
    public void CheckTarget()
    {
        if (Physics.Raycast(aimPos.position, aimPos.transform.forward, out hit, maxRange))
        {
            
            if (hit.transform.CompareTag("Enemy"))
            {
                bloodImpact.transform.position = hit.point;
                bloodImpact.transform.rotation = Quaternion.LookRotation(hit.normal);

                EnemyStateManager enemy = hit.transform.GetComponent<EnemyStateManager>();
                enemy.TakeDamage(Random.Range(20f, 50f));
                bloodImpact.Play();
                return;
            }
            else if (hit.transform.CompareTag("Destruct"))
            {
                hit.transform.GetComponent<boxDestruct>().TakeHit();
            }
            else if (hit.transform.CompareTag("Metal"))
            {
                AudioSource.PlayClipAtPoint(GameManager.Ins.GetSoundManager.metalHit, hit.point);
            }
            impactEffect.transform.position = hit.point;
            impactEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
            impactEffect.Play();
        }
    }
}
