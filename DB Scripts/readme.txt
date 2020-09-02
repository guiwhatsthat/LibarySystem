DB mit Beispiel Data erstellen

Automatisch erstellen:
PS Skript Create_DB_Script.ps1 öffnen
Die zwei Variablen anpassen
Ausführen mit F5


Manuell erstellen:
Reihenfolge

1:
CREATE_DATABASE_Librarysystem

2:
CREATE_TABLE_Book
CREATE_TABLE_User

3 (Weil FKs):
CREATE_TABLE_Rent
CREATE_TABLE_Reservation

4:
INSERT_SOMEDATA_CUSTOMER_BOOK

5:
TRIGGER_SET_AVAIABLE_WHEN_Return_date_INSERTED
TRIGGER_SET_NOTAVAIABLE_WHEN_Rent_ENTRY_INSERTED_AND_AMOUNT_REACHED
TRIGGER_SET_RESERVATION_DONE_WHEN_RENT
TRIGGER_SET_End_rentdate
V_Reservations
V_Rents