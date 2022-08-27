#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.

#SingleInstance force
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.


^F2::
SetKeyDelay -1 ;

WinActivate,, Sibelius
CoordMode, Mouse , Window

Loop Files, D:\tmp\ocr2\sib\*.sib
{

; open

click 20, 50
Sleep 1000

click 20, 150
Sleep 1000

Send %A_LoopFileName%
Send {Enter}

sleep 3000


; export


Sleep 1000

click 20, 50
Sleep 1000

click 20, 380
Sleep 1000

click 350, 300
Sleep 1000

click 500, 350
Sleep 1500

Send, {ENTER}

Sleep 5000

; close
click 20, 50
Sleep 1000

click 20, 170
Sleep 1000

click 320, 110
Sleep 1000

}

 
MsgBox done

