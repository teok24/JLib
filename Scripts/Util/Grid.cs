using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JLib.Utils
{
    public class Grid<T>
    {
        private int width;
        private int height;
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        private float cellSize;
        public float CellSize { get { return cellSize; } }
        private Vector3 originPosition;
        private T[,] gridArray;
        private TextMesh[,] debugTextArray;

        public Grid(int width, int height, float cellSize = 1f, Vector3 originPosition = default(Vector3))
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new T[width, height];
            debugTextArray = new TextMesh[width, height];

            
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    yield return gridArray[x, y];
                }
            }
        }
        public void InitDebugTextArray(Transform parent)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = CreateWorldText(gridArray[x, y]?.ToString(), parent,
                        GetWorldPosition(x, y) + (new Vector3(cellSize, cellSize) * .5f),
                        Color.white);
                }
            }
        }
        public void UpdateDebug()
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white);
        }

        private TextMesh CreateWorldText(string text,
            Transform parent = null,
            Vector3 localPosition = default(Vector3),
            Color? color = null,
            int sortingOrder = 5000)
        {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, color.Value,
                localPosition,
                sortingOrder);
        }

    private TextMesh CreateWorldText(Transform parent, string text, Color color, Vector3 localPosition, int sortingOrder)
    {
        GameObject textObject = new GameObject("World_Text", typeof(TextMesh));
        textObject.transform.SetParent(parent, false);
        textObject.transform.localPosition = localPosition;

        TextMesh textMesh = textObject.GetComponent<TextMesh>();
        textMesh.alignment = TextAlignment.Center;
        textMesh.fontSize = 1;
        textMesh.color = color;

        textMesh.text = text;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }

        public Vector3 GetWorldPosition(int x, int y)
        {
            var v1 = new Vector3(x, y) * cellSize + originPosition;
            return v1;// + (new Vector3(cellSize, cellSize) * .5f);
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        public void SetValue(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                
                
            }
        }

        public void SetValue(Vector3 worldPosition, T value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }

        public T GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
                return default(T);
        }

        public T GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetValue(x, y);
        }
    }
}