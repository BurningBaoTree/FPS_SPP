using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGearsInterFace
{
    Rigidbody rigid { get; set; }
    BoxCollider collider { get; set; }
}
