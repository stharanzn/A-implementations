using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dimensions
{
    public int Height;
    public int Width;
}

public class GroundGrid : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask obstacleLayermask;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Dimensions gridDimensions;
    [SerializeField] private float nodeSize = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float gizmoRadius = 0.5f;
    #endregion

    #region Private Variables
    private GroundNode[,] gridNodes;
    #endregion

    #region Public Variables
    public List<GroundNode> path;
    #endregion

    #region Monobehaviour Methods

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(gridDimensions.Width / 2, 0f, gridDimensions.Height / 2), new Vector3(gridDimensions.Width, 1f, gridDimensions.Height));
        if (gridNodes == null)
        {
            return;
        }
        GroundNode playerNode = NodeFromWorldPosition(playerTransform.position);
        foreach (GroundNode nodePos in gridNodes)
        {
            Gizmos.color = nodePos.IsWalkable ? Color.clear : Color.red;

            if (playerNode == nodePos)
            {
                Gizmos.color = Color.yellow;
            }
            else if (path != null && path.Contains(nodePos))
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawCube(nodePos.WorldPosition, Vector3.one * gizmoRadius);
        }
    }

    #endregion

    #region Private Methods    

    private void DrawGrid()
    {
        int widthSize = Mathf.RoundToInt(gridDimensions.Width / nodeSize);
        int heightSize = Mathf.RoundToInt(gridDimensions.Height / nodeSize);
        gridNodes = new GroundNode[widthSize, heightSize];
        for (int x = 0; x < widthSize; x++)
        {
            for (int y = 0; y < heightSize; y++)
            {
                Vector3 cubePos = new Vector3(nodeSize * x, 0, nodeSize * y);
                bool isWalkable = !Physics.CheckSphere(cubePos, nodeSize / 2, obstacleLayermask) && Physics.CheckSphere(cubePos, nodeSize / 2, groundLayerMask);
                gridNodes[x, y] = new GroundNode(isWalkable, cubePos, x, y);
            }
        }
    }

    #endregion

    #region Public Methods

    public void OnGameStart()
    {
        // worldBottomLeft = new Vector3(transform.position.x - (gridDimensions.Width / 2), 0, transform.position.z - (gridDimensions.Height / 2));
        DrawGrid();
    }

    public List<GroundNode> GetNeighbours(GroundNode node)
    {
        List<GroundNode> neighbours = new();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridDimensions.Width && checkY >= 0 && checkY < gridDimensions.Height)
                {
                    neighbours.Add(gridNodes[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public GroundNode NodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = worldPosition.x / gridDimensions.Width;
        float percentY = worldPosition.z / gridDimensions.Height;

        int xCount = gridNodes.GetLength(0);
        int yCount = gridNodes.GetLength(1);

        int x = Mathf.Clamp(Mathf.RoundToInt(xCount * percentX), 0, xCount - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(yCount * percentY), 0, yCount - 1);
        return gridNodes[x, y];
    }

    #endregion
}
