using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private GroundGrid grid;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;

    #endregion

    #region Private Variables
    private List<GroundNode> _openSet;
    private HashSet<GroundNode> _closedSet;
    #endregion

    #region Public Variables

    #endregion

    #region Monobehaviour Methods

    private void OnDrawGizmos()
    {
        if (_closedSet != null)
        {
            // foreach()
        }
    }

    private void Update()
    {
        FindPath(startTransform.position, endTransform.position);
    }
    #endregion

    #region Private Methods
    private void FindPath(Vector3 startPos, Vector3 endPos)
    {
        GroundNode startNode = grid.NodeFromWorldPosition(startPos);
        GroundNode endNode = grid.NodeFromWorldPosition(endPos);

        if (!startNode.IsWalkable || !endNode.IsWalkable || startNode == endNode)
        {
            grid.path = null;
            return;
        }

        List<GroundNode> openSet = new();
        HashSet<GroundNode> closedSet = new();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            GroundNode currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                break;
            }

            foreach (GroundNode neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        RetracePath(startNode, endNode);
    }

    private void RetracePath(GroundNode startNode, GroundNode endNode)
    {
        List<GroundNode> path = new();
        GroundNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        grid.path = path;
    }

    private int GetDistance(GroundNode nodeA, GroundNode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distX + 10 * (distY - distX);
    }

    #endregion

    #region Public Methods
    public void OnGameStart()
    {
        // FindPath(startTransform.position, endTransform.position);
    }
    #endregion
}
