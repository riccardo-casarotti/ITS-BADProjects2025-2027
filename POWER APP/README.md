# New-Y

ERP application built in Power Apps for client Horsa — integrated with Dynamics 365 Business Central and SharePoint.

## Overview

New-Y is an ERP (Enterprise Resource Planning) application developed in Power Apps for the client Horsa. It digitalizes and simplifies the sales and customer management workflow, providing a single interface that connects customer and inventory data managed in Dynamics 365 Business Central with an application layer built on Power Apps and SharePoint.

## Architecture

| Technology | Role |
|---|---|
| Power Apps (Canvas App) | Application/UI layer: navigation, forms, product gallery, order flow, notifications |
| Dynamics 365 Business Central | Data source for customer/product records and stock levels |
| SharePoint (lists) | Persistence layer for orders, user roles (UserRoles) and approval requests |

## Features

### Authentication and Role-Based Navigation
- Login screen with authentication based on the SharePoint `UserRoles` list
- Loading screen with timer-based routing to the correct interface depending on the user's role
- Role-based visibility and navigation via the `varRuoloUtente` variable — three profiles: **Manager**, **Office**, **Agent**
- Hamburger menu shared across all main screens
- Manager home screen with a 2×2 card grid

### Product Catalog and Stock Management
- Product gallery in grid view
- Stock semaphore — threshold-based color-coding to flag availability levels

### Order Flow
1. Customer selection
2. Product cart (products + quantities)
3. Confirmation, written as a new item to the dedicated SharePoint list

### Notifications and Approvals
- Centralized notification system for pending requests
- Three request types: customer detachment, customer deletion, reorder
- Reorder flow: approval/rejection via LookUp on pending SharePoint requests

## Technical Notes

- Power Fx under the Italian locale: semicolons as parameter separators, `;;` for chaining statements
- SharePoint's `Dynamic` type coercion required wrapping numeric fields with `Value()`
- Delegation limitations required local-collection patterns (`ClearCollect` in `App.OnStart`)
- **Navigate blocked in `Screen.OnVisible`** → solved with a Timer workaround
- **Timer `AutoPause`** silently pauses timers when `Visible = false` → handled explicitly
- Role name mismatch between Italian (app) and English (SharePoint) values → solved by aligning the comparison

## Result

Presented with a grade of 30/30.
