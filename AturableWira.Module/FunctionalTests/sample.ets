;#DropDB AturableWiraEasyTest

#Application AturableWira.Win
#Application AturableWira.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef AturableWira.Win

*Action About...
 
*Action OK

#EndIf

#IfDef AturableWira.Web
*Action Navigation(My Details)
#EndIf
