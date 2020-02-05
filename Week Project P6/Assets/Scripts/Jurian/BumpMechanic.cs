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

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3.2f);
        transform.parent.gameObject.layer = 12;
    }

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
            transform.parent.gameObject.layer = 9;
            gameObject.layer = 9;
            StartCoroutine(Reset());
        }
    }
}
