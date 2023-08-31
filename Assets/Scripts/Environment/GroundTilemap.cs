using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTileCoords
{
    public int x;
    public int y;
}

public class GroundTilemap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    public Tilemap Tilemap => tilemap;
    public GroundTileCoords Coords => _coords;

    private GroundTileCoords _coords = new GroundTileCoords();
}
