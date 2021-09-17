using UnityEngine;

public class SpotlightFlip : MonoBehaviour
{
    public float a;
    public float b;
    public float c;
    public float d;

    private void Update()
    {
        if (GameController.GameIsOver == false)
        {
            if (Player.controls.x < 0)
            {
                transform.localRotation = new Quaternion(180, -180, -180, 180);
            }
            if (Player.controls.x > 0)
            {
                transform.localRotation = new Quaternion(180, 180, 180, 180);
            }
        }
    }
    
}
