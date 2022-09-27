using GrowthHormone.App.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Singleton
builder.Services.AddSingleton<IRepositorioEspecialista, RepositorioEspecialista>();
builder.Services.AddSingleton<IRepositorioFamiliar, RepositorioFamiliar>();
builder.Services.AddSingleton<IRepositorioPaciente, RepositorioPaciente>();
builder.Services.AddSingleton<IRepositorioHistoria, RepositorioHistoria>();
builder.Services.AddSingleton<IRepositorioPatron, RepositorioPatron>();
builder.Services.AddSingleton<ApplContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
