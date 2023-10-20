using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDead : MonoBehaviour
{
    ParticleSystem par;
    private void Awake()
    {
        par = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        par.Play();
        Destroy(this.gameObject, 5);
    }

}
