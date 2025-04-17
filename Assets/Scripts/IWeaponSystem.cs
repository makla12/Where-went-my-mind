using UnityEngine;
using TMPro;

public interface IWeaponSystem
{
    void Initialize(Camera camera, ParticleSystem muzzleFlash, TMP_Text ammoText);
}
