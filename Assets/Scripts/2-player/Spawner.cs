using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;



//[RequireComponent(typeof(AllowedTiles))]
//[RequireComponent(typeof(TilemapCaveGenerator))]
//[RequireComponent(typeof(CaveGenerator))]
public class Spawner : MonoBehaviour
{

  
    //private TilemapCaveGenerator tileMapCaveGenerator;
    [SerializeField] int gridSize;
    [SerializeField] Tilemap tilemap;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] int  reachableCells = 100;
    private TilemapGraph tilemapGraph = null;


    private void Awake()
    {
        //tileMapCaveGenerator = GetComponent<TilemapCaveGenerator>();
     
        //tilemap = tileMapCaveGenerator.GetComponent<Tilemap>();
    }
    // Start is called before the first frame update

    void Start()
    {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
    }
    bool isValid(Vector3 pos)
    {
        TileBase mapPos = TileOnPosition(pos);
        return (allowedTiles.Contains(mapPos));
    }

    public void spawnPlayer()
    {
        int attempts = gridSize * gridSize;
        bool valid = false;
        int reachedCells = 0;
        
        
        Debug.Log("generating random position");
        int x = Random.Range(0, gridSize);
        int y = Random.Range(0, gridSize);
        Vector3 spawnPos = new Vector3(x, y, 0);

        if (isValid(spawnPos)){
            Vector3Int mapPos = tilemap.WorldToCell(spawnPos);
            while (!valid && attempts > 0 && reachedCells < reachableCells)
            {
                int targetX = Random.Range(0, gridSize);
                int targetY = Random.Range(0, gridSize);
                Vector3 targetPos = new Vector3(targetX, targetY, 0);
                if (isValid(targetPos))
                {
                    Debug.Log("found a vald pos check for amunt of reaceable cells");
                    Vector3Int targetCell = tilemap.WorldToCell(targetPos);
                    List<Vector3Int> shortestPath = BFS.GetPath(tilemapGraph, mapPos, targetCell, 1000);
                    if (shortestPath.Count >= reachableCells)
                    {
                        Debug.Log("found valid position");
                        transform.position = mapPos;
                    }
                    else
                    {
                        continue;
                    }
                   
                }

            }
            Debug.Log("faild to find valid position");
            
          

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
       
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
}
