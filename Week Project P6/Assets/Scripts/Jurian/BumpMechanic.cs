using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectTypes
{
    Wall,
    Floor
}

public class BumpMechanic : MonoBehaviour
{
    [SerializeField] private ObjectTypes Type = ObjectTypes.Floor;
    private GameObject Player;
    private GameManager manager;

    private void Start()
    {
        Player = FindObjectOfType<Player>().gameObject;
        manager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetInstanceID() == Player.GetInstanceID() && Type == ObjectTypes.Wall)
        {
            manager.Bumped();
        }
    }
}
