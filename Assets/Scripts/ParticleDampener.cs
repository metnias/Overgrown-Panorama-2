using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleDampener : MonoBehaviour
{
    [SerializeField]
    private float clampDist = 100f;
    [SerializeField]
    private float maxDist = 50f;

    private Transform player;
    private ParticleSystem pSystem;

    private void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        player = PlayerController.Instance().gameObject.transform;
    }

    private void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        var emission = pSystem.emission;
        emission.rateOverTimeMultiplier = 1f - Mathf.Clamp01((dist - maxDist) / clampDist );
    }
}
