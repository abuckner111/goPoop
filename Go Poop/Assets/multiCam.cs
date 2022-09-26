using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class multiCam : MonoBehaviour
{
    [Header("Setup")]
    public Camera       selfCam;
    public GameObject   target;

    //Target data
    private bool        _target;
    private Transform   targetPos;
    private Vector3     _targetPos;
    private Vector3     targetVel;


    [Header("Camera Parameters")]
    public Vector3      OriginOffset;
    public float        orbitAngle = 2.5f;
    public float        orbitRadius = 6f;
    public Vector3      orbitPos = Vector3.zero;
    public float        orbitVelScaler = 0.01f;



    [Header("Camera Settings")]
    public bool         firstPerson = false;
    private bool         _firstPerson;
    public bool         thirdPerson = false;
    private bool         _thirdPerson;
    private bool         _defView;
    public bool         orbitMode = true;
    private bool         _orbitMode;
    public bool         lockedMode = false;
    private bool         _lockedMode;
    private bool         _defMode;


    private void restorePerspectiveMode()
    {
        thirdPerson = _defView;
        firstPerson = !_defView;
        _firstPerson = firstPerson;
        _thirdPerson = thirdPerson;
    }

    private void restoreViewMode()
    {
        orbitMode = _defMode;
        lockedMode = !_defMode;
        _orbitMode = orbitMode;
        _lockedMode = lockedMode;

    }

    private void setDefaults()
    {
        if(firstPerson == true && thirdPerson == false)
        {
            _defView = false;
        }
        else if(firstPerson == false && thirdPerson == true)
        {
            _defView = true;
        }
        else
        {
            _defView = false;
        }

        if(orbitMode == true && lockedMode == false)
        {
            _defMode = true;
        }
        else if(orbitMode == false && lockedMode == true)
        {
            _defMode = false;
        }
        else
        {
            _defMode = true;
        }

        _firstPerson = firstPerson;
        _thirdPerson = thirdPerson;
        _orbitMode = orbitMode;
        _lockedMode = lockedMode;
    }

    private void restoreDefaults()
    {
        restorePerspectiveMode();
        restoreViewMode();
    }


    public void cameraLookInput(InputAction.CallbackContext ctx)
    {

    }

    private void validateSettings()
    {
        if(firstPerson && thirdPerson)
        {
            if(_firstPerson != firstPerson && _thirdPerson == thirdPerson)
            {
                _firstPerson = firstPerson;

                thirdPerson = !thirdPerson;
                _thirdPerson = thirdPerson;
            }

            else if(_firstPerson == firstPerson && _thirdPerson != thirdPerson)
            {
                firstPerson = !firstPerson;
                _firstPerson = firstPerson;

                _thirdPerson = thirdPerson;
            }
            else
            {
                restorePerspectiveMode();
            }

        }
        if(!firstPerson && !thirdPerson)
        {
            restorePerspectiveMode();
        }

        if(orbitMode && lockedMode)
        {
            if(_orbitMode != orbitMode && _lockedMode == lockedMode)
            {
                _orbitMode = orbitMode;

                lockedMode = !lockedMode;
                _lockedMode = lockedMode;
            }

            else if(_orbitMode == orbitMode && _lockedMode != lockedMode)
            {
                orbitMode = !orbitMode;
                _orbitMode = orbitMode;

                _lockedMode = lockedMode;
            }
            else
            {
                restoreViewMode();
            }

        }
        if(!orbitMode && !lockedMode)
        {
            restoreViewMode();
        }
    }

    private void trackTargetVel()
    {
        if(_target == true)
        {
            targetVel = (targetPos.position - _targetPos)/Time.deltaTime;
            _targetPos = targetPos.position;
        }
    }

    private void checkTargetStatus()
    {
        if(target != null && _target == false)
        {
            targetPos = target.transform;
            _target = true;
        }
        if(target == null && _target == true)
        {
            _target = false;
        }
    }

    private void locked()
    {
        if(firstPerson)
        {

        }
        else if(thirdPerson)
        {

        }
    }

    private void orbit()
    {
        if(firstPerson)
        {
            locked();
        }
        else if(thirdPerson)
        {
            trackTargetVel();
            orbitPos = (orbitPos + targetVel*orbitVelScaler).normalized;
            selfCam.transform.position = new Vector3(0f,orbitAngle,0f)-orbitPos*orbitRadius+targetPos.position;
            selfCam.transform.LookAt(targetPos.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        setDefaults();
        checkTargetStatus();
    }

    // Update is called once per frame
    void Update()
    {
        checkTargetStatus();
        validateSettings();

        if(orbitMode)
        {
            orbit();
        }
        if(lockedMode)
        {
            locked();
        }
    }

}
