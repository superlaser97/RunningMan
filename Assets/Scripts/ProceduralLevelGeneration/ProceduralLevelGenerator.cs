using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour
{
    [SerializeField]
    private bool showGizmos = true;

    [Header("Player Parameters")]
    [SerializeField]
    private GameObject player_Prefab;
    [SerializeField]
    private Vector3 playerStartPos;
    
    [Header("Level Generator Parameters")]
    [SerializeField]
    private int totalBlocksToGenerate = 10;

    [Space(5)]

    [SerializeField]
    private ProceduralLevelBlock startBlock;
    [SerializeField]
    private ProceduralLevelBlock endBlock;

    [Space(5)]

    [SerializeField]
    private List<ProceduralLevelBlock> proceduralLevelBlocks;

    private List<int> generatedLevelOrder = new List<int>();

    private void Start()
    {
        GenerateRandomLevel();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(player_Prefab, transform.position + playerStartPos, transform.rotation);
    }

    private void GenerateRandomLevel()
    {
        int uniqueLevelBlocks = proceduralLevelBlocks.Count;
        Vector3 lvlBlockSpnOffset = new Vector3(0, 0);
        int prevGeneratedLevelOrderNum = -1;

        // Generate list of numbers to be used for level generation
        for (int i = 0; i < totalBlocksToGenerate; i++)
        {
            int newLevelOrder = 0;
            do
            {
                newLevelOrder = Random.Range(0, uniqueLevelBlocks);
            }
            while (newLevelOrder == prevGeneratedLevelOrderNum);
            prevGeneratedLevelOrderNum = newLevelOrder;

            generatedLevelOrder.Add(newLevelOrder);
        }

        // Instantiate Start & End Blocks
        if(startBlock)
        {
            lvlBlockSpnOffset += new Vector3(startBlock.GetBlockWidth(), 0);

            ProceduralLevelBlock spawnedLevelBlock = Instantiate(startBlock, transform.position + lvlBlockSpnOffset, transform.rotation);
            spawnedLevelBlock.transform.parent = transform;
            spawnedLevelBlock.HideBlockBounds();
            spawnedLevelBlock.name = startBlock.name;

            lvlBlockSpnOffset += new Vector3(startBlock.GetBlockWidth(), 0);
        }
        if (endBlock)
        {
            lvlBlockSpnOffset += new Vector3(endBlock.GetBlockWidth(), 0);

            ProceduralLevelBlock spawnedLevelBlock = Instantiate(endBlock, transform.position + lvlBlockSpnOffset, transform.rotation);
            spawnedLevelBlock.transform.parent = transform;
            spawnedLevelBlock.HideBlockBounds();
            spawnedLevelBlock.name = endBlock.name;

            lvlBlockSpnOffset += new Vector3(endBlock.GetBlockWidth(), 0);
        }
        // Instantiate Generated Level Blocks
        foreach (int order in generatedLevelOrder)
        {
            ProceduralLevelBlock levelBlock = proceduralLevelBlocks[order];

            lvlBlockSpnOffset += new Vector3(levelBlock.GetBlockWidth(), 0);

            ProceduralLevelBlock spawnedLevelBlock = Instantiate(levelBlock, transform.position + lvlBlockSpnOffset, transform.rotation);
            spawnedLevelBlock.transform.parent = transform;
            spawnedLevelBlock.HideBlockBounds();
            spawnedLevelBlock.name = levelBlock.name;

            lvlBlockSpnOffset += new Vector3(levelBlock.GetBlockWidth(), 0);
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(5, 0, 0));
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 5, 0));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + playerStartPos - new Vector3(0.5f, 0), transform.position + playerStartPos + new Vector3(0.5f, 0));
            Gizmos.DrawLine(transform.position + playerStartPos - new Vector3(0, 0.5f), transform.position + playerStartPos + new Vector3(0, 0.5f));
        }
    }
}
