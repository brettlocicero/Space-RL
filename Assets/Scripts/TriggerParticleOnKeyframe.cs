using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticleOnKeyframe : MonoBehaviour
{
    public ParticleSystem ps;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip sound;
    [SerializeField] Rigidbody2D proj;
    [SerializeField] Transform projSpawn;
    [SerializeField] float projTime = 0.5f;
    [SerializeField] float projForce = 2500f;

    public bool useAud;
    public bool useProj;

    public void PlayParticles ()
    {
        ps.Play();

        if (useAud) aud.PlayOneShot(sound);

        if (useProj) 
        {
            var p = Instantiate(proj, projSpawn.position, Quaternion.identity);
            p.transform.LookAt(GameController.instance.player.transform.position);
            p.GetComponent<Rigidbody2D>().AddForce(projForce * Vector2.left);
            Destroy(p, projTime);
        }
    }
}
