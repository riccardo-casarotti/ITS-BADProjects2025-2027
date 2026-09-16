page 50101 "MTR Meeting Room List"
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Lists;
    SourceTable = "MTR Meeting Room";
    CardPageId = "MTR Meeting Room Card";

    layout
    {
        area(Content)
        {
            repeater(Lines)
            {
                field(RoomCode; Rec.RoomCode) { }
                field(Description; Rec.Description) { }
                field(Capacity; Rec.Capacity) { }
                field(Location; Rec.Location) { }
                field(IsAvailable; Rec.IsAvailable) { }
            }
        }
    }

    actions
    {
        area(Processing)
        {
            action(UpdateRoomStatuses)
            {
                Caption = 'Aggiorna Stato Sale';
                Image = Refresh;
                ApplicationArea = All;

                trigger OnAction()
                var
                    CustomMgt: Codeunit "MTR Custom Mgt.";
                    StatusUpdatedMsg: Label 'Stato delle sale aggiornato.';
                begin
                    CustomMgt.UpdateAllRoomStatuses();
                    CurrPage.Update();
                    Message(StatusUpdatedMsg);
                end;
            }

            action(ShowMostUsedRooms)
            {
                Caption = 'Sale più Utilizzate';
                ApplicationArea = All;

                trigger OnAction()
                var
                    CustomMgt: Codeunit "MTR Custom Mgt.";
                    RoomStats: Dictionary of [Code[20], Integer];
                    RoomCode: Code[20];
                    ResultTxt: Text;
                    StatsLbl: Label '%1: %2 prenotazioni';
                begin
                    RoomStats := CustomMgt.GetMostUsedRooms();

                    foreach RoomCode in RoomStats.Keys() do
                        ResultTxt += StrSubstNo(StatsLbl, RoomCode, RoomStats.Get(RoomCode)) + '\';

                    Message(ResultTxt);
                end;
            }
            action(ShowAvailableRooms)
            {
                Caption = 'Sale Disponibili';
                ApplicationArea = All;

                trigger OnAction()
                var
                    CustomMgt: Codeunit "MTR Custom Mgt.";
                    AvailableRooms: List of [Code[20]];
                    RoomCode: Code[20];
                    ResultTxt: Text;
                begin
                    AvailableRooms := CustomMgt.GetAvailableRooms();

                    foreach RoomCode in AvailableRooms do
                        ResultTxt += RoomCode + ', ';

                    Message(ResultTxt);
                end;
            }
        }
    }
}