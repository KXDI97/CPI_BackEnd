// using System.Text.RegularExpressions;
// using CPI_Backend.API.Data;
// using CPI_Backend.API.Models.DTO;
// using CPI_Backend.API.Models.Para_organizar;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Routing;
// using Microsoft.EntityFrameworkCore;

// namespace CPI_Backend.API.Endpoints;

// public static class PurchaseEndpoint
// {
//     public static void MapPurchaseEndpoints(this IEndpointRouteBuilder app)
//     {
//         //  Endpoints para orden de compra
//         app.MapPost(
//             "/compras",
//             async (CompraDTO dto, AppDbContext db) =>
//             {
//                 var product = await db.Products.FindAsync(dto.Cod_Prod);
//                 if (product == null)
//                 {
//                     return Results.BadRequest("Producto no registrado.");
//                 }
//                 var client = await db.Clients.FindAsync(dto.Doc_Identidad); // ✅ Correcto si estás trabajando con Cliente

//                 if (client == null)
//                     return Results.BadRequest("❌ cliente no existe no registrado.");

//                 var compra = new Compra
//                 {
//                     No_Orden = dto.No_Orden,
//                     Doc_Identidad = dto.Doc_Identidad,
//                     Fecha_Compra = dto.Fecha_Compra,
//                     Cod_Prod = dto.Cod_Prod,
//                     Ref_Prod = dto.Ref_Prod,
//                     Cantidad = dto.Cantidad,
//                     Valor_Prod = dto.Valor_Prod,
//                     TRM = dto.TRM,
//                     Cliente = cliente,
//                 };

//                 db.Compras.Add(compra);
//                 producto.Cantidad += dto.Cantidad; // actualizar inventario
//                 await db.SaveChangesAsync();
//                 return Results.Created($"/compras/{dto.No_Orden}", compra);
//             }
//         );

//         // Listar todas las órdenes
//         app.MapGet("/compras", async (AppDbContext db) => await db.Compras.ToListAsync());

//         // Obtener una orden específica
//         app.MapGet(
//             "/compras/{id}",
//             async (string id, AppDbContext db) =>
//             {
//                 var compra = await db
//                     .Compras.Include(c => c.Cliente)
//                     .FirstOrDefaultAsync(c => c.No_Orden == id);

//                 return compra is not null ? Results.Ok(compra) : Results.NotFound();
//             }
//         );

//         // Actualizar una orden (solo si está pendiente - debes tener un campo Estado)
//         app.MapPut(
//             "/compras/{id}",
//             async (string id, CompraDTO dto, AppDbContext db) =>
//             {
//                 var compra = await db.Compras.FindAsync(id);
//                 if (compra == null)
//                     return Results.NotFound();

//                 compra.Fecha_Compra = dto.Fecha_Compra;
//                 compra.Cod_Prod = dto.Cod_Prod;
//                 compra.Ref_Prod = dto.Ref_Prod;
//                 compra.Cantidad = dto.Cantidad;
//                 compra.Valor_Prod = dto.Valor_Prod;
//                 compra.TRM = dto.TRM;

//                 await db.SaveChangesAsync();
//                 return Results.Ok(compra);
//             }
//         );

//         // Eliminar (anular) una orden
//         app.MapDelete(
//             "/compras/{id}",
//             async (string id, AppDbContext db) =>
//             {
//                 var compra = await db.Compras.FindAsync(id);
//                 if (compra == null)
//                     return Results.NotFound();

//                 db.Compras.Remove(compra);
//                 await db.SaveChangesAsync();
//                 return Results.Ok("Orden anulada correctamente.");
//             }
//         );

//         // --- FACTURAS ---
//         app.MapPost(
//             "/facturas",
//             async (FacturaDTO dto, AppDbContext db) =>
//             {
//                 var compra = await db.Compras.FindAsync(dto.No_Orden);
//                 var producto = await db.Productos.FindAsync(dto.Cod_Prod);
//                 if (compra == null || producto == null)
//                 {
//                     return Results.BadRequest("Orden o producto no existe.");
//                 }

//                 var factura = new Factura
//                 {
//                     Num_Factura = dto.Num_Factura,
//                     No_Orden = dto.No_Orden,
//                     Cod_Prod = dto.Cod_Prod,
//                     SubTotal = dto.SubTotal,
//                     IVA = dto.IVA,
//                     Doc_Identidad = dto.Doc_Identidad,
//                     TRM = dto.TRM,
//                     Total = dto.Total,
//                 };

//                 db.Facturas.Add(factura);
//                 await db.SaveChangesAsync();
//                 return Results.Created($"/facturas/{dto.Num_Factura}", factura);
//             }
//         );

//         // --- TRANSACCIONES ---
//         app.MapPost(
//             "/transacciones",
//             async (TransaccionDTO dto, AppDbContext db) =>
//             {
//                 var orden = await db.Compras.FindAsync(dto.No_Orden);
//                 var factura = await db.Facturas.FindAsync(dto.Num_Factura);
//                 if (orden == null || factura == null)
//                 {
//                     return Results.BadRequest("Orden o factura no existe.");
//                 }

