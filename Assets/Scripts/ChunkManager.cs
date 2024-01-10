using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    
    [Header(" Elements ")]
    [SerializeField] private Chunk[] chunksPrefabs;
    [SerializeField] private Chunk[] levelChunks;
    private GameObject finishLine;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateOrderedLevel();

        finishLine = GameObject.FindWithTag("Finish");
    }

    private void CreateOrderedLevel()
    {
        Vector3 chunkPos = Vector3.zero;

        int length = levelChunks.Length;
        // 시작시 랜덤으로 5칸 생성 
        for (int i = 0; i < length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];

            if (i > 0)
                chunkPos.z += chunkToCreate.GetLength() * (float)0.5;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPos, Quaternion.identity, this.transform);
            chunkPos.z += chunkInstance.GetLength() * (float)0.5;
        }
    }

    private void CreateRandomLevel()
    {
        Vector3 chunkPos = Vector3.zero;

        int length = chunksPrefabs.Length;
        // 시작시 랜덤으로 5칸 생성 
        for (int i = 0; i < length; i++)
        {
            Chunk chunkToCreate = chunksPrefabs[Random.Range(0, length)];

            if (i > 0)
                chunkPos.z += chunkToCreate.GetLength() * (float)0.5;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPos, Quaternion.identity, this.transform);
            chunkPos.z += chunkInstance.GetLength() * (float)0.5;
        }
    }

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }
}
