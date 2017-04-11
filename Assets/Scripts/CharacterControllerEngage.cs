using UnityEngine;


public class CharacterControllerEngage : MonoBehaviour
{
    private GameObject _player;
    private CharacterController2D _playerController;

    void Start()
    {
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<CharacterController2D>();
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            _playerController.enabled = true;
            Debug.Log(other);
        }
    }
}
