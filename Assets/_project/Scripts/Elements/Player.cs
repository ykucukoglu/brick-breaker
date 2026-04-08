using UnityEngine;

public class Player : MonoBehaviour
{
    public void RestartPlayer()
    {
        print("in restart player...");
    }

    public void MovePlayer(float xPos)
    {
        xPos = Mathf.Clamp(xPos, -2f, 2f);
        transform.position = new Vector3(xPos, transform.position.y, 0);
    }
}
