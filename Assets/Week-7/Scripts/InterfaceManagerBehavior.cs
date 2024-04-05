using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MazeGame
{
    public class InterfaceManagerBehavior : MonoBehaviour
    {
        //Properties
        [SerializeField] private GameObject playerObject;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI keyText;
        private PlayerBehavior player;

        //Methods
        void Awake()
        {
            player = playerObject.GetComponent<PlayerBehavior>();
        }

        void Update()
        {
            UpdateText();
        }

        void UpdateText()
        {
            scoreText.text = $"# of Coins: {player.coinCount}";
            healthText.text = $"Health: {player.health}";
            keyText.text = $"# of Keys: {player.numberOfKeys}";
        }
    }
}
