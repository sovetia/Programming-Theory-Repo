using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MovePlatform : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private float startPos = -13f;
    private float moveStop = -3.5f;

    //public static bool movePlatform = false;
    //public static bool gameOver = false;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void StartPos()
    {
        transform.position = new Vector3(0, startPos, 0);
    }

    public void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

        if (transform.position.y > moveStop) // Платформа останавливается, когда доходит до края. Игра заканчивается
        {
            transform.position = new Vector3(transform.position.x, moveStop, transform.position.z);

            gameManager.GameOver();

        }
    }

    public void MoveDown()
    {

        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed * 5);
        if (transform.position.y < startPos)
        {
            transform.position = new Vector3(transform.position.x, startPos, transform.position.z);
        }

        Bonus.bonusActive = false;

    }


}
