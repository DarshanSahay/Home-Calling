using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulMessage : MonoBehaviour
{
    [SerializeField] private GameObject soulUI;
    [SerializeField] private Text soulMessage;
    [SerializeField][TextArea] private string message;
    private void Start()
    {
        soulMessage.text = message;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            soulUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            soulUI.gameObject.SetActive(false);
        }
    }
}
