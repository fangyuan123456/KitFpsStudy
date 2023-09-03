using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoleAniBase : MonoBehaviour
{
    [HideInInspector] public bool isMove = false;
    [HideInInspector] public bool isSpint = false;
    [HideInInspector] public bool isJump = false;
    [HideInInspector] public bool isAttack = false;
    [HideInInspector] public bool isCrouching = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetIsMove(bool _b)
    {
        isMove = _b;
    }
    public void SetIsSprint(bool _b)
    {
        isSpint = _b;
    }
    public void SetIsJump(bool _b)
    {
        isJump = _b;
    }
    public void SetIsAttack(bool _b)
    {
        isAttack = _b;
    }
    public void SetIsCrouching(bool _b)
    {
        isCrouching = _b;
    }
    // Update is called once per frame
    public void Update()
    {
        
    }
}
