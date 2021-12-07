using UnityEngine;
using UnityEngine.UI;

public class DifficulteButton : MonoBehaviour
{
    public Button button;
    private GameManager gameManager;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button.onClick.AddListener(SetDif);
    }

    void SetDif()
    {
        Debug.Log(gameObject.name + " is click");
        gameManager.StartGame(difficulty);
    }
}
