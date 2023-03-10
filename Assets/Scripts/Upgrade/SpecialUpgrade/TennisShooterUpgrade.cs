using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tennis Shooter", menuName = "Upgrade/Tennis Shooter", order = 2)]
public class TennisShooterUpgrade: SpecialUpgrade, IUpgrade
{
    [SerializeField] private GameObject tennisShooterPrefab;
    public override void Upgrade()
    {
        var prefab = Instantiate(tennisShooterPrefab, parent:Player.Instance.transform);
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localRotation = Quaternion.identity;
    }

}