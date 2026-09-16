page 50103 "MTR Meeting Room Booking Sub"
{
    PageType = ListPart;
    ApplicationArea = All;
    SourceTable = "MTR Meeting Room Booking";

    layout
    {
        area(Content)
        {
            repeater(Lines)
            {
                field(RoomCode; Rec.RoomCode) { }
                field(EntryNo; Rec.EntryNo) { }
                field(BookingDate; Rec.BookingDate) { }
                field(StartTime; Rec.StartTime) { }
                field(EndTime; Rec.EndTime) { }
                field(BookedBy; Rec.BookedBy) { }
                field(Status; Rec.Status) { }
            }
        }
    }

    trigger OnNewRecord(BelowxRec: Boolean)
    var
        RoomFilter: Text;
    begin
        RoomFilter := Rec.GetFilter(RoomCode);
        if RoomFilter <> '' then
            Rec.RoomCode := CopyStr(RoomFilter, 1, MaxStrLen(Rec.RoomCode));
    end;
}