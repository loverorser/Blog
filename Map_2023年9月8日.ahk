RM()
{
    ToolTip()
}
IsRShiftDown := False
; /
Is_Slash_Down := False
Is_QuestionMark_Down := False
Is_Home_Down := False
Is_Delete_Down := False
~/::
{
    global
    Is_Slash_Down := True
}
~?::
{
    global
    Is_QuestionMark_Down := True
}
~+Home::
{
    global
    Is_Home_Down := True
}
~+Delete::
{
    global
    Is_Delete_Down := True
}
SwitchPad := True
DelayTime := 0
~RShift Up::
{
    global
    IsRShiftDown := False

    if(Is_QuestionMark_Down == True or Is_Home_Down == True or Is_Delete_Down == True){

    }else{


    if((A_TickCount-DelayTime)>220){
        Send "{LShift}"
        ;ToolTip("大于")
    }else{
        ;Send "^{Space}"
        Send "{LShift}"
        if(SwitchPad == True){
            SwitchPad := False
            SetNumLockState False
            ToolTip("关闭小键盘映射")
            SetTimer(RM, -1000)
        }else{
            SwitchPad := True
            SetNumLockState True
            ToolTip("打开小键盘映射")
            SetTimer(RM, -1000)
        }
    }
    }
    

    ;Send "{NumLock}"
    ;Send "{RShift}"




}
~RShift::{
    global
    if(ISRShiftDown == False){
        IsRShiftDown := True
        IS_Slash_Down := False
        Is_QuestionMark_Down := False
        Is_Home_Down := False
        Is_Delete_Down := False
        DelayTime := A_TickCount
    }
}
PrintScreen::
{
    if(SwitchPad == True)
        Send "7"
    else
        Send "{PrintScreen}"
}
ScrollLock::
{
    if(SwitchPad == True)
        Send "8"
    else
        Send "{ScrollLock}"
}
Pause::
{
    if(SwitchPad == True)
        Send "9"
    else
        Send "{Pause}"
}


Insert::
{
    if(SwitchPad == True)
        Send "4"
    else
        Send "{Insert}"
}
Home::
{
    if(SwitchPad == True){
        Send "5"
    }else{
        Send "{Home}"
    }
}
PgUp::
{
    if(SwitchPad == True)
        Send "6"
    else
        Send "{PgUp}"
}


Delete::
{
    if(SwitchPad == True)
        Send "1"
    else
        Send "{Delete}"
}
End::
{
    if(SwitchPad == True)
        Send "2"
    else
        Send "{End}"
}
PgDn::
{
    if(SwitchPad == True)
        Send "3"
    else
        Send "{PgDn}"
}

RControl::
{
    if(SwitchPad == True)
        Send "0"
    else
        Send "{RControl Down}"
}

RControl UP::
{
    if(SwitchPad == False)
        Send "{RControl UP}"
}