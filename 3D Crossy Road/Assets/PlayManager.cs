// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Duck duck;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -4;
    [SerializeField] int forwardViewDistance = 15;
    //[SerializeField, Range(min: 0,max: 1)] float treeProbability;

    Dictionary<int,Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    [SerializeField] private int travelDistance;

    public UnityEvent<int, int> OnUpdateTerrainLimit;
    private void Start()
    {

     for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
     {
        
        var terrain = Instantiate(original: terrainList[0]);

        terrain.transform.position = new Vector3(x: 0,y: 0,z: zPos);

        if(terrain is Grass grass)
            grass.SetTreePercentage(newProbability: zPos < -1 ? 1 : 0);

        terrain.Generate(size: horizontalSize);

        activeTerrainDict[key: zPos] = terrain;
     }   

     for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
     {
        SpawnRandomTerrain(zPos);  
     }  
     
      OnUpdateTerrainLimit.Invoke(arg0: horizontalSize,arg1: travelDistance + backViewDistance);
    
    }
    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain comparatorTerrain = null;
        int randomIndex;
        
        for (int z = -1; z >= - 3; z--)
        {
            var checkPos = zPos + z;
            // System.Type comparatorType = comparatorTerrain.GetType() ?? null;
            // System.Type checkType = activeTerrainDict[checkPos].GetType();
            if (comparatorTerrain == null)
            {
                comparatorTerrain = activeTerrainDict[key: checkPos];
                continue;
            }
            else if(comparatorTerrain.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(minInclusive: 0,maxExclusive: terrainList.Count);
                return SpawnTerrain(terrainList[randomIndex], zPos);
            }
            else
            {
                continue;
            }
        }

        var candidateTerrain = new List<Terrain>(terrainList);
        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            // System.Type comparatorType = comparatorTerrain.GetType();
            // System.Type checkType = candidateTerrain[i].GetType();
            if (comparatorTerrain.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }  

        randomIndex = Random.Range(minInclusive: 0,maxExclusive: candidateTerrain.Count);
        return SpawnTerrain(candidateTerrain[randomIndex], zPos);
    }
    
    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        terrain = Instantiate(original: terrain); 
        terrain.transform.position = new Vector3(x: 0,y: 0,z: zPos);
        terrain.Generate(size: horizontalSize);
        activeTerrainDict[key: zPos] = terrain;
        return terrain;
    }
    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if (targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt (f: targetPosition.z);
            UpdateTerrain();
        }
        
    }
    public void UpdateTerrain()
    {
        var destroyPos = travelDistance - 1 + backViewDistance;
        Destroy(obj: activeTerrainDict[destroyPos].gameObject);
        activeTerrainDict.Remove(destroyPos);

        var spawnPosition = travelDistance - 1 + forwardViewDistance;
        SpawnRandomTerrain(spawnPosition);

        OnUpdateTerrainLimit.Invoke(arg0: horizontalSize,arg1: travelDistance + backViewDistance);
    }
}
