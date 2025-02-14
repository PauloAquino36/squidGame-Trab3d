using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool canMove = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            canMove = true;
            Debug.Log("Pode se mover!");
            yield return new WaitForSeconds(Random.Range(3, 6));

            canMove = false;
            Debug.Log("PARADO! Quem se mover morre!");
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }
}
