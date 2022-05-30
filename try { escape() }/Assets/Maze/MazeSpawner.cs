using Unity.Mathematics;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1.5f,1.5f,0);
    public HintRenderer HintRenderer;
    private Vector3 positionDelta = new Vector3(1.5f, 0, 0);
    public GameObject zakuskaPrefab;

    public Maze maze;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();
        

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z) + positionDelta, 
                    Quaternion.identity);

                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }
        
        print(maze.finishPosition);
        var rotation = Quaternion.identity;
        var finPosition = maze.finishPosition;
        // if (maze.finishPosition.x == 0 || maze.finishPosition.x == generator.Width - 2)
        // {
        //     rotation = Quaternion.Euler(0, 0, 90);
        // }
        //
        // if (position.x == generator.Width - 2) position.x += CellSize.x;
        // else if (position.x != 0) position.x += CellSize.x / 2 - 0.06f;
        //
        // if (position.y == generator.Height - 2) position.y += CellSize.y;
        // else if (position.y != 0) position.y += CellSize.y / 2 - 0.06f;

        var finishCeil = maze.cells[finPosition.x, finPosition.y];
        if (finPosition.x == 0)
            Instantiate(zakuskaPrefab, new Vector3(finPosition.x, finPosition.y + CellSize.y / 2, 0) + positionDelta,
                Quaternion.Euler(0, 0, 90));
        else if (finPosition.y == 0)
            Instantiate(zakuskaPrefab, new Vector3(finPosition.x + CellSize.x / 2, finPosition.y, 0) + positionDelta,
                Quaternion.identity);
        
        else if (finPosition.x == generator.Width - 2)
            Instantiate(zakuskaPrefab, new Vector3(finPosition.x + CellSize.x, finPosition.y + CellSize.y / 2, 0) + positionDelta,
                Quaternion.Euler(0, 0, 90));
        else if (finPosition.y == generator.Height - 2)
            Instantiate(zakuskaPrefab, new Vector3(finPosition.x + CellSize.x / 2, finPosition.y + CellSize.y, 0) + positionDelta,
                Quaternion.identity);
    }
}