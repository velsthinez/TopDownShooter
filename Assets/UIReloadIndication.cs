using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class UIReloadIndication : MonoBehaviour
{
    private UnityEngine.UI.Image _reloadBar;
    private WeaponHandler playerWeaponHandler;
    // Start is called before the first frame update
    void Start()
    {
        _reloadBar = GetComponent<UnityEngine.UI.Image>();
        
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");

        if (playerGO == null)
            return;
        
        playerWeaponHandler = playerGO.GetComponent<WeaponHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWeaponHandler == null)
            return;

        if (playerWeaponHandler.CurrentWeapon == null)
            return;

        if (playerWeaponHandler.CurrentWeapon.CurrentBulletCount > 0)
        {
            float currentBulletCount = playerWeaponHandler.CurrentWeapon.CurrentBulletCount;
            float maxBulletCount = playerWeaponHandler.CurrentWeapon.MaxBulletCount;

            float bulletLeftFill = currentBulletCount / maxBulletCount;
            
            if (_reloadBar != null)
                _reloadBar.fillAmount = bulletLeftFill;
        }
        
        if (!playerWeaponHandler.CurrentWeapon.ReloadCooldown.IsOnCooldown)
            return;

        float reloadFill = playerWeaponHandler.CurrentWeapon.ReloadCooldown.TimeLeft /
                             playerWeaponHandler.CurrentWeapon.ReloadCooldown.Duration;

        reloadFill -= 1;

        reloadFill *= -1;
        
        
        if (_reloadBar != null)
            _reloadBar.fillAmount = reloadFill;
    }
}
