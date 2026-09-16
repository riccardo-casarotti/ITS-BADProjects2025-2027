# Meeting Room Booking

Business Central AL extension — Final Exam (Verifica Finale BC).

## Overview

An AL extension for Microsoft Dynamics 365 Business Central implementing a **Meeting Room Booking** system: manage a set of meeting rooms and their reservations directly inside Business Central, with automatic conflict checking, computed statistics and room-status tracking.

## Data Model

### Table `MTR Meeting Room` (50100)
Represents a single meeting room. Primary key `RoomCode`, auto-generated on insert (`SALA001`, `SALA002`, …).
- `Description`, `Capacity`, `Location` — descriptive fields
- `IsAvailable` — whether the room can be booked at all (e.g. not under maintenance)
- `Status` (enum) — Free / Occupied / Under Maintenance, derived automatically
- `No. of Bookings` / `Total Hours Booked` — FlowFields, filtered by Date Filter, excluding cancelled bookings

### Table `MTR Meeting Room Booking` (50101)
Represents a single reservation. Primary key: auto-incrementing `EntryNo`.
- `RoomCode` — validated to reject unavailable rooms
- `BookingDate` — validated to reject past dates
- `StartTime` / `EndTime` — end must be later than start; duration computed automatically
- `BookedBy`, `Status` (Confirmed / Cancelled)
- Insert/Modify triggers call a shared availability check, so a booking can never overlap another active booking on the same room

## Business Logic — Codeunit `MTR Custom Mgt.`

- **CheckAvailability / CheckRoomAvailability** — overlap-detection logic, used on insert and modify, excluding cancelled bookings and the record being edited
- **CheckRoomIsAvailable** — blocks bookings on rooms flagged unavailable
- **CreateReservation** — reusable procedure to programmatically create a booking after re-validating availability
- **DetermineRoomStatus / UpdateAllRoomStatuses** — computes and refreshes every room's current status based on active bookings covering the current date/time
- **GetAvailableRooms** — returns rooms currently marked as available
- **GetMostUsedRooms** — returns a dictionary of room codes and booking counts, used for usage statistics

## Pages

| Page | Purpose |
|---|---|
| MTR Meeting Room List | List of all rooms; actions to refresh statuses, show most-used/available rooms |
| MTR Meeting Room Card | Detail card for a room, with statistics and an embedded bookings sub-page |
| MTR Meeting Room Booking Sub | List part embedded in the room card, pre-filtered to the current room |
| MTR Meeting Room Booking List | Standalone list of all bookings across every room |

## Enums

- **MTR Booking Status**: Confirmed / Cancelled
- **MTR Room Status**: Free / Occupied / Under Maintenance (computed automatically)
