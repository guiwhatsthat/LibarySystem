USE Librarysystem;

-- INSERT Book
INSERT INTO [Librarysystem].[dbo].[Book] (Name,ISBN,Author,Publisher,Amount,Avaiable)
VALUES ('Harry Potter 1 und der Stein der Weisen','978-3-551-35401-3','J. K. Rowling','Carlsen',3,1),
('Harry Potter und der Gefangene von Askaban','978-3-551-35403-7','J. K. Rowling','Carlsen',2,1),
('Corona Fehlalarm?','978-3-99060-191-4','Sucharit Bhakdi, Karina Reiss','Goldegg Verlag',4,1),
('Imperium USA','978-3-280-05708-7','Daniele Ganser','Orell Füssli Verlag',2,0),
('Keine Panik, ist nur Technik','978-3-8338-7546-5','Kenza Ait Si Abbou','Graefe und Unzer Autorenverlag ein Imprint von GRÄFE UND UNZER Verlag GmbH',1,0),
('Wie viel wiegt mein Leben?','978-3-95910-288-9','Antonia C. Wesseling','Eden Books - Ein Verlag der Edel Germany GmbH',7,1);

-- INSERT USER
INSERT INTO [Librarysystem].[dbo].[User] (Username,Password,Surname,Last_name,Adress,ZIP,City,Write,Write_rent)
VALUES ('Fabrice','1234','Fabrice','Reiser','Sulgenauweg 31',3007,'Bern',0,0),
('Steven','1234','Steven','Gasser','Sulgenauweg 31',3007,'Bern',0,0),
('Maexu','1234','May','Mustermann','Musterweg 10',3007,'Bern',0,0),
('Bidu','1234','Beat','Testmann','Testweg 11',3013,'Bern',0,0),
('Henae','1234','Heinz','Wasauchimmer','Irgendeinweg 12',3013,'Bern',0,0),
('Roefe','1234','Rolf','Irgendwas','Irgendwasstrasse 13',3013,'Bern',0,0);
-- INSERT Rent

INSERT INTO [Librarysystem].[dbo].[Rent] (Lend_date,Return_date,End_rentdate,FKey_Book,FKey_User)
-- Aktuell vermietete Buecher
VALUES (2020-07-26,NULL,2020-08-25,1,1),
(2020-07-28,NULL,2020-08-27,1,3),
(2020-07-29,NULL,2020-08-28,3,2),
(2020-08-01,NULL,2020-08-30,4,1),
(2020-08-03,NULL,2020-09-02,4,4),
(2020-08-05,NULL,2020-09-04,5,5),
(2020-08-10,NULL,2020-09-09,6,1),
(2020-07-08,NULL,2020-08-09,6,3),
-- In vergangenheit vermietete Buecher
(2020-06-13,2020-06-29,2020-07-12,1,6),
(2020-06-16,2020-07-20,2020-07-15,1,5),
(2020-06-18,2020-07-10,2020-07-17,3,5),
(2020-06-25,2020-07-30,2020-07-24,5,4),
(2020-07-03,2020-08-02,2020-08-02,6,2);

-- INSERT Reservation
INSERT INTO [Librarysystem].[dbo].[Reservation] (Reservation_date,Done,FKey_Book,FKey_User)
-- Reservation, welche jetzt Rent sind
VALUES (2020-07-25,1,4,1),
(2020-07-15,1,4,4),
(2020-08-03,1,6,1),
-- Aktuelle Reservationen
(2020-08-18,0,3,4),
(2020-08-21,0,6,5),
(2020-08-24,0,6,6);
