using BCrypt.Net;
Console.WriteLine("Admin123! => " + BCrypt.Net.BCrypt.HashPassword("Admin123!"));
Console.WriteLine("Supervisor123! => " + BCrypt.Net.BCrypt.HashPassword("Supervisor123!"));
Console.WriteLine("Doctor123! => " + BCrypt.Net.BCrypt.HashPassword("Doctor123!"));
