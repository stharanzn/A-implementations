using UnityEngine;

public class GroundNode
{
    #region Inspector Variables
    public bool IsWalkable = false;
    public Vector3 WorldPosition;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public GroundNode parent;
    #endregion

    #region Private Variables

    #endregion

    #region Public Variables

    #endregion

    #region Monobehaviour Methods

    #endregion

    #region Private Methods

    #endregion

    #region Public Methods
    public GroundNode(bool isWalkable, Vector3 worldPosition, int _gridX, int _gridY)
    {
        IsWalkable = isWalkable;
        WorldPosition = worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }
    #endregion
}
