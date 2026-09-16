page 50104 "MTR Meeting Room Booking List"
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Lists;
    SourceTable = "MTR Meeting Room Booking";

    layout
    {
        area(Content)
        {
            repeater(Lines)
            {
                field(EntryNo; Rec.EntryNo) { }
                field(RoomCode; Rec.RoomCode) { }
                field(BookingDate; Rec.BookingDate) { }
                field(StartTime; Rec.StartTime) { }
                field(EndTime; Rec.EndTime) { }
                field(BookedBy; Rec.BookedBy) { }
                field(Status; Rec.Status) { }
            }
        }
    }
}