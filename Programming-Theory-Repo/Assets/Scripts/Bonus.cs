using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public float bonusSpeed = 3.0f;

    public Material[] bonusMaterial;
    Renderer bonusRend;
    private int indexColor = 0;

    public MovePlatform moveP;
    public PlayerController playerC;

    public static bool bonusActive = false; // ����� �� ������� ����� ���� �������� �����
    public static bool bonusMagic = false; // ��� ����������



    // Start is called before the first frame update
    void Start()
    {

        bonusRend = GetComponent<Renderer>();
        moveP = GameObject.Find("Platforma").GetComponent<MovePlatform>();
        playerC = GameObject.Find("Player").GetComponent<PlayerController>();
        //Invoke(ChangeBonus(), 0.1f);
        indexColor = Random.Range(0, bonusMaterial.Length); // change bonus
        bonusRend.sharedMaterial = bonusMaterial[indexColor];

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * bonusSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bonusActive = true;

            if (indexColor == 0) // �������� 10 �������
            {
                moveP.MoveDown();
            }
            else if (indexColor == 1) // ������������� �������
            {
                playerC.BonusMagicOn();
                bonusMagic = true;
            }
            else if (indexColor == 2) // ��������� �����
            {
                playerC.BonusTimeOn();
            }

        }

        Destroy(gameObject);

    }

    void ChangeBonus()
    {

    }

}
