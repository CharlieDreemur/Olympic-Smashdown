using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class ChunkManager : MonoBehaviour {

    public GameObject playerObj;
    private List<Chunk> chunks = new List<Chunk> { };

    static int chunkSize = 4;
    static int viewDist = 6;
    
	public float scale;
	public int octaves;
	[Range(0,1)]
	public float persistence;
	public float lacunarity;
	public int seed;
	public Vector2 offset;
    

    // Start is called before the first frame update
    void Start() {
        if (playerObj == null) { playerObj = GameObject.Find("Player"); }

        DrawChunks();
    }

    // Update is called once per frame
    void Update() {
        DrawChunks();
        DeleteExtra();
    }

    void DrawChunks() {

        int playerX = Mathf.RoundToInt( GetPlayerChunkCoords().x );
        int playerY = Mathf.RoundToInt( GetPlayerChunkCoords().y );

        for ( int i = -viewDist; i <= viewDist; i++ ) {
            for ( int j = -viewDist; j <= viewDist; j++ ) {

                    int trueX = i + playerX;
                    int trueY = j + playerY;

                    if ( isWithinRange( new Vector2( trueX, trueY ) ) && !HasDuplicate( trueX, trueY ) ) {

                        Chunk newChunk = new Chunk( chunkSize, new Vector2( trueX, trueY ), GetNoise( new Vector2( trueX, trueY ) ) );
                        chunks.Add( newChunk );
                    }

            }
        }


    }


    void DeleteExtra() {

        for ( int i = chunks.Count - 1; i >= 0; i-- ) {

            if ( !isWithinRange( chunks[i].GetCoords() ) ) {

                List<GameObject> tiles = chunks[i].GetTiles();

                for ( int j = 0; j < tiles.Count; j++ ) {
                    if ( tiles[j] != null ) { 
                        Destroy( tiles[j] );
                    }
                }

                chunks.RemoveAt(i);

            }
        }
    }

    bool isWithinRange( Vector2 coords ) {
        int xDist = Mathf.RoundToInt( Mathf.Abs( coords.x - GetPlayerChunkCoords().x ) );
        int yDist = Mathf.RoundToInt( Mathf.Abs( coords.y - GetPlayerChunkCoords().y ) );

        return viewDist >= ( xDist + yDist );
    }

    bool HasDuplicate( int x, int y ) {

        for ( int i = 0; i < chunks.Count; i++ ) {

            if ( x == chunks[i].GetCoords().x && y == chunks[i].GetCoords().y ) {
                // Debug.Log( chunks[i].GetCoords() );
                return true;
            }
        }

        return false;
    }


    Vector2 GetPlayerPosition() { return playerObj.transform.position; }

    Vector2 GetPlayerChunkCoords() {
        Vector2 playerPosition = GetPlayerPosition();
        return new Vector2( Mathf.FloorToInt( playerPosition.x / chunkSize ), Mathf.FloorToInt( playerPosition.y / chunkSize ) );
    }

    float[,] GetNoise( Vector2 chunkPos ) {
        if ( scale <= 0 ) { scale = 0.0001f; }

        float halfWidth = chunkSize / 2f;
        float halfHeight = chunkSize / 2f;

        float[,] noiseMap = new float[chunkSize, chunkSize];

        System.Random rand = new System.Random(seed.GetHashCode());

        Vector2[] octavesOffset = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            float offset_X = rand.Next(-100000, 100000) + offset.x;
            float offset_Y = rand.Next(-100000, 100000) + offset.y;
            octavesOffset[i] = new Vector2(offset_X / chunkSize , offset_Y / chunkSize);
        }

        for (int x = 0; x < chunkSize; x++) {
            for (int y = 0; y < chunkSize; y++) {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                float superpositionCompensation = 0;

                
                for (int i = 0; i < octaves; i++) {


                    float sampleX = (x - halfWidth) / scale * frequency + octavesOffset[i].x * frequency;
                    float sampleY = (y - halfHeight) / scale * frequency + octavesOffset[i].y * frequency;

                    float noiseValue = Mathf.PerlinNoise(sampleX + chunkPos.x, sampleY + chunkPos.y);
                    noiseHeight += noiseValue * amplitude;
                    noiseHeight -= superpositionCompensation;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                    superpositionCompensation = amplitude / 2;

                }

                noiseMap[x, y] = Mathf.Clamp01(noiseHeight); 
                
            }
        }

        return noiseMap;
    }
}
