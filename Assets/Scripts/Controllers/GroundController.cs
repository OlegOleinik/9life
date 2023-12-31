using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundController : AController
{
    [SerializeField] private List<TileBase> tiles;
    public List<TileBase> Tiles => tiles;
    public void SetGrid(GameObject value) => grid = value;
    
    private GameObject grid;
    private Player player;

    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Player, out player);
    }

    public void OnTileExit(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            grid.transform.position = player.transform.position;
        }
    }
}
