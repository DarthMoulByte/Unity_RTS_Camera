using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera _cam;
    NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //mouse right click movement
        if (Input.GetMouseButtonDown(0))
        {
            AddTargetToFollow();
        }
    }

    void AddTargetToFollow()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //new position to follow
            agent.destination = hit.point;
        }
    }
}
