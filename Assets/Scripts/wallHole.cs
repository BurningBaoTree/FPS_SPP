using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class wallHole : MonoBehaviour
{
    /// <summary>
    /// 최소거리
    /// </summary>
    public float minDistance;

    /// <summary>
    /// 
    /// </summary>
    public Transform trackedObject;

    public MeshFilter filter;

    public void Remesh()
    {
        filter.mesh = GenerateMeshWithHoles();
    }

    Mesh mesh;
    Vector3[] vertices;
    Vector3[] normals;
    Vector2[] uvs;
    int[] triangles;
    bool[] trianglesDisabled;
    List<int>[] trisWithVertex;

    Vector3[] origvertices;
    Vector3[] orignormals;
    Vector2[] origuvs;
    int[] origtriangles;

    void Start()
    {
        //mesh 새로 만듦
        mesh = new Mesh();
        //mesh filter 컴포넌트 가져옴
        filter = GetComponent<MeshFilter>();
        //
        orignormals = filter.mesh.normals;
        origvertices = filter.mesh.vertices;
        origuvs = filter.mesh.uv;
        origtriangles = filter.mesh.triangles;

        vertices = new Vector3[origvertices.Length];
        normals = new Vector3[orignormals.Length];
        uvs = new Vector2[origuvs.Length];
        triangles = new int[origtriangles.Length];
        trianglesDisabled = new bool[origtriangles.Length];

        orignormals.CopyTo(normals, 0);
        origvertices.CopyTo(vertices, 0);
        origtriangles.CopyTo(triangles, 0);
        origuvs.CopyTo(uvs, 0);

        trisWithVertex = new List<int>[origvertices.Length];
        for (int i = 0; i < origvertices.Length; ++i)
        {
            trisWithVertex[i] = origtriangles.IndexOf(i);

        }
        filter.mesh = GenerateMeshWithHoles();
    }

    Mesh GenerateMeshWithHoles()
    {
        Vector3 trackPos = trackedObject.position;
        for (int i = 0; i < origvertices.Length; ++i)
        {
            Vector3 v = new Vector3(origvertices[i].x * transform.localScale.x, origvertices[i].y * transform.localScale.y, origvertices[i].z * transform.localScale.z);
            if ((v + transform.position - trackPos).magnitude < minDistance)
            {
                for (int j = 0; j < trisWithVertex[i].Count; ++j)
                {
                    int value = trisWithVertex[i][j];
                    int remainder = value % 3;
                    trianglesDisabled[value - remainder] = true;
                    trianglesDisabled[value - remainder + 1] = true;
                    trianglesDisabled[value - remainder + 2] = true;
                }
            }
        }
        triangles = origtriangles;
        triangles = triangles.RemoveAllSpecifiedIndicesFromArray(trianglesDisabled).ToArray();

        mesh.SetVertices(vertices.ToList<Vector3>());
        mesh.SetNormals(normals.ToList());
        mesh.SetUVs(0, uvs.ToList());
        mesh.SetTriangles(triangles, 0);
        for (int i = 0; i < trianglesDisabled.Length; ++i)
            trianglesDisabled[i] = false;
        return mesh;
    }
    Mesh GenerateMeshWithFakeHoles()
    {
        Vector3 trackPos = trackedObject.position;
        for (int i = 0; i < origvertices.Length; ++i)
        {
            if ((origvertices[i] + transform.position - trackPos).magnitude < minDistance)
            {
                normals[i] = -orignormals[i];
            }
            else
            {
                normals[i] = orignormals[i];
            }
        }
        mesh.SetVertices(vertices.ToList<Vector3>());
        mesh.SetNormals(normals.ToList());
        mesh.SetUVs(0, uvs.ToList());
        mesh.SetTriangles(triangles, 0);
        return mesh;
    }
    void Update()
    {
        Remesh();
    }
}

