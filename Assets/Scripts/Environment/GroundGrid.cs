using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGrid : MonoBehaviour
{
    [SerializeField] private GroundTilemap groundTilemapPrefab;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    private Action<Collider2D> OnTileExit = (Collider2D collider) => { };

    private GroundController groundController;
    
    void OnTriggerExit2D(Collider2D collider) => OnTileExit.Invoke(collider);

    private void Start()
    {
        Controllers.GetController(EControllerType.Ground, out groundController);
        OnTileExit += groundController.OnTileExit;
        groundController.SetGrid(gameObject);

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                var newTile = Instantiate(groundTilemapPrefab, transform);
                newTile.transform.position = new Vector3Int(i, j, 0) * newTile.Tilemap.size;
                if (i == 0 && j == 0) boxCollider2D.size = (Vector2Int)newTile.Tilemap.size * 2;
            }
        }
    }
}
