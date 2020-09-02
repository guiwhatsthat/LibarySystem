#Edit this two values
$ServerInsatnce = "DESKTOP-35P8P5I\SQLEXPRESS"
$SQLInstallationPath = "E:\SQL\SQL Server Installation\MSSQL15.SQLEXPRESS\MSSQL\Data"


#Do not edit
#Create DB
$ScriptPath = Join-Path -Path $PSScriptRoot -ChildPath "CREATE_DATABASE_Librarysystem.sql"
$Content = Get-Content -Path $ScriptPath
$NewContent = $Content -replace 'D:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQLEXPRESS\\MSSQL\\DATA', $SQLInstallationPath
$NewContent | Set-Content -Path $ScriptPath -Force
Invoke-Sqlcmd -InputFile $ScriptPath -ServerInstance $ServerInsatnce #-database "mydatabase"

#Run other Scripts
$ScriptNames = "CREATE_TABLE_Book","CREATE_TABLE_User","CREATE_TABLE_Rent","CREATE_TABLE_Reservation","INSERT_SOMEDATA","TRIGGER_SET_AVAIABLE_WHEN_Return_date_INSERTED","TRIGGER_SET_NOTAVAIABLE_WHEN_Rent_ENTRY_INSERTED_AND_AMOUNT_REACHED","TRIGGER_SET_RESERVATION_DONE_WHEN_RENT","TRIGGER_SET_End_rentdate","STORED_PROCEDURE_TOPBOOKS","V_Reservations","V_Rents"
$Scripts = [System.Collections.ArrayList]@()
Foreach ($ScriptName in $ScriptNames) {
    Invoke-Sqlcmd -InputFile $(Join-Path -Path $PSScriptRoot -ChildPath "$($ScriptName).sql") -ServerInstance $ServerInsatnce -Database "Librarysystem"
}
