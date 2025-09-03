using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CPI_Backend.API.Data;
using CPI_Backend.API.DTO.Clients;
using CPI_Backend.API.Models.Clients;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CPI_Backend.API.Endpoints;
public static class ClientsEndpoint
{
    public static void MapClientsEndpoints(this IEndpointRouteBuilder app)
    {
        // GET - Obtener todos los clientes
        app.MapGet("/clients", async (AppDbContext db) =>
        {
            var clients = await db.Clients
                .Include(c => c.Role)
                .Select(c => new ClientDto
                {
                    IdentityDoc = c.IdentityDoc,
                    Username = c.Username,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    RoleCode = c.RoleCode,
                    RoleName = c.Role.RoleName
                })
                .ToListAsync();

            return Results.Ok(clients);
        });

        // GET - Obtener cliente por ID
        app.MapGet("/clients/{identityDoc}", async (string identityDoc, AppDbContext db) =>
        {
            var client = await db.Clients
                .Include(c => c.Role)
                .Where(c => c.IdentityDoc == identityDoc)
                .Select(c => new ClientDto
                {
                    IdentityDoc = c.IdentityDoc,
                    Username = c.Username,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    RoleCode = c.RoleCode,
                    RoleName = c.Role.RoleName
                })
                .FirstOrDefaultAsync();

            return client is not null ? Results.Ok(client) : Results.NotFound();
        });

        // POST - Crear nuevo cliente
        app.MapPost("/clients", async (CreateClientDto input, AppDbContext db) =>
        {
            // Validar que no exista el cliente
            var existingClient = await db.Clients.FirstOrDefaultAsync(c =>
                c.IdentityDoc == input.IdentityDoc || c.Email == input.Email);

            if (existingClient != null)
            {
                return Results.BadRequest("Identity document or email is already registered");
            }

            // Validar que existe el rol
            var role = await db.Roles.FindAsync(input.RoleCode);
            if (role == null)
            {
                return Results.BadRequest("Invalid role code");
            }

            // Validar contraseña
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$");
            if (!regex.IsMatch(input.Password))
            {
                return Results.BadRequest(
                    "Password must include uppercase and lowercase letters, numbers, and special characters");
            }

            // Crear cliente
            var client = new Client
            {
                IdentityDoc = input.IdentityDoc,
                Username = input.Username,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Password = input.Password,
                RoleCode = input.RoleCode
            };

            // Encriptar contraseña
            var hasher = new PasswordHasher<Client>();
            client.Password = hasher.HashPassword(client, input.Password);

            db.Clients.Add(client);
            await db.SaveChangesAsync();

            // Retornar DTO
            var clientDto = new ClientDto
            {
                IdentityDoc = client.IdentityDoc,
                Username = client.Username,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                RoleCode = client.RoleCode,
                RoleName = role.RoleName
            };

            return Results.Created($"/clients/{client.IdentityDoc}", clientDto);
        });

        // PUT - Actualizar cliente
        app.MapPut("/clients/{identityDoc}", async (string identityDoc, UpdateClientDto input, AppDbContext db) =>
        {
            var client = await db.Clients.FindAsync(identityDoc);
            if (client is null)
                return Results.NotFound();

            // Validar email único (excepto el cliente actual)
            var existingClient = await db.Clients.FirstOrDefaultAsync(c =>
                c.Email == input.Email && c.IdentityDoc != identityDoc);

            if (existingClient != null)
            {
                return Results.BadRequest("Email is already registered");
            }

            // Validar rol
            if (!string.IsNullOrEmpty(input.RoleCode))
            {
                var role = await db.Roles.FindAsync(input.RoleCode);
                if (role == null)
                {
                    return Results.BadRequest("Invalid role code");
                }
                client.RoleCode = input.RoleCode;
            }

            // Actualizar datos
            client.Username = input.Username;
            client.Email = input.Email;
            client.PhoneNumber = input.PhoneNumber;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE - Eliminar cliente
        app.MapDelete("/clients/{identityDoc}", async (string identityDoc, AppDbContext db) =>
        {
            var client = await db.Clients.FindAsync(identityDoc);
            if (client is null)
                return Results.NotFound();

            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // POST - Login de cliente
        app.MapPost("/clients/login", async (LoginDto login, AppDbContext db) =>
        {
            var client = await db.Clients
                .Include(c => c.Role)
                .FirstOrDefaultAsync(c => c.IdentityDoc == login.IdentityDoc);

            if (client == null)
            {
                return Results.Unauthorized();
            }

            var hasher = new PasswordHasher<Client>();
            var result = hasher.VerifyHashedPassword(client, client.Password, login.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new
            {
                Message = "Login successful",
                Client = new
                {
                    client.IdentityDoc,
                    client.Username,
                    client.Email,
                    client.PhoneNumber,
                    Role = client.Role.RoleName
                }
            });
        });
    }
}