INSERT INTO [DeliverySlot]
           ([Slot])
     VALUES
           ('11 AM to 2 PM'),
           ('2 PM to 5 PM'),
           ('5 PM to 8 PM')
GO



Alter table Users
Add ProfileComplete bit 

Update Users
set ProfileComplete = 0


select * from DeliverySlot