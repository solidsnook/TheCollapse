using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingCubeScript : MonoBehaviour
{
    // Declaring Variables

    // Public
    public bool isMoving = false;           // Determines if it should start moving towards the blackhole (make this randomly turn on, on the current layer as time passes)
    public float rotateSpeed = 1;           // Determines how fast the block will rotate
    public float Health;
    public Block BlockType;                 //block class type with its variables


    // Private
    private GameObject blackHole;           // Reference to BlackHole Object using tags
    private Vector3 rotateDirection;        // Determines what direction the block will rotate in
    private bool IsTremble = false;
    public float DecayTime;

    void Start()
    {
        blackHole = GameObject.FindWithTag("BlackHole");
        rotateDirection = Random.insideUnitSphere;
        Health = BlockType.Health;
        DecayTime = 0;
    }

    void Update()
    {
        DecayTime += Time.deltaTime;
        // If the block is directly facing the black hole then get sucked up
        Vector3 Direction = (blackHole.transform.position - transform.position).normalized;
        var ray = new Ray(transform.position, Direction);
        //Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(transform.position, blackHole.transform.position), Color.green);

        if(DecayTime > 0.1)
        {
            //randomly decay depedning on black hole decay rate
            float random = UnityEngine.Random.Range(0.0f, 1000 / blackHole.GetComponent<BlackHoleScript>().DecayRate);

            if (random <= 1 && !isMoving)
            {
                //Vector3 Direction = (blackHole.transform.position - transform.position).normalized;
                //var ray = new Ray(transform.position, Direction);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(transform.position, blackHole.transform.position), Color.green);
                if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, blackHole.transform.position)))
                {
                    Debug.Log(hit.transform.gameObject);
                    if (hit.transform.gameObject == blackHole)
                    {
                        Health -= 1;
                    }
                }
            }
            DecayTime = 0;
        }

        //shake if health is less than 10
        if(Health < 5 && IsTremble == false)
        {
            IsTremble = true;
            Debug.Log("Started trembling");
            StartCoroutine(shakeGameObjectCOR(this.gameObject, 10));
        }

        //detach block from planet when halth is 0
        if(Health <= 0 && !isMoving)
        {
            isMoving = true;
            transform.parent = null;
            StopAllCoroutines();

            //play random detach sound effect
            if(GetComponent<AudioSource>())
            {
                if(BlockType.DetachSounds.Count > 0)
                {
                    int rand = Random.Range(0, BlockType.DetachSounds.Count);
                    GetComponent<AudioSource>().clip = BlockType.DetachSounds[rand];
                    GetComponent<AudioSource>().Play();
                }
            }
        }

        if (isMoving && Health <= 0)
        {
            MoveTowardsBlackHole();
            RandomlyRotate();
        }
    }
    

    void MoveTowardsBlackHole()
    {
        // Makes block move towards the black hole depending on block speed
        float step = (BlockType.speed / Vector3.Distance(transform.position, blackHole.transform.position) * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, blackHole.transform.position, step);
    }

    void RandomlyRotate()
    {
        // This will gradually rotate the block in a random direction
        transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
    }

    IEnumerator shakeGameObjectCOR(GameObject objectToShake, float totalShakeDuration)
    {
        //Get Original Pos and rot
        Vector3 defaultPos = objectToShake.transform.localPosition;

        float counter = 0f;

        //Shake Speed
        const float speed = 0.1f;

        //Do the actual shaking
        while (counter < totalShakeDuration)
        {
            counter += Time.deltaTime;
            float decreaseSpeed = speed;

            //Shake GameObject
            objectToShake.transform.localPosition += new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.01f);
            objectToShake.transform.localPosition -= new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.01f);

            yield return null;

        }
        objectToShake.transform.localPosition = defaultPos; //Reset to original postion

        IsTremble = false;

        Debug.Log("Done!");
    }
}
