using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
    { 
        private readonly int width;
        private readonly int height;
        private readonly float cellSize;
        private readonly Vector3 position;
        private readonly GameObject[,] grid;
        
        public Grid(int width, int height, float cellSize, Vector3 position)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.position = position;
            grid = new GameObject[width, height];
        }

        public bool IsEmpty(int x, int y) => grid[x, y] == null;

        public void Delete(int x, int y)
        {
            if (IsEmpty(x, y)) return;
            Destroy(grid[x, y]);
        }

        public Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * cellSize + position;

        public (int, int) GetXY(Vector3 worldPosition)
        {
            var x = Mathf.FloorToInt((worldPosition - position).x / cellSize);
            var y = Mathf.FloorToInt((worldPosition - position).y / cellSize);
            return (x, y);
        }

        private void SetValue(int x, int y, GameObject value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
                grid[x, y] = value;
        }

        public void SetValue(Vector3 worldPosition, GameObject value)
        {
            var (x, y) = GetXY(worldPosition);
            SetValue(x, y, value);
        }
        
        private GameObject GetValue(int x, int y) => grid[x, y];

        public GameObject GetValue(Vector3 worldPosition)
        {
            var (x, y) = GetXY(worldPosition);
            return GetValue(x, y);
        }

        public void VisualizeGrid()
        {
            for(var x = 0; x < grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), 
                        Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), 
                        Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), 
                Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), 
                Color.white, 100f);
        }
            
    }
