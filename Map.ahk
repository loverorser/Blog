RM()
{
    ToolTip()
}

SwitchPad := True
RShift::
{

    global
    if(SwitchPad == True){
        SwitchPad := False
        ToolTip("关闭小键盘映射")
    }else{
        SwitchPad := True
        ToolTip("打开小键盘映射")
    }
    Send "{NumLock}"


    SetTimer(RM, -1000)

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
    if(SwitchPad == True)
        Send "5"
    else
        Send "{Home}"
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
        Send "{RControl}"
}
