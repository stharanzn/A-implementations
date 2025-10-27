using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private GroundGrid groundGrid;
    [SerializeField] private PathFinding pathFinding;
    #endregion

    #region Private Variables

    #endregion

    #region Public Variables

    #endregion

    #region Monobehaviour Methods
    private void Start()
    {
        groundGrid.OnGameStart();
        pathFinding.OnGameStart();
    }
    #endregion

    #region Private Methods

    #endregion

    #region Public Methods

    #endregion
}
