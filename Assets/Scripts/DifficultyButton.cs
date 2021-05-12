using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] int difficulty;

    Button button;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        manager.StartGame(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
