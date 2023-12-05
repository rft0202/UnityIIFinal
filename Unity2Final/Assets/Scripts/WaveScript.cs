using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{

    public int squaresWidth;
    List<float> vertexHeights = new(); //Same length ^^^
    public float vertexSpacing=1, maxHeight, rate;
    public bool random;

    void Start()
    {
        //Debug.Log(Mathf.Pow((squaresWidth + 1),2));
        Vector3[] newVertices = new Vector3[(int)(Mathf.Pow((squaresWidth + 1), 2))]; //Length = (squaresWidth+1)^2
        vertexHeights.Capacity = (int)(Mathf.Pow((squaresWidth + 1), 2));
        int[] newTriangles = new int[((int)(Mathf.Pow((squaresWidth), 2))*6)]; //Length = (squaresWidth^2)*6
        //Debug.Log(newVertices.Length);

        //Setting verticies
        int width = (int)Mathf.Sqrt(newVertices.Length);
        for(int i=0; i<width; i++)
        {
            for(int j=0; j<width; j++)
            {
                newVertices[i * width + j] = new Vector3(vertexSpacing * i, 0, vertexSpacing * j);
                if(random)
                    vertexHeights.Add(Random.Range(0, 360));
                else
                    vertexHeights.Add(newVertices[i * width + j].x + newVertices[i * width + j].z);
            }
        }
        //Setting Triangles
        int cnt = 0;
        for(int i=0; i < width-1; i++)
        {
            for(int j=0; j<width-1; j++)
            {
                int ind = i * width + j;
                newTriangles[0 + cnt * 6] = ind+1;
                newTriangles[1 + cnt * 6] = ind+width;
                newTriangles[2 + cnt * 6] = ind;
                newTriangles[3 + cnt * 6] = ind+width+1;
                newTriangles[4 + cnt * 6] = ind + width;
                newTriangles[5 + cnt * 6] = ind + 1;
                cnt++;
            }
        }

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
    }

    // Update is called once per frame
    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        //Move verticies
        for (var i = 0; i < mesh.vertexCount; i++){
            moveVert(i, vertices);
        }

        mesh.vertices = vertices;
        if(GetComponent<MeshCollider>()!=null)
            GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void moveVert(int ind, Vector3[] arr)
    {
        arr[ind] = new Vector3(arr[ind].x, vertHeight(ind,arr), arr[ind].z);
        vertexHeights[ind] = vertexHeights[ind] +(rate*Time.deltaTime);
    }

    float vertHeight(int ind, Vector3[] arr)
    {
        return Mathf.Sin(vertexHeights[ind])*maxHeight;
    }
}