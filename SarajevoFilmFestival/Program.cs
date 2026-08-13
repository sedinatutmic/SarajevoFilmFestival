using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SarajevoFilmFestival.Models;
using SarajevoFilmFestival.Repositories;
using SarajevoFilmFestival.Services;
using SarajevoFilmFestival.Data;

namespace SarajevoFilmFestival;

class Program
{
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=sff.db"));
        serviceCollection.AddScoped(typeof(Repository<>));
        serviceCollection.AddScoped<BookingRepository>();
        serviceCollection.AddScoped<ScreeningRepository>();
        serviceCollection.AddScoped<IAppService, FestivalService>();
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate(); 
            var festivalService = scope.ServiceProvider.GetRequiredService<IAppService>();
            PokreniMeni(festivalService);
        }
    }

    private static void PokreniMeni(IAppService festivalService)
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("       SARAJEVO FILM FESTIVAL MENI        ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Dodaj novu projekciju");
            Console.WriteLine("2. Dodaj novog kupca");
            Console.WriteLine("3. Dodijeli rezervaciju kupcu (Popust >= 5)");
            Console.WriteLine("4. Prikaz ukupnih prihoda");
            Console.WriteLine("5. Prikaz svih rezervacija grupisano po projekciji");
            Console.WriteLine("0. Izlaz");
            Console.WriteLine("==========================================");
            Console.Write("Odaberite opciju: ");

            string? unos = Console.ReadLine();

            switch (unos)
            {
                case "1":
                    Console.Write("Naslov projekcije: ");
                    string naslov = Console.ReadLine() ?? "";
                    
                    Console.Write("Cijena karte (decimal): ");
                    decimal.TryParse(Console.ReadLine(), out decimal cijena);
                    
                    Console.Write("Ukupan kapacitet: ");
                    int.TryParse(Console.ReadLine(), out int kapacitet);

                    try
                    {
                        var s = festivalService.AddScreening(naslov, DateTime.Now.AddDays(1), cijena, kapacitet, kapacitet);
                        Console.WriteLine($"\n[+] Projekcija uspješno dodana!");
                    }
                    catch (Exception ex)
                    {
                        string greska = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        Console.WriteLine($"\n[!] Greška: {greska}");
                    }
                    Wait();
                    break;

                case "2":
                    Console.Write("Ime kupca: ");
                    string ime = Console.ReadLine() ?? "";
                    
                    Console.Write("Prezime kupca: ");
                    string prezime = Console.ReadLine() ?? "";
                    
                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";
                    
                    Console.Write("Broj telefona (broj): ");
                    int.TryParse(Console.ReadLine(), out int telefon);

                    try
                    {
                        
                        var c = festivalService.addCustomer(ime, prezime, email, telefon);
                        Console.WriteLine($"\n[+] Kupac uspješno dodan! ID: {c.ID}");
                    }
                    catch (Exception ex)
                    {
                        string greska = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        Console.WriteLine($"\n[!] Greška: {greska}");
                    }
                    Wait();
                    break;

                case "3":
                    Console.Write("Unesite ID kupca: ");
                    int.TryParse(Console.ReadLine(), out int customerId);

                    Console.Write("Unesite ID projekcije: ");
                    int.TryParse(Console.ReadLine(), out int screeningId);

                    Console.Write("Broj karata: ");
                    int.TryParse(Console.ReadLine(), out int ticketCount);

                    try
                    {
                        var b = festivalService.assignBookingToCustomer(customerId, screeningId, ticketCount);
                        Console.WriteLine($"\n[+] Rezervacija uspjela! Ukupna cijena: {b.TotalPrice} KM");
                    }
                    catch (Exception ex)
                    {
                        string greska = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        Console.WriteLine($"\n[!] Greška: {greska}");
                    }
                    Wait();
                    break;

                case "4":
                    decimal prihodi = festivalService.GetTotalRevenue();
                    Console.WriteLine($"\nUkupni prihodi od potvrdjenih rezervacija: {prihodi} KM");
                    Wait();
                    break;

                case "5":
                    var grupisano = festivalService.GroupBookingByScreening();
                    Console.WriteLine("\n--- REZERVACIJE PO PROJEKCIJAMA ---");
                    foreach (var kvp in grupisano)
                    {
                        Console.WriteLine($"Projekcija ID: {kvp.Key} | Broj rezervacija: {kvp.Value.Count}");
                    }
                    Wait();
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("\nNepoznata opcija!");
                    Wait();
                    break;
            }
        }
    }

    private static void Wait()
    {
        Console.WriteLine("\nPritisnite bilo koju tipku...");
        Console.ReadKey();
    }
}