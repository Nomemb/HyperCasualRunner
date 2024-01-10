using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header(" Elements ")]
    [SerializeField] private LevelSO[] levels;
    
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
        GenerateLevel();

        finishLine = GameObject.FindWithTag("Finish");
    }


    private void GenerateLevel()
    {
        int currentLevel = GetLevel();

        currentLevel = currentLevel % levels.Length;
        
        LevelSO level = levels[currentLevel];
        
        CreateLevel(level.chunks);
    }
    private void CreateLevel(Chunk[] levelChunks)
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

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 0);
    }
}
