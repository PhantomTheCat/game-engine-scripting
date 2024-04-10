using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CharacterEditor
{
    public class CharacterEditor : MonoBehaviour
    {
        [SerializeField] Button nextMaterial;
        [SerializeField] Button nextBodyPart;
        [SerializeField] Button loadGame;

        [SerializeField] Character character;

        int id;
        BodyTypes bodyType = BodyTypes.Head;

        private void Awake()
        {
            //Setup the button listeners
            nextMaterial.onClick.AddListener(NextMaterial);
            nextBodyPart.onClick.AddListener(NextBodyPart);
            loadGame.onClick.AddListener(LoadGame);
        }

        void NextMaterial()
        {
            //Getting the id for the next material
            if (id >= 3)
            {
                id = 0;
            }
            else { id++; }

            //Switch case for each BodyType and save the id of what color that body part is
            switch (bodyType)
            {
                case BodyTypes.Head:
                    PlayerPrefs.SetInt("Head", id);
                    break;
                case BodyTypes.Body:
                    PlayerPrefs.SetInt("Body", id);
                    break;
                case BodyTypes.Arm:
                    PlayerPrefs.SetInt("Arm", id);
                    break;
                case BodyTypes.Leg:
                    PlayerPrefs.SetInt("Leg", id);
                    break;
            }

            //Telling the character to load to get the updated body piece
            character.Load();
        }

        void NextBodyPart()
        {     
            //Switches it to next body part
            switch (bodyType)
            {
                case BodyTypes.Head:
                    bodyType = BodyTypes.Body;
                    break;
                case BodyTypes.Body:
                    bodyType = BodyTypes.Arm;
                    break;
                case BodyTypes.Arm:
                    bodyType = BodyTypes.Leg;
                    break;
                case BodyTypes.Leg:
                    bodyType = BodyTypes.Head;
                    break;
            }

            //Gets the current saved value from player prefs to the id for the current body part
            switch (bodyType)
            {
                case BodyTypes.Head:
                    id = PlayerPrefs.GetInt("Head", 1);
                    break;
                case BodyTypes.Body:
                    id = PlayerPrefs.GetInt("Body", 1);
                    break;
                case BodyTypes.Arm:
                    id = PlayerPrefs.GetInt("Arm", 1);
                    break;
                case BodyTypes.Leg:
                    id = PlayerPrefs.GetInt("Leg", 1);
                    break;
            }

        }

        void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}