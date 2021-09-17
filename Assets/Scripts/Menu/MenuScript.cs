
using UnityEngine;

public class MenuScript : MonoBehaviour
{


    
    public void OnStartGameSelected()
    {

        SceneController.instance.LoadScene(1);

    }

    public void OnPressExit()
    {
        Application.Quit();
    }


}
