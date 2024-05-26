using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//spawn and manage blocks for dropping onto the planet, and jetisson them after they loiter long enough

public class InventorySystem : MonoBehaviour
{

    public AudioSource BlockPlacer;
    public AudioSource BlockGrabber;

    ////we need inventory cube specific thigns but the normal cubes dont need :)
    //[Serializable]
    //public struct InventoryCube
    //{
    //    //make sure we got timeros
    //    public GameObject cube;
    //    public int bhTimer;
    //    public int bhMaxTimer;
    //    //if we get succed
    //    public bool canBeVacuumed;
    //    public bool isBeingPlaced;
    //    public bool selectable;

    //    public void TimerTick()
    //    {
    //        bhTimer++;
    //    }

    //    public void SetVacuum()
    //    {
    //        if(!canBeVacuumed) { canBeVacuumed = true; }
    //        else{ canBeVacuumed = false; }
    //    }

    //    public void SetPlacing()
    //    {
    //        if (!isBeingPlaced) { isBeingPlaced = true; }
    //        else{ isBeingPlaced = false; }

    //    }

    //    public void SetSelectable()
    //    {
    //        if(!selectable) { selectable = true; }
    //        else { selectable = false; }
    //    }


    //}

    public GameManager gM;

    public Transform planetTransform;
    public Transform bhTransform;

    //for selectione visualizations
    public Material selectionMaterial;
    public Material dragMaterial;

    //tilt of orbit  that blocks will follow
    public Vector3 orbitTilt;
    //block selfrotation axis;
    public Vector3 blockTilt;
    //what the heck
    
    //we have to store our blocks
    public List<GameObject> inventory;
    //block spawn delay (minimum)
    public int blockSpawnDelay;
    public float currentDelay;
    //how long blocks can satay before they get removed
    public int maxBlockLifetime;
    //how fast blocks orbit
    public int orbitSpeed;
    //how high the inventory orbit is
    public int orbitHeight;
    //maximum blocks in the inventory at once
    public int maxBlockCount;

    private GameObject selectedCube;


    //block type counters for spawn chance weight
    public List<GameObject> CubeList;



    //current inventory count
    public int blockCount;

    //selection glow indicator
    public GameObject glowSelectionIndicator;
    //indicator helper transform keeper
    public Transform currentSelection;


    // Start is called before the first frame update
    void Start()
    {
        inventory.Clear();
        blockCount = 0;
        currentDelay = 0;

        orbitTilt = UnityEngine.Random.insideUnitSphere;
        blockTilt = UnityEngine.Random.onUnitSphere;
        glowSelectionIndicator.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectCube();
        }

        //system for managing floating inventory blocks

        //increment the spawn timer if we don't have maximum blocks
        if (blockCount < maxBlockCount)
        {
            currentDelay = currentDelay + (1 * Time.deltaTime);

            //weight the different cube types and choose the one we will apparate from the ether
            //currently only one type or something

            if(currentDelay>=blockSpawnDelay)
            {
                //play random detach sound effect
                if (CubeList.Count > 0)
                {
                    int rand = UnityEngine.Random.Range(0, CubeList.Count);
                    GameObject SpawnedCube = CubeList[rand];
                    //GetComponent<AudioSource>().Play();
                    SpawnBlock(SpawnedCube);
                }
            }
        }
        OrbitBlocks();
        //LetGoOfInventory();
        //follow the indicator with our selection
        if(currentSelection != null)
        {
            glowSelectionIndicator.transform.position = currentSelection.position;
        }



    }

    void SpawnBlock(GameObject blockType)
    {
        //spawn at equator, send on an orbit along the preset tilt
        var spawnPos = planetTransform.position + planetTransform.forward * orbitHeight;
        GameObject spawned = Instantiate(blockType, spawnPos, transform.rotation);
       // NewCube.GetComponent<MovingCubeScript>().BlockType = blockType;
        spawned.GetComponent<MovingCubeScript>().isMoving = true;
        spawned.transform.position = spawnPos;
        //spawned.AddComponent<Rigidbody>();
        //var rb  = spawned.cube.GetComponent<Rigidbody>();
        //rb.useGravity = false;
        //rb.angularDrag = 0;
        //rb.isKinematic = true;
        //spawned.bhTimer = 0;
        //spawned.bhMaxTimer = maxBlockCount * blockSpawnDelay;
        //spawned.canBeVacuumed = false;
        //spawned.isBeingPlaced = false;
        //spawned.selectable = true;
        //gM.AddCube(spawned.cube); //only happens when placing cube
        //spawned.bhMaxTimer = spawned.cube.GetComponent<MovingCubeScript>().Health * 2;
        // NewCube.
        //rb.maxAngularVelocity = 10;
        inventory.Add(spawned);
        blockCount++;
        currentDelay = 0;
    }



    void OrbitBlocks()
    {
        foreach(GameObject a in inventory)
        {
            //first we make them slowly rotate about their own axis

            a.transform.RotateAround(planetTransform.position, planetTransform.up, orbitSpeed * Time.deltaTime);

            //a.TimerTick();
            //if(a.bhTimer>=a.bhMaxTimer && a.isBeingPlaced==false)
            //{
            //    a.SetVacuum();
            //        //= true;
            //}
        }
    }

    //public void LetGoOfInventory()
    //{
    //    foreach (GameObject a in inventory)
    //    {
    //        if(a.canBeVacuumed)
    //        {
    //            a.cube.GetComponent<MovingCubeScript>().enabled = true;
    //            //a.cube.compon
    //            Rigidbody rb = a.cube.GetComponent<Rigidbody>();
    //            Destroy(rb);
    //            a.SetSelectable();
    //            a.cube.GetComponent<MovingCubeScript>().isMoving = true;
    //            inventory.Remove(a);
    //            //gM.RemoveCube(a.cube);
    //            //a.cube.GetComponent<Rigidbody>().
    //        }
    //    }
    //}

    //select a cube
    public void SelectCube()
    {
        //if there is no cube selected than select a cube
        if (!selectedCube)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.transform.gameObject.tag == "Block" && hit.transform.gameObject.GetComponent<MovingCubeScript>().isMoving)
                {
                    BlockGrabber.Play();
                    selectedCube = hit.transform.gameObject;
                    Debug.Log("hit:" + hit.transform.name);
                    glowSelectionIndicator.SetActive(true);
                    currentSelection = hit.transform.gameObject.transform;


                }
            }
        }
        else
        {
         
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.transform.gameObject.tag == "Block" && hit.transform.gameObject.GetComponent<MovingCubeScript>().isMoving == false)
                {
                    BlockPlacer.Play();
                    Destroy(selectedCube);
                    inventory.Remove(selectedCube);
                    blockCount--;
                    Vector3 postition = hit.transform.position + hit.normal;
                    gM.SpawnBlock(selectedCube, postition, hit.transform.rotation);
                    Debug.Log("Placed Block:" + hit.transform.name);
                    glowSelectionIndicator.SetActive(false);
                    currentSelection = null;
                }
            }
        }
    }
}
