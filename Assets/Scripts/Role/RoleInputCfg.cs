using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public struct CfgStuct
{
    public List<int> confirmKeyList;
    public bool isTriggerOnce; 

}
public static class RoleInputCfg
{
    public static Dictionary<string, CfgStuct> cfg = new Dictionary<string, CfgStuct>() {
        {
            "attack",
            new CfgStuct()
            {
                confirmKeyList = new List<int>(){
                    (int)  MouseButton.Left
                }
            } 
        },
        {
            "sprint",
            new CfgStuct()
            {
                confirmKeyList = new List<int>(){
                    (int)  KeyCode.LeftShift
                },
                isTriggerOnce = true
            }
        },
    };
}
