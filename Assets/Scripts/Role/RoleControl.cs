using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class RoleControl : MonoBehaviour
{
    Dictionary<string, bool> triggerLockMap = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnAttack(int keyCode,bool isTrigger)
    {
        transform.GetComponent<RoleBase>().Attack();
    }
    public void OnSprint(int keyCode,bool isTrigger)
    {
        transform.GetComponent<RoleBase>().Sprint(isTrigger);
    }
    void CheckMoveDirUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 moveDir = new Vector2(horizontalInput, verticalInput);

        if (horizontalInput == 0 && verticalInput == 0)
        {
            GetComponent<RoleBase>().Move(false, moveDir);
        }
        else
        {
            GetComponent<RoleBase>().Move(true, moveDir);
        }
    }
    void CheckChangeFaceUpdate()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        Vector2 moveDir = new Vector2(mouseX, -mouseY);

        GetComponent<RoleBase>().ChangeFace(moveDir);
    }
    void CheckTriggerOppeatorUpdate()
    {
        bool GetPressUp(int key)
        {
            if (key < 5)
            {
                return Input.GetMouseButtonUp(key);
            }
            else
            {
                return Input.GetKeyUp((KeyCode)key);
            }
        }
        bool GetPress(int key)
        {
            if (key < 5)
            {
                return Input.GetMouseButton(key);
            }
            else
            {
                return Input.GetKey((KeyCode)key);
            }
        }
        int GetTriggerKey(CfgStuct cfg)
        {
            for(var i = 0; i < cfg.confirmKeyList.Count; i++)
            {
                if (GetPress(cfg.confirmKeyList[i]) || GetPressUp(cfg.confirmKeyList[i])){
                    return cfg.confirmKeyList[i];
                }
            }
            return -1;
        }
        void RunMethod(string methodName, int keyCode, bool isTrigger)
        {
            Type type = typeof(RoleControl);
            string result = methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
            var method = type.GetMethod("On" + result);
            object[] parameters = new object[] { keyCode, isTrigger};
            if (method != null)
            {
                method.Invoke(this, parameters);
            }
        }
        var cfg = RoleInputCfg.cfg;
        foreach(KeyValuePair<string,CfgStuct> kvp in cfg)
        {
            var triggerKey = GetTriggerKey(kvp.Value);
            if (triggerKey > -1)
            {
                bool isTriggerLock = triggerLockMap.ContainsKey(kvp.Key) && triggerLockMap[kvp.Key];
                if (isTriggerLock)
                {
                    if (GetPressUp(triggerKey))
                    {
                        triggerLockMap[kvp.Key] = false;
                        RunMethod(kvp.Key, triggerKey, false);
                    }
                }
                else
                {
                    if (kvp.Value.isTriggerOnce)
                    {
                        if (triggerLockMap.ContainsKey(kvp.Key))
                        {
                            triggerLockMap[kvp.Key] = true;
                        }
                        else
                        {
                            triggerLockMap.Add(kvp.Key, true);
                        }
                    }
                    RunMethod(kvp.Key, triggerKey, true);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckMoveDirUpdate();
        CheckChangeFaceUpdate();
        CheckTriggerOppeatorUpdate();

    }
}
