using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "Block")]
public class Block : ScriptableObject
{
    public int ID;
    public string Name;

    public int weight;
    public int speed;
    public int Health;

    public List<AudioClip> DetachSounds;
}
