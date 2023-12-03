using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lootdrop : MonoBehaviour
{
    public float DropChance = 10f;
    
    public GameObject Loot;

    private Health _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();

        if (_health != null)
        {
            _health.OnDeath += DropLoot;
        }
    }

    void DropLoot()
    {

        float chance = Random.Range(0, 100);

        if (chance <= DropChance)
        {
            GameObject.Instantiate(Loot, transform.position, Quaternion.identity);
        }

    }
}
