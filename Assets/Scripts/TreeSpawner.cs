using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject treePrefab; // Prefab of the tree you want to spawn
    public int numberOfTrees = 1000; // Number of trees to spawn

    private TreeInstance[] originalTreeInstances;
    void Start()
    {
        if (terrain != null)
        {
            // Backup the original tree instances before making modifications
            originalTreeInstances = terrain.terrainData.treeInstances;

        }
        else
        {
            Debug.LogError("Terrain not assigned!");
        }
    }

    public void ModifyTerrain()
    {
        // Check if terrain and treePrefab are assigned
        if (terrain != null && treePrefab != null)
        {
            // Get terrain data
            TerrainData terrainData = terrain.terrainData;

            for (int i = 0; i < numberOfTrees; i++)
            {
                // Generate random positions within the terrain boundaries
                float randomX = Random.Range(0f, 1f) * terrainData.size.x;
                float randomZ = Random.Range(0f, 1f) * terrainData.size.z;
                float randomY = terrainData.GetHeight((int)randomX, (int)randomZ);

                // Create a new tree instance
                TreeInstance newTree = new TreeInstance();

                // Normalize the position
                float normalizedX = randomX / terrainData.size.x;
                float normalizedY = randomY / terrainData.size.y;
                float normalizedZ = randomZ / terrainData.size.z;

                newTree.position = new Vector3(normalizedX, normalizedY, normalizedZ);

                // Set other properties of the new tree instance if needed
                newTree.prototypeIndex = 0; // Index of the tree prototype in the terrain's tree prototypes array
                newTree.widthScale = 1f;
                newTree.heightScale = 1f;

                // Add the new tree instance to the terrain's tree instances array
                terrain.AddTreeInstance(newTree);
            }

            // Update the terrain to apply changes
            terrain.Flush();

        }
        else
        {
            Debug.LogError("Terrain or treePrefab not assigned!");
        }
    }

    public void ResetTerrain()
    {
        // Restore the original tree instances
        terrain.terrainData.treeInstances = originalTreeInstances;
        terrain.Flush();
    }
}
