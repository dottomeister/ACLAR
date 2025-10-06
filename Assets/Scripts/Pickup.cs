using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    SLOWMO,
    FUEL,
    LOSECONTROL
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;

    PickupType GetPickupType()
    {
        return pickupType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pickup: " + collision.gameObject.name);

        if (collision.gameObject.name == "Spaceship")
        {
            switch (pickupType)
            {
                case PickupType.SLOWMO:
                    Debug.Log("SLOWMO");
                    collision.gameObject.GetComponent<PlayerController>().SetSlowmo();
                    break;
                case PickupType.FUEL:
                    Debug.Log("FUEL");
                    collision.gameObject.GetComponent<PlayerController>().AddFuel(10);
                    break;
                case PickupType.LOSECONTROL:
                    Debug.Log("LOSECONTROL");
                    collision.gameObject.GetComponent<PlayerController>().SetLoseControl();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
