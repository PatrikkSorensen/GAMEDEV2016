using UnityEngine;
using System.Collections;

public class PlayerStatusScript : MonoBehaviour  {
    public bool isChannelled;
    public bool isBonded;
    public bool canSlingshot;
    public float speed; 
    public int enemiesAttached;

    public PlayerStatusScript(float speed)
    {
        this.speed = speed;
        this.enemiesAttached = 0;
        this.isChannelled = false; 
        this.isBonded = false;
        this.canSlingshot = false; 
    }

    public void setSpeed(float speed)
    {
        this.speed = speed; 
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public void setBondStatus(bool b)
    {
        this.isBonded = b; 
    }

    public bool getBondStatus()
    {
        return this.isBonded;
    }

    public void setChannelStatus(bool b)
    {
        this.isChannelled = b;
    }

    public bool getChannelStatus()
    {
        return this.isChannelled;
    }

    public bool getCanSlingShot()
    {
        return this.canSlingshot;
    }

    public void setCanSlingshot(bool b)
    {
        this.canSlingshot = b; 
    }
}