//                 var transaccion = new Transaccion
//                 {
//                     Num_Transaccion = dto.Num_Transaccion,
//                     No_Orden = dto.No_Orden,
//                     Num_Factura = dto.Num_Factura,
//                     Recordatorio = dto.Recordatorio,
//                     Estado_Transac = dto.Estado_Transac,
//                     Fechas_Pago = dto.Fechas_Pago,
//                 };

//                 db.Transacciones.Add(transaccion);
//                 await db.SaveChangesAsync();
//                 return Results.Created($"/transacciones/{dto.Num_Transaccion}", transaccion);
//             }
//         );

//         // --- COSTOS LÓGICOS ---
//         app.MapPost(
//             "/costos-logicos",
//             async (CostoLogicoDTO dto, AppDbContext db) =>
//             {
//                 var compra = await db.Compras.FindAsync(dto.No_Orden);
//                 if (compra == null)
//                 {
//                     return Results.BadRequest("Orden no encontrada.");
//                 }

//                 var costo = new CostoLogico
//                 {
//                     No_Orden = dto.No_Orden,
//                     Transporte_Internacional = dto.Transporte_Internacional,
//                     Transporte_Local = dto.Transporte_Local,
//                     Nacionalizacion = dto.Nacionalizacion,
//                     Seguro_Carga = dto.Seguro_Carga,
//                     Almacenamiento = dto.Almacenamiento,
//                     Otros = dto.Otros,
//                 };

//                 db.CostosLogicos.Add(costo);
//                 await db.SaveChangesAsync();
//                 return Results.Created($"/costos-logicos/{dto.No_Orden}", costo);
//             }
//         );
//         app.MapGet(
//             "/productos",
//             async (AppDbContext db) =>
//             {
//                 return await db.Productos.ToListAsync();
//             }
//         );
//         app.MapGet(
//             "/compras/ultimo",
//             async (AppDbContext db) =>
//             {
//                 var ultima = await db
//                     .Compras.OrderByDescending(c => c.No_Orden)
//                     .FirstOrDefaultAsync();

//                 if (ultima == null)
//                     return "ORD-000";

//                 var actual = ultima.No_Orden.Replace("ORD-", "");
//                 if (int.TryParse(actual, out int num))
//                 {
//                     return $"ORD-{(num + 1).ToString("D3")}";
//                 }
//                 return "ORD-001";
//             }
//         );

//         app.MapPost(
//             "/productos",
//             async (Producto prod, AppDbContext db) =>
//             {
//                 // Validar que no exista ya
//                 var existe = await db.Productos.FindAsync(prod.Cod_Prod);
//                 if (existe != null)
//                     return Results.BadRequest("Ya existe un producto con ese código.");

//                 prod.Fecha = prod.Fecha == DateTime.MinValue ? DateTime.Now : prod.Fecha;

//                 db.Productos.Add(prod);
//                 await db.SaveChangesAsync();
//                 return Results.Created($"/productos/{prod.Cod_Prod}", prod);
//             }
//         );

//         // 🔍 Obtener todos los clientes
//         app.MapGet("/clientes", async (AppDbContext db) => await db.Clientes.ToListAsync());

//         // 🔍 Obtener un cliente por su documento
//         app.MapGet(
//             "/clientes/{doc}",
//             async (string doc, AppDbContext db) =>
//             {
//                 var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.DocIdentidad == doc);
//                 return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
//             }
//         );

//         // ➕ Crear un nuevo cliente
//         app.MapPost(
//             "/clientes",
//             async (Cliente cliente, AppDbContext db) =>
//             {
//                 db.Clientes.Add(cliente);
//                 await db.SaveChangesAsync();
//                 return Results.Created($"/clientes/{cliente.DocIdentidad}", cliente);
//             }
//         );

//         // ✏️ Actualizar un cliente existente
//         app.MapPut(
//             "/clientes/{doc}",
//             async (string doc, Cliente inputCliente, AppDbContext db) =>
//             {
//                 var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.DocIdentidad == doc);
//                 if (cliente is null)
//                     return Results.NotFound();

//                 cliente.NomUsuario = inputCliente.NomUsuario;
//                 cliente.Correo = inputCliente.Correo;
//                 cliente.NumTel = inputCliente.NumTel;
//                 cliente.Contrasenia = inputCliente.Contrasenia;
//                 cliente.CodRol = inputCliente.CodRol;

//                 await db.SaveChangesAsync();
//                 return Results.Ok(cliente);
//             }
//         );

//         // ❌ Eliminar un cliente
//         app.MapDelete(
//             "/clientes/{doc}",
//             async (string doc, AppDbContext db) =>
//             {
//                 var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.DocIdentidad == doc);
//                 if (cliente is null)
//                     return Results.NotFound();

//                 db.Clientes.Remove(cliente);
//                 await db.SaveChangesAsync();

//                 return Results.NoContent();
//             }
//         );
//     }
// }