/// <summary>
/// 필요해서 만든 정적 클래스
/// </summary>
public static class ArrayExtensionMethods // : MonoBehaviour
{
    public static Array AddToArray(this Array a, object o)
    {
        if (a.GetType().GetElementType() == o.GetType())
        {
            Array b = Array.CreateInstance(a.GetType().GetElementType(), a.Length + 1);
            a.CopyTo(b, 1);
            b.SetValue(o, 0);

            a = b;
        }
        else
        {
            Debug.Log("Type mismatch, object not added. -- (Type) "
                + a.GetType().GetElementType() + " != (Type) " + o.GetType());
        }
        return a;
    }
    public static Array AddToArrayAtIndex(this Array a, object o, int index)
    {
        if (index > a.Length)
        {
            Debug.Log("Index outside the bounds of array. Object not added.");
        }
        else
        {
            if (a.GetType().GetElementType() == o.GetType())
            {
                Array b = Array.CreateInstance(a.GetType().GetElementType(), a.Length + 1);

                for (int i = 0; i < index; ++i)
                {
                    b.SetValue(a.GetValue(i), i);
                }
                for (int i = index + 1; i < b.Length; ++i)
                {
                    b.SetValue(a.GetValue(i - 1), i);
                }

                b.SetValue(o, index);
                a = b;
            }
            else
            {
                Debug.Log("Type mismatch, object not added. -- (Type) "
                    + a.GetType().GetElementType() + " != (Type) " + o.GetType());
            }
        }
        return a;
    }
    public static Array Remove(this Array a, object o)
    {
        if (a.GetType().GetElementType() == o.GetType())
        {
            if (a.Length == 0)
            {
                Debug.Log("array length already 0.");
                return a;
            }
            int occurrences = 0;
            for (int i = 0; i < a.Length; ++i)
            {
                if (a.GetValue(i).Equals(o))
                {
                    occurrences++;
                }
            }
            Array b = Array.CreateInstance(a.GetType().GetElementType(), a.Length - occurrences);
            int index = 0;
            for (int i = 0; i < a.Length; ++i)
            {
                if (!a.GetValue(i).Equals(o))
                {
                    b.SetValue(a.GetValue(i), index);
                    index++;

                }
            }
            a = b;
        }
        else
        {
            Debug.Log("Mismatched type passed as parameter for Array.Remove() -- (Type) "
                + a.GetType().GetElementType() + " != (Type) " + o.GetType());
        }
        return a;
    }
    public static Array RemoveFromArrayAtIndex(this Array a, int index)
    {
        if (index >= a.Length)
        {
            Debug.Log("Index outside the bounds of the array. Object not removed.");
        }
        else
        {
            if (a.Length > 0)
            {
                Array b = Array.CreateInstance(a.GetType().GetElementType(), a.Length - 1);
                for (int i = 0; i < index; ++i)
                {
                    b.SetValue(a.GetValue(i), i);
                }
                for (int i = index; i < b.Length; ++i)
                {
                    b.SetValue(a.GetValue(i + 1), i);
                }

                a = b;
            }
            else
            {
                Debug.Log("array has no elements to remove");
            }
        }
        return a;
    }
    public static Array EfficientShuffle(this Array a)
    {
        //uses the Fisher-Yates method of shuffling
        Array b = Array.CreateInstance(a.GetType().GetElementType(), a.Length);
        var randomness = new System.Random();

        for (int i = a.Length - 1; i >= 0; --i)
        {
            int rand = randomness.Next(a.Length);
            b.SetValue(a.GetValue(rand), i);
            a = a.RemoveFromArrayAtIndex(rand);
        }
        return b;

    }

    public static Array Shuffle(this Array a)
    {
        int replacements = UnityEngine.Random.Range(100, 1000);

        for (int i = 0; i < replacements; i++)
        {

            int A = UnityEngine.Random.Range(0, a.Length);
            int B = UnityEngine.Random.Range(0, a.Length);


            object e = a.GetValue(A);
            object f = a.GetValue(B);
            object g = a.GetValue(A);

            e = f;
            f = g;

            a.SetValue(e, A);
            a.SetValue(f, B);
        }

        return a;
    }



    //the following two functions are specific to the "generating holes dynamically" project
    public static List<int> RemoveAllSpecifiedIndicesFromArray(this int[] a, bool[] indicesToRemove)
    {
        //b리스트 생성과 동시에 초기화
        List<int> b = new List<int>();

        //bool의 배열 인자값의 길이만큼 반복문 내용 : 
        for (int i = 0; i < indicesToRemove.Length; ++i)
        {
            //indicesToRemove[i] 가 거짓일때 
            if (!indicesToRemove[i])
                b.Add(a[i]);
        }
        return b;
    }
    public static List<int> IndexOf(this Array a, object o)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < a.Length; ++i)
        {
            if (a.GetValue(i).Equals(o))
            {
                result.Add(i);
            }
        }
        result.Sort();
        return result;
    }

}