using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCone : MonoBehaviour
{
    public Collider collider;
    public GameObject prefab;
    private float coneInZone;
    public Transform spawnPos;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coneInZone = 0;

        Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Cone"))
            {
                coneInZone++;
            }
        }

        if (coneInZone == 0) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer < 0)
            {
                Instantiate(prefab, spawnPos);
            }
            
        } else
        {
            spawnTimer = 1;
        }
    }
}
