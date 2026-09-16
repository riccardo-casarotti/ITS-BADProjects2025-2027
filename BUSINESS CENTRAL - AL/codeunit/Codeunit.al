codeunit 50100 "MTR Custom Mgt."
{
    //controlla se una sala è libera in un intervallo orario.
    procedure CheckAvailability(RoomCode: Code[20]; BookingDate: Date; StartTime: Time; EndTime: Time; EntryNoToExclude: Integer): Boolean
    var
        Booking2: Record "MTR Meeting Room Booking";
    begin
        Booking2.Reset();
        Booking2.SetRange(RoomCode, RoomCode);
        Booking2.SetRange(BookingDate, BookingDate);
        Booking2.SetFilter(EntryNo, '<>%1', EntryNoToExclude);
        Booking2.SetFilter(StartTime, '<%1', EndTime);
        Booking2.SetFilter(EndTime, '>%1', StartTime);
        Booking2.SetFilter(Status, '<>%1', Booking2.Status::Cancelled);

        exit(Booking2.IsEmpty());
    end;
//chiama chek avaliability e se trova conflitto mi da errore 
    procedure CheckRoomAvailability(Booking: Record "MTR Meeting Room Booking")
    var
        RoomConflictErr: Label 'Room %1 is already booked in that time slot on %2.';
    begin
        if not CheckAvailability(Booking.RoomCode, Booking.BookingDate, Booking.StartTime, Booking.EndTime, Booking.EntryNo) then
            Error(RoomConflictErr, Booking.RoomCode, Booking.BookingDate);
    end;
//controlla il flag IsAvailable della sala.
    procedure CheckRoomIsAvailable(Room: Record "MTR Meeting Room")
    var
        RoomNotAvailableErr: Label 'Room %1 is not available and cannot be booked.';
    begin
        if not Room.IsAvailable then
            Error(RoomNotAvailableErr, Room.RoomCode);
    end;
//crea una nuova prenotazione da codice
    procedure CreateReservation(RoomCode: Code[20]; BookingDate: Date; StartTime: Time; EndTime: Time; BookedBy: Code[30]; var NewBooking: Record "MTR Meeting Room Booking")
    var
        RoomConflictErr: Label 'Room %1 is already booked in that time slot.';
    begin
        if not CheckAvailability(RoomCode, BookingDate, StartTime, EndTime, 0) then
            Error(RoomConflictErr, RoomCode);

        NewBooking.Init();
        NewBooking.RoomCode := RoomCode;
        NewBooking.BookingDate := BookingDate;
        NewBooking.StartTime := StartTime;
        NewBooking.EndTime := EndTime;
        NewBooking.BookedBy := BookedBy;
        NewBooking.Status := NewBooking.Status::Confirmed;
        NewBooking.Insert(true);
    end;
//alcola le ore tra StartTime e EndTime e restituisce il risultato in ore decimali.
    procedure CalculateDuration(StartTime: Time; EndTime: Time): Decimal
    begin
        exit((EndTime - StartTime) / 1000 / 3600);
    end;

    procedure DetermineRoomStatus(Room: Record "MTR Meeting Room"): Enum "MTR Room Status"
    var
        Booking: Record "MTR Meeting Room Booking";
    begin
        if not Room.IsAvailable then
            exit("MTR Room Status"::Manutenzione);

        Booking.Reset();
        Booking.SetRange(RoomCode, Room.RoomCode);
        Booking.SetRange(BookingDate, Today());
        Booking.SetFilter(StartTime, '<=%1', Time());
        Booking.SetFilter(EndTime, '>%1', Time());
        Booking.SetFilter(Status, '<>%1', Booking.Status::Cancelled);

        if not Booking.IsEmpty() then
            exit("MTR Room Status"::Occupata);

        exit("MTR Room Status"::Libera);
    end;
//aggiorna lo stato di tutte le sale in base alla disponibilità e alle prenotazioni correnti.
    procedure UpdateAllRoomStatuses()
    var
        Room: Record "MTR Meeting Room";
        Booking: Record "MTR Meeting Room Booking";
    begin
        if Room.FindSet(true) then
            repeat
                if Room.IsAvailable then
                    Room.Status := Room.Status::Libera
                else
                    Room.Status := Room.Status::Manutenzione;
                Room.Modify();
            until Room.Next() = 0;

        Booking.Reset();
        Booking.SetRange(BookingDate, Today());
        Booking.SetFilter(StartTime, '<=%1', Time());
        Booking.SetFilter(EndTime, '>%1', Time());
        Booking.SetFilter(Status, '<>%1', Booking.Status::Cancelled);

        if Booking.FindSet() then
            repeat
                if Room.Get(Booking.RoomCode) then
                    if Room.IsAvailable then begin
                        Room.Status := Room.Status::Occupata;
                        Room.Modify();
                    end;
            until Booking.Next() = 0;
    end;
//restituisce un elenco di sale disponibili per la prenotazione.
    procedure GetAvailableRooms(): List of [Code[20]]
    var
        Room: Record "MTR Meeting Room";
        AvailableRooms: List of [Code[20]];
    begin
        Room.Reset();
        Room.SetRange(IsAvailable, true);
        if Room.FindSet() then
            repeat
                AvailableRooms.Add(Room.RoomCode);
            until Room.Next() = 0;

        exit(AvailableRooms);
    end;
//restituisce un dizionario con il numero di prenotazioni per ogni sala.
    procedure GetMostUsedRooms(): Dictionary of [Code[20], Integer]
    var
        Room: Record "MTR Meeting Room";
        RoomStats: Dictionary of [Code[20], Integer];
    begin
        Room.Reset();
        if Room.FindSet() then
            repeat
                Room.CalcFields("No. of Bookings");
                RoomStats.Add(Room.RoomCode, Room."No. of Bookings");
            until Room.Next() = 0;

        exit(RoomStats);
    end;
}