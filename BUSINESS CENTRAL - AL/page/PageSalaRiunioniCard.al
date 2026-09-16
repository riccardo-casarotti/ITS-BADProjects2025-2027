page 50100 "MTR Meeting Room Card"
{
    PageType = Card;
    ApplicationArea = All;
    SourceTable = "MTR Meeting Room";

    layout
    {
        area(Content)
        {
            group(General)
            {
                Caption = 'General';
                field(RoomCode; Rec.RoomCode) { }
                field(Description; Rec.Description) { }
                field(Capacity; Rec.Capacity) { }
                field(Location; Rec.Location) { }
                field(IsAvailable; Rec.IsAvailable) { }
                field(Status; Rec.Status) { }
                field("No. of Bookings"; Rec."No. of Bookings") { }
                field("Total Hours Booked"; Rec."Total Hours Booked") { }
            }
            part(BookingSub; "MTR Meeting Room Booking Sub")
            {
                Caption = 'Bookings';
                ApplicationArea = All;
                SubPageLink = RoomCode = field(RoomCode);
            }
        }
    }

    trigger OnAfterGetCurrRecord()
    begin
        Rec.CalcFields("No. of Bookings", "Total Hours Booked");
    end;
}
