using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapeartext : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(this.gameObject, 10);
    }
}
