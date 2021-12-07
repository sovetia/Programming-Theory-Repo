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
        if (other.gameObject.CompareTag("Player")) // ����� �����
            {
            if (ballIndex == PlayerController.indexColor || Bonus.bonusMagic) // ���������� ���������� ��� �����
            {
                gameManager.UpdateScore();
                
            }  else moveP.MoveUp(); // ������������ ����������
        }
        
        if (other.gameObject.CompareTag("Ground")) // ������ ����
        {
            moveP.MoveUp();
        }

        Destroy(gameObject); // ��� ����� ���������

    }
}
