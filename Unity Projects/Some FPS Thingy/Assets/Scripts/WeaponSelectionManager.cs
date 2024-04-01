using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionManager : MonoBehaviour
{
    [Header("Weaponsbar")]
    public Image VoidForeground;
    public Color Void;
    public Image FireForeground;
    public Color Fire;
    public Image IceForeground;
    public Color Ice;
    public FireProjectileController Elements;

    // Start is called before the first frame update
    void Start()
    {
        Void = Color.magenta;
        Fire = Color.red;
        Ice = Color.blue;

        VoidForeground.color = Void;
        FireForeground.color = Fire;
        IceForeground.color = Ice;
    }

    // Update is called once per frame
    void Update()
    {
        SelectedWeapon();
    }

    // Function to set foreground value based on selected weapon
    public void SelectedWeapon()
    {
        if (Elements.Void == true)
        {
            Void.a = 1f;
            Fire.a = 0.5f;
            Ice.a = 0.5f;

            VoidForeground.color = Void;
            FireForeground.color = Fire;
            IceForeground.color = Ice;
        }
        else if (Elements.Fire == true)
        {
            Void.a = 0.5f;
            Fire.a = 1f;
            Ice.a = 0.5f;

            VoidForeground.color = Void;
            FireForeground.color = Fire;
            IceForeground.color = Ice;
        }
        else if (Elements.Ice == true)
        {
            Void.a = 0.5f;
            Fire.a = 0.5f;
            Ice.a = 1f;

            VoidForeground.color = Void;
            FireForeground.color = Fire;
            IceForeground.color = Ice;
        }
    }
}
