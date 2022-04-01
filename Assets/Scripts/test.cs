using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class test : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;

    [SerializeField]
    private int nodeAmount = 0;

    private Voronoi GenerateVoronoi(Vector2Int size, int nodeAmount)
    {
        var centriods = new List<Vector2>();

        for(var i = 0; i < nodeAmount; i++)
        {
            var x = Random.Range(0, size.x);
            var y = Random.Range(0, size.y);

            centriods.Add(new Vector2(x, y));
        }

        var rect = new Rect(0f, 0f, size.x, size.y);
        var voronoi = new Voronoi(centriods, Rect);

        return voronoi;
    }
}
