using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    /* Every NPC has a type that is mapped to an integer when instantiated.
     * 0 - Player
     * 1 - Guard
     * 2 - Chef
     * 3 - Janitor
     * 4 - Regular #1
     */
    public int type;

}
