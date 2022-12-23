using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;
     public float xOffset, yOffset;
    public float maxDisatance;
    [SerializeField] float controllerSpeed = 5;
    public float h;
    [SerializeField] GameObject LevelParrent;
    public static PlayerController Instance;
    public bool handsFree =true;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        pathCreator = LevelParrent.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<PathCreator>();

        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        float tempSpeed = 0 ;
        //float moveInput = Input.GetAxis("Horizontal");
        //tempSpeed = controllerSpeed;

        if (Input.touchCount > 0) 
        {
            handsFree = false;
            Touch curTouch = Input.GetTouch(0);
            h = curTouch.deltaPosition.x;
            tempSpeed = controllerSpeed;
        }
        else
        {
            h = 0;
        }

        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            Vector3 desiredPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            xOffset += h * Time.deltaTime * tempSpeed;

            transform.position = desiredPoint;

            xOffset = Mathf.Clamp(xOffset, -maxDisatance, maxDisatance);

            desiredPoint = transform.TransformPoint(new Vector3(0, xOffset, 0));

            transform.position = desiredPoint;
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }


}
