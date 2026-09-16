table 50101 "MTR Meeting Room Booking"
{
    DataClassification = CustomerContent;

    fields
    {
        field(1; EntryNo; Integer)
        {
            Caption = 'Entry No.';
            AutoIncrement = true;
            Editable = false;
        }
        field(2; RoomCode; Code[20])
        {
            Caption = 'Codice Sala';
            TableRelation = "MTR Meeting Room".RoomCode;

            trigger OnValidate()
            var
                Room: Record "MTR Meeting Room";
                CustomMgt: Codeunit "MTR Custom Mgt.";
            begin
                if Rec.RoomCode = '' then
                    exit;

                Room.Get(Rec.RoomCode);
                CustomMgt.CheckRoomIsAvailable(Room);
            end;
        }

        field(3; BookingDate; Date)
        {
            Caption = 'Data Prenotazione';


            trigger OnValidate()
            var
                DateInPastErr: Label 'Booking Date cannot be in the past.';
            begin
                if Rec.BookingDate < Today() then
                    Error(DateInPastErr);
            end;
        }

        field(4; StartTime; Time)
        {
            Caption = 'Ora Inizio Prenotazione';
            trigger OnValidate()
            begin
                Rec.TestField(StartTime);
            end;
        }

        field(5; EndTime; Time)
        {
            Caption = 'Ora Fine Prenotazione';

            trigger OnValidate()
            begin
                if EndTime <= StartTime then
                    Error('L''ora di fine prenotazione deve essere maggiore dell''ora di inizio prenotazione.');

                Rec."Duration (Hours)" := (Rec.EndTime - Rec.StartTime) / 1000 / 3600;
            end;

        }
        field(6; BookedBy; Code[30])
        {
            Caption = 'Prenotato da';

        }
        field(7; Status; enum "MTR Booking Status")
        {
            Caption = 'Stato Prenotazione';
        }

        field(8; "Duration (Hours)"; Decimal)
        {
            Caption = 'Duration (Hours)';
            DecimalPlaces = 0 : 2;
            Editable = false;
        }


    }

    keys
    {
        key(Pk; EntryNo)
        {
            Clustered = true;
        }
    }


    trigger OnInsert()
    var
        CustomMgt: Codeunit "MTR Custom Mgt.";
    begin
        CustomMgt.CheckRoomAvailability(Rec);
    end;

    trigger OnModify()
    var
        CustomMgt: Codeunit "MTR Custom Mgt.";
    begin
        CustomMgt.CheckRoomAvailability(Rec);
    end;

}