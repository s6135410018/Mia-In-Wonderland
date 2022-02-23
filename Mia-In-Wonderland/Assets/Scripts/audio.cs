using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    [SerializeField] private AudioSource sources;

    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private playerController playerController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private playerHealth playerHealth;

    private void OnEnable() {
        playerController.m_clip += StartAudio;
        gameManager.m_clip += StartAudio;
        playerHealth.m_clip += StartAudio;
    }
    private void OnDisable() {
        playerController.m_clip -= StartAudio;
        gameManager.m_clip -= StartAudio;
        playerHealth.m_clip -= StartAudio;
    }
    private void Start() {
        sources.volume = 0.05f;
    }
    public void StartAudio(int index)
    {
        for (int i = 0; i < audioClip.Length; i++)
        {
            sources.PlayOneShot(audioClip[index]);
        }
    }
}
