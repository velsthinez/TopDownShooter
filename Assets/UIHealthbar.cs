using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    public Image Healthbar;
    public Image HealthbarChaser;
    public float DelayFillSpeed = 1f;
    
    private Transform _player;

    private Health _playerHealth;

    private float chaserFillAmount = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_player == null)
            return;

        _playerHealth = _player.GetComponent<Health>();

        _playerHealth.OnHit += UpdateHealthbar;
        _playerHealth.OnHeal += UpdateHealthbar;
    }

    // Update is called once per frame
    void Update()
    {
        if (Healthbar == null || HealthbarChaser == null)
            return;

        if (_playerHealth == null)
        {
            Healthbar.fillAmount = 0f;
        }

        chaserFillAmount = Mathf.Lerp(chaserFillAmount,Healthbar.fillAmount , Time.deltaTime * DelayFillSpeed);
        HealthbarChaser.fillAmount = chaserFillAmount;
    }

    void UpdateHealthbar(GameObject go)
    {
        if (Healthbar == null || HealthbarChaser == null)
            return;
        
        float fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
        
        Healthbar.fillAmount = fillAmount;
    }
}
