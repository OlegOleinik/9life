using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(GroundTilemap))]
public class TileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("RandomizeGrids"))
        {
            RandomizeGrids();
        }
    }
    
    private void RandomizeGrids()
    {
        var myScript = target as GroundTilemap;
        var tilemap = myScript.GetComponent<Tilemap>();
        var groundController = GameObject.FindObjectOfType<GroundController>();
        for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
        {
            for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j),
                    groundController.Tiles[UnityEngine.Random.Range(0, groundController.Tiles.Count)]);
            }
        }
    }
}
