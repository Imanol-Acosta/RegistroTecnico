using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Services;
using RegistroTecnico.Components;
using RegistroTecnico.DAL;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<TecnicoService>();

// Base de datos
var Constr = builder.Configuration.GetConnectionString("PostgreSqlConnection");
builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseNpgsql(Constr));


var supabaseOptions = new SupabaseOptions
{
    AutoConnectRealtime = true
};

var supabase = new Supabase.Client(url, key, supabaseOptions);
await supabase.InitializeAsync();

// Lo registras como singleton para poder inyectarlo donde lo necesites
builder.Services.AddSingleton(supabase);

var app = builder.Build();

// Configure the HTTP request pipeline.
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
