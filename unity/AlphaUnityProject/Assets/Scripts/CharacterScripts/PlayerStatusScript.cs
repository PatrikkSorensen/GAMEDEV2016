using UnityEngine;
using System.Collections;

// Component based solution instead of object oriented class solution. Might be changed.

public class PlayerStatusScript : MonoBehaviour  {
    private bool isEmpowered = false;
    private bool isBonded = false;
    private bool canSlingshot = false;
    private bool canEmpower = true; 
    private float speed = 15;

    // Speed state
    public void setSpeed(float speed)
    {
        this.speed = speed; 
    }

    public float getSpeed()
    {
        return this.speed;
    }

    // Bond state
    public void setBondStatus(bool b)
    {
        this.isBonded = b; 
    }

    public bool getBondStatus()
    {
        return this.isBonded;
    }

    // Can empower state
    public void setCanEmpowerStatus(bool b)
    {
        this.canEmpower = b;
    }

    public bool getCanEmpowerStatus()
    {
        return this.canEmpower;
    }

    // Empower state 
    public void setEmpowerStatus(bool b)
    {
        
        this.isEmpowered = b; 
    }

    public bool getEmpowerStatus()
    {
        return this.isEmpowered; 
    }
        
    // Slingshot state TODO: Make it attached state instead
    public bool getCanSlingShot()
    {
        return this.canSlingshot;
    }

    public void setCanSlingshot(bool b)
    {
        this.canSlingshot = b; 
    }
}
