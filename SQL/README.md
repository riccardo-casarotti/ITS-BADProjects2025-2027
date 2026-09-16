# Database Fundamentals — Final Exam

MySQL exercises on the `corsi` database — DML and DQL.

## Overview

Final exam for the Database Fundamentals course, based on a MySQL database called `corsi` (courses). Split into DML exercises (insert/update/delete) and DQL exercises (joins, filtering, sorting, grouping, formatting).

## DML — Data Manipulation

- **Insert a new course** — added a `MongoDB` course (€300.00) via `INSERT INTO` with default values for auto-generated columns
- **Update a teacher's email** — ran a `SELECT` on `docenti` to find the correct `id` for Marco Bruni, then a targeted `UPDATE` using that id
- **Delete students from a region** — previewed with a `SELECT` first, then `DELETE` from `studenti` for region `'Lombardia'`

## DQL — Queries and Joins

- **Teacher of a specific course** — `INNER JOIN` between `docenti` and `corsi`, filtered by course title
- **Female students, sorted** — filtered `studenti` by `genere = 'F'`, sorted by surname/name
- **Students born in a date range** — filtered by birth date range, sorted by surname, name, birth date (DESC)
- **Enrollment details** — joined `iscrizioni`, `studenti`, `corsi` for title/price/date, sorted by title and date (DESC)
- **Courses with no enrollments** — solved two ways: `LEFT JOIN` + `IS NULL`, and a `NOT IN` subquery
- **Enrollment count per course, including zero** — `LEFT JOIN` with `COUNT(i.id)` instead of `COUNT(*)`, grouped by course
- **Courses taught by a specific teacher** — joined `corsi` and `docenti`, filtered by surname `'Rossi'`
- **Enrollment date formatting** — `DATE_FORMAT()` for Italian date format (`dd-mm-YYYY`) under a backtick-quoted alias
- **Enrollment count for a specific course** — `INNER JOIN` + `COUNT()` filtered to one course title
