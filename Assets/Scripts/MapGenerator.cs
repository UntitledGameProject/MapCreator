using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class MapGenerator : MonoBehaviour
{
    public int seed = -1;
    public int width = 100;
    public int height = 100;
    Voronoi voronoi;


    private void Start()
    {
        if (seed < 0)
        {
            UpdateSeed();
        }
        else
        {
            Random.InitState(seed);

        }
        //StartCoroutine(Generate2());
        voronoi = VoronoiGenerator.Generate(new Vector2Int(100, 100), 50, 3);
    }

    private void OnDrawGizmos()
    {
        if (voronoi != null)
        {
            VoronoiGenerator.DrawGizmos(voronoi);
        }
    }

    private void UpdateSeed()
    {
        seed = Random.Range(0, 100000);
        Random.InitState(seed);
    }

    private float[,] GenerateNoise()
    {
        Vector2 offset = new Vector2(Random.value * 10 * width, Random.value * 10 * height);
        return NoiseGenerator.Generate2D(width, height, 0.025F, offset);
    }

    private IEnumerator Generate()
    {
        GameObject tilePrefab = Resources.Load<GameObject>("Tile");
        float[,] noise = GenerateNoise();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(i, j, 0);
                if (noise[i, j] < 0.25F)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.red;
                }
                else if (noise[i, j] < 0.5F)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                else if (noise[i, j] < 0.75F)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else if (noise[i, j] < 1)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
        }

        yield return true;
    }

    private IEnumerator Generate2()
    {
        GameObject tilePrefab = Resources.Load<GameObject>("Tile");
        float[,] noise1 = GenerateNoise();
        float[,] noise2 = GenerateNoise();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(i, j, 0);
                if (noise1[i, j] < 0.5F && noise2[i, j] < 0.5F)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (noise1[i, j] >= 0.5F && noise2[i, j] < 0.5F)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                if (noise1[i, j] < 0.5F && noise2[i, j] >= 0.5F)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.green;
                }
                if (noise1[i, j] >= 0.5F && noise2[i, j] >= 0.5F)
                {
                    tile.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
        }

        yield return true;
    }
}
