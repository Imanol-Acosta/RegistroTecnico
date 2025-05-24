using RegistroTecnico.Components;
using RegistroTecnico.DAL;
using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container 
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Conexión a base de datos
var Constr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(Constr));

// Inyección de servicios
builder.Services.AddScoped<TecnicoService>();
builder.Services.AddScoped<ClienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();