using System.Collections.Generic;
using UnityEngine;

public class PrimaryPlayerCreator : MonoBehaviour
{
    [SerializeField] private Leg _defaultLeg;
    [SerializeField] private Body _defaultBody;
    [SerializeField] private Head _defaultHead;
    [SerializeField] private Weapon _defaultWeapon;

    public Experience CreateDefaultExperience()
    {
        return new Experience(1, 0);
    }

    public IEnumerable<string> CreateDefaultDetails()
    {
        List<string> details = new List<string>
        {
            _defaultLeg.Id,
            _defaultBody.Id,
            _defaultHead.Id,
            _defaultWeapon.Id,
        };

        return details;
    }

    public IEnumerable<Upgrades> CreateDefaultUpgrades()
    {
        return new List<Upgrades>();
    }
}
