using System.Text.RegularExpressions;
using CPI_Backend.API.Data;
using CPI_Backend.API.Models.DTO;
using CPI_Backend.API.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace CPI_Backend.API.Endpoints;

public static class UsersEndpoint
{
    public static void MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        //Endpoint de Login & Register

        app.MapGet("/usuarios", async (AppDbContext db) => await db.Users.ToListAsync());

        app.MapGet(
            "/usuarios/{id}",
            async (string id, AppDbContext db) =>
                await db.Users.FindAsync(id) is User u ? Results.Ok(u) : Results.NotFound()
        );

        app.MapPost(
            "/usuarios",
            async (User input, AppDbContext db) =>
            {
                // Validar si ya existe el usuario o el email
                var existingUser = await db.Users.FirstOrDefaultAsync(u =>
                    u.Username == input.Username || u.Email == input.Email
                );

                //Valida que no se repitan datos
                if (existingUser != null)
                {
                    return Results.BadRequest("Username or email is already registered");
                }

                if (input.Password != input.ConfirmPassword)
                {
                    return Results.BadRequest("Password does not match");
                }
                //Valida que la contraseña incluya caracteres especiales
                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$");

                if (!regex.IsMatch(input.Password))
                {
                    return Results.BadRequest(
                        "The password must include uppercase and lowercase letters, numbers, and special characters"
                    );
                }
                //Asignar el rol por defecto
                input.Role = "Default";

                // Encriptar la contraseña
                var hasher = new PasswordHasher<User>();
                input.Password = hasher.HashPassword(input, input.Password);

                //Añade los datos a la base de datos
                db.Users.Add(input);
                await db.SaveChangesAsync();
                return Results.Created($"/usuarios/{input.Id}", input);
            }
        );
        app.MapDelete(
            "/usuarios/{id}",
            async (string id, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(id);
                if (user is null)
                    return Results.NotFound();

                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
        );

        app.MapPost(
            "/login",
            async (LoginDTO login, AppDbContext db) =>
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Username == login.Username);

                if (user == null)
                {
                    return Results.Unauthorized();
                }

                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, login.Password);

                if (result == PasswordVerificationResult.Failed)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(
                    new
                    {
                        Message = "Login exitoso",
                        User = new
                        {
                            user.Id,
                            user.Username,
                            user.Email,
                            user.Role
                        },
                    }
                );
            }
        );

        app.MapPut(
            "/usuarios/{id}",
            async (int id, User input, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(id);
                if (user is null)
                    return Results.NotFound();

                user.Username = input.Username;
                user.Email = input.Email;
                user.Password = input.Password;

                await db.SaveChangesAsync();
                return Results.NoContent();
            }
        );
    }
}
