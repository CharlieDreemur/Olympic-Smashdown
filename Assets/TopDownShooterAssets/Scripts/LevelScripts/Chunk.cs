using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class Chunk {

    private int chunkSize;
    private Vector2 chunkCoords; 

    private List<GameObject> tiles = new List<GameObject> {};

    float[,] terrain;

    public Chunk( int _chunkSize, Vector2 _chunkCoords, float[,] _terrain ) {
        chunkSize = _chunkSize;
        chunkCoords = _chunkCoords;
        terrain = _terrain;

        DrawTiles();
    }

    // // Start is called before the first frame update
    // void Start() {}

    // // Update is called once per frame
    // void Update() {}

    void DrawTiles() {

        for ( int i = 0; i < chunkSize; i++ ) {

            for ( int j = 0; j < chunkSize; j++ ) {

                Vector3 newCoords = new Vector3( i + ( chunkCoords.x * chunkSize ), j + ( chunkCoords.y * chunkSize ), 1 );
                GameObject newTile = CreateTile( newCoords, terrain[i, j] );
                tiles.Add( newTile );

            }
        }

    }

    GameObject CreateTile( Vector3 worldCoords, float noise ) {
        GameObject tile = new GameObject();
        tile.AddComponent<SpriteRenderer>();

        // if ( noise > 0.35 ) {
        //     tile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Tile>("tilemap_45").sprite;
        //     // tile.GetComponent<SpriteRenderer>().sprite = sprite;
        // } else {
        //     // tile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/Tilemaps/New Folder/tilemap_0.asset");
        //     tile.GetComponent<SpriteRenderer>().sprite = sprite;
        // }
        

        // if ( Mathf.Abs( chunkCoords.x % 2 ) != Mathf.Abs( chunkCoords.y % 2 )  ) {
        //     tile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Tile>("tilemap_45").sprite;
        // } else {
        //     tile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Tile>("tilemap_1").sprite;
        // }        

        if ( noise > 0.35 ) {
            tile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Tile>("tilemap_45").sprite;
        } else {
            tile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Tile>("tilemap_1").sprite;
        }

        
        tile.transform.position = worldCoords;

        return tile;
    }

    public Vector2 GetCoords() { return chunkCoords; }

    public List<GameObject> GetTiles() { return tiles; }
}
