using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lootdrop : MonoBehaviour
{
    public float DropChance = 10f;

    public GameObject[] RandomLoot;

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

        if (chance > DropChance)
            return;

        int selectedLoot = Random.Range(0, RandomLoot.Length);
        
        Debug.Log(selectedLoot);
        
        if (chance <= DropChance)
        {
            GameObject.Instantiate(RandomLoot[selectedLoot], transform.position, Quaternion.identity);
        }

    }
}
