using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoleBase : MonoBehaviour
{
    public RoleAniBase roleAni;
    public CharacterController controller;
    public float moveSpeed;
    public float rotationSpeed;
    private float sprintSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Sprint(bool isTrigger)
    {
        sprintSpeed = isTrigger ? 2 : 1;
    }
    public void Attack()
    {

    }
    public void Move(bool isTrigger,Vector2 moveDir)
    {
        controller.Move(transform.TransformDirection(new Vector3(moveDir.x * moveSpeed*sprintSpeed*Time.deltaTime, 0, moveDir.y * moveSpeed * sprintSpeed * Time.deltaTime)));
        roleAni?.SetIsMove(isTrigger);
    }
    public float minEyesAngle = 0;
    public float maxEyesAngle = 0;
    public void ChangeFace(Vector2 rotationOffsetVec2)
    {
        var localEulerAngles = transform.localEulerAngles;
        var newEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y + rotationOffsetVec2.x * rotationSpeed, localEulerAngles.z);
        transform.localEulerAngles = newEulerAngles;


        var rootNode = transform.Find("Root");
        var rootLocalEulerAngles = rootNode.transform.localEulerAngles;
        var rotationX = rootLocalEulerAngles.x;
        if (rotationX > 90)
        {
            rotationX = rotationX - 360;
        }
        var newAngleX = Math.Clamp(rotationX + rotationOffsetVec2.y * rotationSpeed, minEyesAngle, maxEyesAngle);
        var rootNewEulerAngles = new Vector3(newAngleX, rootLocalEulerAngles.y, rootLocalEulerAngles.z);
        rootNode.transform.localEulerAngles = rootNewEulerAngles;
    }
    public void Jump()
    {
        if (!controller.isGrounded)
        {
            roleAni?.SetIsJump(true);
        }
    }
    public void Crouching(bool isTrigger)
    {
        roleAni?.SetIsCrouching(isTrigger);
    }
    public void Attack(bool isTrigger)
    {
        roleAni?.SetIsAttack(isTrigger);
    }
    // Update is called once per frame
    private float dropSpeed;
    public float dropGravity;
    public float dropMaxSpeed;
    void dropUpdate()
    {
        if (!controller.isGrounded)
        {
            dropSpeed += dropGravity * Time.deltaTime;
            if(dropSpeed< dropMaxSpeed)
            {
                dropSpeed = dropMaxSpeed;
            }
            controller.Move(new Vector3(0, dropSpeed*Time.deltaTime, 0));
        }
    }
    protected void Update()
    {
        dropUpdate();
    }
}
