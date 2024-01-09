using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Chunk[] chunksPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 chunkPos = Vector3.zero;

        int length = chunksPrefabs.Length;
        // 시작시 랜덤으로 5칸 생성 
        for (int i = 0; i < length;  i++)
        {
            Chunk chunkToCreate = chunksPrefabs[Random.Range(0, length)];
                      
            if (i > 0)
                chunkPos.z += chunkToCreate.GetLength() * (float)0.5;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPos, Quaternion.identity, this.transform);
            chunkPos.z += chunkInstance.GetLength() * (float)0.5 ;
        }
    }
}
