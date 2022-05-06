using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public enum Type
    {
        Biome1,
        Biome2,
        Biome3,
        Biome4,
        Biome5,
        Biome6,
        Biome7,
        Biome8,
    };

    public Type biome;

    private void Start()
    {
        int rand = Random.Range(0, 8);
        biome = (Type)rand;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(biome);
    }
}
