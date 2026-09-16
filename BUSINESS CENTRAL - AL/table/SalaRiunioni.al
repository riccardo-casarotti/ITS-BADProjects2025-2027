table 50100 "MTR Meeting Room"
{
    DataClassification = CustomerContent;
    LookupPageId = "MTR Meeting Room List";
    DrillDownPageId = "MTR Meeting Room List";

    fields
    {
        field(1; RoomCode; Code[20])
        {
            Caption = 'Code';
            Editable = false;
        }
        field(2; Description; Text[100])
        {
            DataClassification = ToBeClassified;

        }
        field(3; Capacity; Integer)
        {
            Caption = 'Capienza massima';

        }
        field(4; Location; Text[30])
        {
            Caption = 'Piano/Edificio';
        }
        field(5; IsAvailable; Boolean)
        {
            Caption = 'Disponibile';
        }

        field(6; "No. of Bookings"; Integer)
        {
            Caption = 'No. of Bookings';
            FieldClass = FlowField;
            CalcFormula = count("MTR Meeting Room Booking" where(RoomCode = field(RoomCode), BookingDate = field("Date Filter"), Status = filter(<> Cancelled)));
            Editable = false;
        }
        field(7; "Total Hours Booked"; Decimal)
        {
            Caption = 'Total Hours Booked';
            FieldClass = FlowField;
            CalcFormula = sum("MTR Meeting Room Booking"."Duration (Hours)" where(RoomCode = field(RoomCode), BookingDate = field("Date Filter"), Status = filter(<> Cancelled)));
            Editable = false;
        }
        field(8; "Date Filter"; Date)
        {
            Caption = 'Date Filter';
            FieldClass = FlowFilter;
        }
        field(9; Status; Enum "MTR Room Status")
        {
            Caption = 'Stato Sala';
            Editable = false;
        }
    }

    keys
    {
        key(Pk; RoomCode)
        {
            Clustered = true;
        }

    }
    fieldgroups
    {
        fieldgroup(Dropdown; RoomCode, Description) { }
    }

    //auto genera il room code, perchè lo avevo messo come non editabile, ma non usciva fuori da solo  
    trigger OnInsert()
    var
        LastRoom: Record "MTR Meeting Room";
        NextNo: Integer;
    begin
        if RoomCode <> '' then
            exit;

        LastRoom.Reset();
        LastRoom.SetFilter(RoomCode, 'SALA*');
        if LastRoom.FindLast() then
            Evaluate(NextNo, CopyStr(LastRoom.RoomCode, 5));

        NextNo += 1;
        RoomCode := 'SALA' + Format(NextNo, 3, '<Integer,3><Filler Character,0>');
    end;

}