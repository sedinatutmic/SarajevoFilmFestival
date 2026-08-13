Sarajevo Film Festival - Booking & Management System

A C# .NET console application built for managing film festival screenings, customer registrations, ticket bookings, and real-time availability tracking with automatic discount calculation.


Key Features
- Screening Management: Add film screenings with capacity limits, dates, and ticket prices.
- Customer Registration: Register customers with contact details.
- Smart Ticket Booking:
  - Automatic price calculation.
  - Applies a 15% discount for bookings of 5 or more tickets 
  - Real-time seat availability checks.
- Seat Availability & Occupancy Status: View capacity status per screening (*Free*, *Nearly Sold Out*, *Sold Out*) with visual percentage breakdown.
- Booking Management:
  - Update Bookings: Change ticket quantities or switch to a different screening with automated seat adjustments and discount recalculation.
  - Cancel Bookings: Cancel existing bookings and automatically restore available seats to the screening capacity.
  - View Cancelled Bookings: Track and review all cancelled reservations.
  - Revenue Reporting: View total confirmed revenue across all active bookings.



- Language: C# (.NET 10.0)
- Data Access: Entity Framework Core
- Pattern: Repository & Service Layer Pattern
- Database: SQLite
- Architecture: Layered architecture (Models, Repositories, Services, Console UI)




   ```bash
   git clone [https://github.com/YOUR_USERNAME/SarajevoFilmFestival.git](https://github.com/YOUR_USERNAME/SarajevoFilmFestival.git)
   cd SarajevoFilmFestival
