using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Edited Origianl Triangulator Script for X,Y axis into X,Z axis. 

public class Triangulator
{
    Vector2 m_scale = new Vector2(1f, 1f);

    private List<Vector3> m_points = new List<Vector3>();

    public Triangulator(Vector3[] points)
    {
        m_points = new List<Vector3>(points);
    }

    public int[] Triangulate()
    {
        List<int> indices = new List<int>();

        int n = m_points.Count;
        if (n < 3)
            return indices.ToArray();

        int[] V = new int[n];
        if (Area() > 0)
        {
            for (int v = 0; v < n; v++)
                V[v] = v;
        }
        else
        {
            for (int v = 0; v < n; v++)
                V[v] = (n - 1) - v;
        }

        int nv = n;
        int count = 2 * nv;
        for (int v = nv - 1; nv > 2;)
        {
            if ((count--) <= 0)
                return indices.ToArray();

            int u = v;
            if (nv <= u)
                u = 0;
            v = u + 1;
            if (nv <= v)
                v = 0;
            int w = v + 1;
            if (nv <= w)
                w = 0;

            if (Snip(u, v, w, nv, V))
            {
                int a, b, c, s, t;
                a = V[u];
                b = V[v];
                c = V[w];
                indices.Add(a);
                indices.Add(b);
                indices.Add(c);
                for (s = v, t = v + 1; t < nv; s++, t++)
                    V[s] = V[t];
                nv--;
                count = 2 * nv;
            }
        }

        indices.Reverse();
        return indices.ToArray();
    }

    private float Area()
    {
        int n = m_points.Count;
        float A = 0.0f;
        for (int p = n - 1, q = 0; q < n; p = q++)
        {
            Vector3 pval = m_points[p];
            Vector3 qval = m_points[q];
            A += pval.x * qval.z - qval.x * pval.z;
        }
        return (A * 0.5f);
    }

    private bool Snip(int u, int v, int w, int n, int[] V)
    {
        int p;
        Vector3 A = m_points[V[u]];
        Vector3 B = m_points[V[v]];
        Vector3 C = m_points[V[w]];
        if (Mathf.Epsilon > (((B.x - A.x) * (C.z - A.z)) - ((B.z - A.z) * (C.x - A.x))))
            return false;
        for (p = 0; p < n; p++)
        {
            if ((p == u) || (p == v) || (p == w))
                continue;
            Vector3 P = m_points[V[p]];
            if (InsideTriangle(A, B, C, P))
                return false;
        }
        return true;
    }

    private bool InsideTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
    {
        float ax, az, bx, bz, cx, cz, apx, apz, bpx, bpz, cpx, cpz;
        float cCROSSap, bCROSScp, aCROSSbp;

        ax = C.x - B.x; az = C.z - B.z;
        bx = A.x - C.x; bz = A.z - C.z;
        cx = B.x - A.x; cz = B.z - A.z;
        apx = P.x - A.x; apz = P.z - A.z;
        bpx = P.x - B.x; bpz = P.z - B.z;
        cpx = P.x - C.x; cpz = P.z - C.z;

        aCROSSbp = ax * bpz - az * bpx;
        cCROSSap = cx * apz - cz * apx;
        bCROSScp = bx * cpz - bz * cpx;

        return ((aCROSSbp >= 0.0f) && (bCROSScp >= 0.0f) && (cCROSSap >= 0.0f));
    }

    public Vector2[] CalculateUV()
    {
        int n = m_points.Count;
        Vector2[] _uv = new Vector2[n];

        float xMin = float.MaxValue;
        float xMax = float.MinValue;
        float zMin = float.MaxValue;
        float zMax = float.MinValue;

        float x, z;

        for (int i = 0; i < n; i++)
        {
            x = m_points[i].x;
            z = m_points[i].x;

            if (x < xMin) xMin = x;
            if (x > xMax) xMax = x;
            if (z < zMin) zMin = z;
            if (z > zMax) zMax = z;
        }

        float dx = xMax - xMin;
        float dz = zMax - zMin;
        m_scale.Set(dx, dz);

        for (int i = 0; i < n; i++)
        {
            _uv[i] = new Vector2(
                    m_points[i].x / dx,
                    m_points[i].z / dz);
        }

        return _uv;
    }

    public Vector2 CalculateScale(float _scale)
    {
        return m_scale / _scale;
    }
}
