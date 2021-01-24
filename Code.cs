using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour
{
    [Range(2,100)] public int length = 5;
    public GameObject cube;
    public GameObject player;
    public int detailScale = 8;
    public int noiseHeight = 3;
    public List<GameObject> blockList;
    private Vector3 startPos = Vector3.zero;
    private Hashtable cubePos;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x);
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z);

    // Start is called before the first frame update
    void Start()
    {
        cubePos = new Hashtable();
        GenerateTerrain(length);
    }

    private void Update()
    {
        if (Mathf.Abs(XPlayerMove) >= 1 || Mathf.Abs(ZPlayerMove) >= 1)
        {
            GenerateTerrain(length);
        }
    }

    private void GenerateTerrain(int length)
    {
        for (int x = -length; x < length; x++)
        {
            for (int z = -length; z < length; z++)
            {
                float xNoise = (x + transform.position.x) / detailScale;
                float zNoise = (z + transform.position.y) / detailScale;
                float yNoise = Mathf.PerlinNoise(xNoise + XPlayerLocation, zNoise + ZPlayerLocation);

                Vector3 pos = new Vector3(x + XPlayerLocation, yNoise * noiseHeight, z + ZPlayerLocation);
                if (!cubePos.ContainsKey(pos))
                {
                    var cubeInstance = Instantiate(cube, pos, Quaternion.identity, transform);
                    blockList.Add(cubeInstance);
                    cubePos.Add(pos, cubeInstance);
                }
            }
        }
    }
}
