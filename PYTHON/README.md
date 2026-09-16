# Stock Portfolio Simulator

Python console application — OOP exercise (classes, static/class methods, exception handling).

## Overview

A console-based stock portfolio simulator split across three modules (`main_portfolio.py`, `portfolio.py`, `stock.py`). Built to satisfy a specific set of OOP constraints: a `main()` entry point, snake_case naming, colored console output via `termcolor`, full `try/except/else/finally` error handling, and the use of both a static method and a class method.

## Modules

### `stock.py` — `Stock` class
Represents a single stock held in a portfolio, identified by its ticker.
- **Class attribute** `total_stocks_created` — shared across all instances, incremented on every new `Stock`
- **Static method** `validate_ticker()` — normalizes the ticker with `strip()`/`upper()`, enforces exactly 4 characters, raises `ValueError` otherwise
- **Class method** `get_stocks_created_count()` — returns the class-level counter
- `add_quantity()` / `remove_quantity()` — adjust held quantity; `remove_quantity()` returns `False` instead of raising when the amount exceeds what's available
- `__str__()` — custom string representation

### `portfolio.py` — `Portfolio` class
Represents a named portfolio belonging to an owner, holding a dict of `Stock` objects plus running totals of money spent/earned.
- `buy_stock()` — wraps ticker validation in `try/except/else/finally`: `except` prints a colored error for an invalid ticker, `else` runs only on success, `finally` always prints a separator
- `sell_stock()` — validates the ticker, checks the stock is held, and only registers the sale if `remove_quantity()` confirms enough shares are available
- `view_single_stock()` — prints a single stock's details
- `view_portfolio()` — prints all holdings, or a message if empty
- `show_summary()` — computes/prints the final balance (3 decimal places), green if positive, red if negative

### `main_portfolio.py` — program flow
Scripted demonstration organized in labeled phases:
1. **Purchase phase** — buys 3 stocks, including one with an intentionally invalid ticker to trigger error handling
2. **Sale phase** — sells part of a held stock, attempts an oversized sale, attempts to sell a stock never purchased
3. **Error tests** — selling/viewing stocks that don't exist
4. **Single-stock view**
5. **Portfolio view and summary**
6. **Class-level demonstration** — calls `Stock.get_stocks_created_count()` directly on the class
