using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpawn : MonoBehaviour
{
    [Header("ZombieSpawn Varible")]
    public GameObject zombiePrefab;
    public Transform zombiSpawnPosition;
    //public GameObject dangerZone1;
    private float repeatCycle = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);

            //distroy collider so it wont be infent spawninig everytime player hit collider
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombiSpawnPosition.position, zombiSpawnPosition.rotation);

    }
}
