using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer;
    public GameManager gameManager;

    public Material[] playerMaterial;
    Renderer playerRend;
    public static int indexColor = 0;

    private float horInput;
    private Vector2 moveDirection = Vector2.zero;

    private float xRange = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        
        playerRend = GetComponent<Renderer>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speedPlayer = 4.0f;

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE

        horInput = Input.GetAxis("Horizontal");
        if (horInput != 0)
        {
            transform.Translate(Vector3.right * horInput * Time.deltaTime * speedPlayer);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }


#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch horTouch = Input.GetTouch(0);

            if (horTouch.phase == TouchPhase.Moved && gameManager.gameActive)
            {
                Vector2 positionChange = horTouch.deltaPosition;
                //positionChange.x = -positionChange.x;
                //positionChange.y = 0;
                //moveDirection = positionChange.normalized;

                if (positionChange.x > 0)
                {
                    transform.position += new Vector3(1, 0, 0) * speedPlayer * Time.deltaTime;
                }
                else
                {
                    transform.position += new Vector3(-1, 0, 0) * speedPlayer * Time.deltaTime;
                }

            }

            if (horTouch.phase == TouchPhase.Began) // переключение
            {
                Ray ray = Camera.main.ScreenPointToRay(horTouch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.CompareTag("Player") && gameManager.gameActive && !Bonus.bonusMagic && !gameManager.pauseActive)
                {
                    ChangeColor();
                }
            }

            // if (!MovePlatform.gameOver)
            // {
            //     transform.position += (Vector3)moveDirection; //* -speedPlayer * Time.deltaTime;
            // }

        }

#endif

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

       // if (Bonus.bonusActive && Bonus.bonusMagic || Bonus.bonusActive && Bonus.bonusTime) // бонусы для игрока
       // {
       //     StartCoroutine(BonusOff());
       //     Bonus.bonusActive = false;
       // }


    }

    public void BonusMagicOn()
    {
        Debug.Log("Start Corutine");
        indexColor = 4;
        playerRend.sharedMaterial = playerMaterial[4];
        StartCoroutine(BonusMagicOff());
    }

    public void BonusTimeOn()
    {
        Debug.Log("time out");
        Time.timeScale = 0.5f;
        speedPlayer = 8;
        StartCoroutine(BonusTimeOff());
    }

    IEnumerator BonusMagicOff()
    {
        yield return new WaitForSeconds(10);
        indexColor = 0;
        playerRend.sharedMaterial = playerMaterial[0];
        Bonus.bonusActive = false;
        Bonus.bonusMagic = false;
        Debug.Log("End Corutine");


    }

    IEnumerator BonusTimeOff()
    {
        yield return new WaitForSeconds(10);
        Time.timeScale = 1.0f;
        speedPlayer = 4;
        Bonus.bonusActive = false;
        Debug.Log("End corutine");
    }
    
  

    public void ChangeColor()
    {
        indexColor += 1;

       if (gameManager.targetCount == 2) // easy
        {
            if (indexColor > 1)
            {
                indexColor = 0;
            }
        }

        if (gameManager.targetCount == 3) // medium
        {
            if (indexColor > 2)
            {
                indexColor = 0;
            }
        }


        if (indexColor > 3)
        {
            indexColor = 0;
        }

        playerRend.sharedMaterial = playerMaterial[indexColor];

    }
}
