using UnityEngine;

//Будем рассматривать точку в 2D, (без y координаты в 3D)
public class Point : MonoBehaviour
{
    public float X => transform.position.x;
    public float Z => transform.position.z;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var position = new Vector3(transform.position.x, 0, transform.position.z);
        Gizmos.DrawWireSphere(position,0.5f);
    }
}