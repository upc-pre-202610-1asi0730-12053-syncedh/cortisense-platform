using BCryptNet = BCrypt.Net.BCrypt;
Console.WriteLine("Admin123! => " + BCryptNet.HashPassword("Admin123!"));
Console.WriteLine("Supervisor123! => " + BCryptNet.HashPassword("Supervisor123!"));
Console.WriteLine("Doctor123! => " + BCryptNet.HashPassword("Doctor123!"));
