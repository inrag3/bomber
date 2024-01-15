using Infrastructure;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Grid))]
public class Grid : MonoBehaviour
{
    private UnityEngine.Grid _grid;

    private void Awake()
    {
        _grid = GetComponent<UnityEngine.Grid>();
    }
}