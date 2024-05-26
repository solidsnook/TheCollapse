using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    // Declaring Variables

    // Public
    public GameManager GM;
    public float radius = 1f;               // Determines how big the black hole will be
    public int blocksConsumed = 0;          // Determines how many blocks were eaten
    public List<AudioClip> AbsorbEffects;
    public int DecayRate;
    public int DecayDecreaseMultiplyer;

    // Private
    private int currentBlocksConsumed = 0;  // used to check if blocksConsumed's value has changed

    void Update()
    {
        // Set the scale to the radius value
        gameObject.transform.localScale = new Vector3(radius, radius, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the block touches the black hole it is consumed
        if (other.gameObject.CompareTag("Block"))
        {
            ConsumeBlock(other.gameObject);
        }
    }

    void ConsumeBlock(GameObject block)
    {
        // Adds to the black hole's counter and destroys the block
        GM.RemoveCube(block);
        Destroy(block, 0);
        Debug.Log("Block Consumed"); // Temp
        blocksConsumed++;

        //using the radius for the decay power
        radius += 0.05f; //can replace with the cube mass later
        DecayRate = ((int)radius/ DecayDecreaseMultiplyer) + DecayRate;

        //play random Absorb sound effect
        if (GetComponent<AudioSource>())
        {
            if (AbsorbEffects.Count > 0)
            {
                int rand = Random.Range(0, AbsorbEffects.Count);
                GetComponent<AudioSource>().clip = AbsorbEffects[rand];
                GetComponent<AudioSource>().Play();
            }
        }
    }
}

