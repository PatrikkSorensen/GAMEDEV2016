using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour {

    private KeyCode mL = KeyCode.LeftArrow;
    private KeyCode mR = KeyCode.RightArrow;
    private KeyCode mUp = KeyCode.UpArrow;
    private KeyCode mDown = KeyCode.DownArrow;
    private KeyCode mPull = KeyCode.P;

    private KeyCode bL = KeyCode.A;
    private KeyCode bR = KeyCode.D;
    private KeyCode bUp = KeyCode.W;
    private KeyCode bDown = KeyCode.S;
    private KeyCode bPull = KeyCode.E;

    private KeyCode establihBond = KeyCode.B;
    private KeyCode breakBond = KeyCode.V;
    private KeyCode chanel = KeyCode.C;

    public DebugController()
    {
        mL = KeyCode.LeftArrow;
        mR = KeyCode.RightArrow;
        mUp = KeyCode.UpArrow;
        mDown = KeyCode.DownArrow;
        mPull = KeyCode.P;

        bL = KeyCode.A;
        bR = KeyCode.D;
        bUp = KeyCode.W;
        bDown = KeyCode.S;
        bPull = KeyCode.E;

        establihBond = KeyCode.B;
        breakBond = KeyCode.V;
        chanel = KeyCode.C;
    }

    public KeyCode ML
    {
        get
        {
            return mL;
        }

        set
        {
            mL = value;
        }
    }

    public KeyCode MR
    {
        get
        {
            return mR;
        }

        set
        {
            mR = value;
        }
    }

    public KeyCode MUp
    {
        get
        {
            return mUp;
        }

        set
        {
            mUp = value;
        }
    }

    public KeyCode MDown
    {
        get
        {
            return mDown;
        }

        set
        {
            mDown = value;
        }
    }

    public KeyCode MPull
    {
        get
        {
            return mPull;
        }

        set
        {
            mPull = value;
        }
    }

    public KeyCode BL
    {
        get
        {
            return bL;
        }

        set
        {
            bL = value;
        }
    }

    public KeyCode BR
    {
        get
        {
            return bR;
        }

        set
        {
            bR = value;
        }
    }

    public KeyCode BUp
    {
        get
        {
            return bUp;
        }

        set
        {
            bUp = value;
        }
    }

    public KeyCode BDown
    {
        get
        {
            return bDown;
        }

        set
        {
            bDown = value;
        }
    }

    public KeyCode BPull
    {
        get
        {
            return bPull;
        }

        set
        {
            bPull = value;
        }
    }

    public KeyCode EstablihBond
    {
        get
        {
            return establihBond;
        }

        set
        {
            establihBond = value;
        }
    }

    public KeyCode BreakBond
    {
        get
        {
            return breakBond;
        }

        set
        {
            breakBond = value;
        }
    }

    public KeyCode Chanel
    {
        get
        {
            return chanel;
        }

        set
        {
            chanel = value;
        }
    }


}
