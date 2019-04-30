using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cmdr2.ui {

    public interface IPointer {
        bool GetClickButton();
        bool GetClickButtonDown();
        bool GetTriggerButton();
        bool GetTriggerButtonDown();
        bool GetBackButtonDown();
        Quaternion GetOrientation();
        void SetVisible(bool state);
        Transform GetLaser();
    }

}