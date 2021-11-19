using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Wall : MonoBehaviour
{
    [SerializeField] private Transform topObstacle;
    [SerializeField] private Transform bottomObstacle;

    public void SetHoleSize(float size)
    {
        if (topObstacle != null && bottomObstacle != null)
        {
            var offset = size * .5f;

            topObstacle.position += Vector3.up * offset;
            bottomObstacle.position += Vector3.down * offset;
        }
    }
}
