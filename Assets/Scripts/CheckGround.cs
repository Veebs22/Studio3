using UnityEngine;
using UnityEngine.Events;

public class CheckGround : MonoBehaviour
{   
    public bool isOnground = true;
   // public UnityEvent TouchedGround = new();
   // public UnityEvent LeftGround = new();
    private void OnTriggerEnter(Collider triggeredObject)
    {
        if (triggeredObject.CompareTag("Ground"))
        {
            isOnground = true;
            //TouchedGround.Invoke();
        }

    }
    private void OnTriggerExit(Collider triggeredObject)
    {
        if (triggeredObject.CompareTag("Ground"))
        {
            isOnground = false;
            //LeftGround.Invoke();
        }

    }
    private void OnTriggerStay(Collider triggeredObject)
    {
        if (triggeredObject.CompareTag("Ground"))
        {
            isOnground = true;
            
        }

    }


}
