using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Transform pointLT;
    [SerializeField] private Transform pointRB;

    public Vector3 GetSpawnPoint()
    {
        float x = Random.Range(pointLT.position.x, pointRB.position.x);
        float y = Random.Range(pointRB.position.y, pointLT.position.y);
        return new Vector3(x, y, 0);
    }
}
