using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private AI ai;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        ai = FindObjectOfType<AI>();
    }
    public void Bumped()
    {
        player.Invincible = true;
        ai.CatchUp(10f);
    }
}
