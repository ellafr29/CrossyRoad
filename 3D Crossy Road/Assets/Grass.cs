
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePrefabList;
    [SerializeField, Range(min: 0,max: 1)] float treePercentage;

    public override void Generate(int size)
    {
        base.Generate(size);

        var limit = Mathf.FloorToInt(f: (float)size / 2);
        var treeCount = Mathf.FloorToInt(f: (float)size * treePercentage); 

        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }
        for (int i = 0; i < treeCount; i++)
        {
            var randomIndex = Random.Range(minInclusive: 0,maxExclusive: emptyPosition.Count);
            var pos = emptyPosition[randomIndex];

            emptyPosition.RemoveAt(randomIndex);

            randomIndex = Random.Range(minInclusive: 0,maxExclusive: treePrefabList.Count);;
            var prefab = treePrefabList[randomIndex];

            var tree = Instantiate(original: prefab, parent: transform);
            tree.transform.localPosition = new Vector3(x: pos,y: 0,z: 0);
        }
    }
}
