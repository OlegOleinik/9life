using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Transform pointLT;
    [SerializeField] private Transform pointRB;

    public Vector3 GetSpawnPoint()
    {
        float x = UnityEngine.Random.Range(pointLT.position.x, pointRB.position.x);
        float y = UnityEngine.Random.Range(pointRB.position.y, pointLT.position.y);
        
        return new Vector3(x, y, 0);
    }
}
