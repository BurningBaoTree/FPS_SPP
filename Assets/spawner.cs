using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public Vector3 room;
    public GameObject Enemy;

    public int spawnNum;
    private void Start()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-room.x*0.5f, room.x * 0.5f), Random.Range(-room.y * 0.5f, room.y * 0.5f), Random.Range(-room.z * 0.5f, room.z * 0.5f));
            Instantiate(Enemy, (this.transform.position + pos),Quaternion.identity);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position,room);      
    }
#endif
}
