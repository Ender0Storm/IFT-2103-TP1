using UnityEngine;
public class HoleController : MonoBehaviour
{
    void Update()
    {
        if ((long) transform.position.y <= 0.25 &&
            (long) transform.position.x >= 14.5 &&
            (long) transform.position.x <= 15.5 &&
            (long) transform.position.z <= -1 &&
            (long) transform.position.z <= 1)
        {
            transform.position = new Vector3(-1, 7.5f, 30);
        }
    }
}
