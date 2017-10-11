using UnityEngine;
using System.Collections;

public class SpeedParticles : MonoBehaviour {
	
	private PlayerControl _player;
	private float speed;
    ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        ps = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_player.status)
        {
            if (_player.currrentSpeed <= 15)
            {
                var em = ps.emission;
                em.enabled = false;
            }
            else
            {
                var em = ps.emission;
                em.enabled = false;
                var vel = ps.velocityOverLifetime;
             //   Vector3 aux = vel;// particleEmitter.localVelocity;
           //     aux.z = -(_player.currrentSpeed * 50) / 20;
               // vel = aux;
            }
        }
        else
        {
            var em = ps.emission;
            em.enabled = false;
        }
    }
}
