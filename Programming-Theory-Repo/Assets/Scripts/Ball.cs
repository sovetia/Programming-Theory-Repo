using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static float ballSpeed;
    public float ballSpeedPlus;
    public int ballIndex;

    public MovePlatform moveP;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        moveP = GameObject.Find("Platforma").GetComponent<MovePlatform>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * ballSpeed * ballSpeedPlus);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // ловит игрок
            {
            if (ballIndex == PlayerController.indexColor || Bonus.bonusMagic) // правильная сортировка или бонус
            {
                gameManager.UpdateScore();
                
            }  else moveP.MoveUp(); // неправильная сортировка
        }
        
        if (other.gameObject.CompareTag("Ground")) // падает мимо
        {
            moveP.MoveUp();
        }

        Destroy(gameObject); // при любом попадании

    }
}
