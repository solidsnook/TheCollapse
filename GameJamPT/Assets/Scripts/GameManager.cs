using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public GameObject CubePrefab;
    List<GameObject> Cubes = new List<GameObject>();

    public GameObject CoreBlock, MantleBlock, CrustBlock;
    public int CoreRadius, MantleWidth, CrustWidth;
    public GameObject PlanetCenter;
    public TMP_Text PlanetHealthText;
    public int LosePercent;

    public int OrigonalPlanetSize;
    
    [SerializeField]
    public TextLoadOverScene textSaver;


    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        OrigonalPlanetSize = 1;
        //GetComponent<AudioSource>().Play();
        SpawnWorld(CoreRadius, MantleWidth, CrustWidth);
        
        // Finds and References the Planet Health UI
        //GameObject PlanetHealthTextRef = GameObject.Find("PlanetHealthText");
        //PlanetHealthText = PlanetHealthTextRef.GetComponent<TMP_Text>();
        
        textSaver = TextLoadOverScene.textManager.GetComponent<TextLoadOverScene>();
    }

    private void Update()
    {
        FindTextManager();
    }

    void FindTextManager()
    {
        if (textSaver == null)
        {
            textSaver = TextLoadOverScene.textManager.GetComponent<TextLoadOverScene>();
        }
    }

    void SpawnWorld(int CR, int MW, int CrW)
    {
        int WHD = CR + MW + CrW;
        for (int x = -WHD + 1; x < WHD; x++)
        {
            for (int y = -WHD + 1; y < WHD; y++)
            {
                for (int z = -WHD + 1; z < WHD; z++)
                {
                    Vector3 SpawnPos = new Vector3(x, y, z);
                    if (x < CR && x > -CR && y < CR && y > -CR && z < CR && z > -CR)
                    {
                        SpawnBlock(CoreBlock, SpawnPos, transform.rotation);
                    }
                    else if(x < MW + CR && x > -(MW + CR) && y < MW + CR && y > -(MW + CR) && z < MW + CR && z > -(MW + CR))
                    {
                        SpawnBlock(MantleBlock, SpawnPos, transform.rotation);
                    }
                    else
                    {
                        SpawnBlock(CrustBlock, SpawnPos, transform.rotation);
                    }
                }
            }
        }
        OrigonalPlanetSize = Cubes.Count;
        UpdatePlanetHealth();
    }

    public void SpawnBlock(GameObject Prefab, Vector3 Position , Quaternion Rotation)
    {
        GameObject NewCube = Instantiate(Prefab, Position, Rotation);
        NewCube.transform.SetParent(PlanetCenter.transform);
        NewCube.GetComponent<MovingCubeScript>().enabled = true;
        NewCube.GetComponent<MovingCubeScript>().isMoving = false;
        AddCube(NewCube);
    }

    public void RemoveCube(GameObject Cube)
    {
        Cubes.Remove(Cube);
        UpdatePlanetHealth();
    }

    public void AddCube(GameObject Cube)
    {
        Cubes.Add(Cube);
        UpdatePlanetHealth();
    }

    public void UpdatePlanetHealth()
    {
        Debug.Log("CubeCount = " + Cubes.Count);
        Debug.Log("OGPlanetSize = " + OrigonalPlanetSize);

        float Percent = ((float)Cubes.Count / (float)OrigonalPlanetSize) * 100;


        PlanetHealthText.text = "Planet Health: " + ((int)Percent).ToString() + "%";

        if (Percent < LosePercent)
        {
            GameOverCondition();
        }
    }

    void GameOverCondition()
    {
        textSaver.SetTimerText();
        textSaver.SetBlocksDestroyedText();
        // Opens GameOverScreen Scene
        SceneManager.LoadScene(2);
    }
}
