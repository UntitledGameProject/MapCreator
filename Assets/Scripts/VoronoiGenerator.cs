using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class VoronoiGenerator : MonoBehaviour
{
    public static Voronoi Generate(Vector2Int size, int nodeAmount, int lloydIterationCount)
    {
        List<Vector2> centroids = new List<Vector2>();

        for (int i = 0; i < nodeAmount; i++)
        {
            int x = Random.Range(0, size.x);
            int y = Random.Range(0, size.y);

            centroids.Add(new Vector2(x, y));
        }

        Rect rect = new Rect(0f, 0f, size.x, size.y);
        Voronoi voronoi = new Voronoi(centroids, rect, lloydIterationCount);

        return voronoi;
    }

    public static void DrawGizmos(Voronoi voronoi)
    {
        List<Vector2> siteCoords = voronoi.SiteCoords();

        foreach(Vector2 coord in siteCoords)
        {
            Gizmos.DrawCube(new Vector3(coord.x, coord.y, 0), Vector3.one);
        }

        foreach(Site site in voronoi.Sites)
        {
            List<Site> neighbors = site.NeighborSites();
            foreach(var neighbor in neighbors)
            {
                Edge edge = voronoi.FindEdgeFromAdjacentPolygons(site, neighbor);

                if (edge.ClippedVertices is null)
                {
                    continue;
                }

                Vector2 corner1 = edge.ClippedVertices[LR.LEFT];
                Vector2 corner2 = edge.ClippedVertices[LR.RIGHT];

                Gizmos.DrawLine(corner1, corner2);
            }
        }
    }
}
