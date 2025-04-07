using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Exemplo_Identity_Autorizacao.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do Entity Framework e conex�o com banco de dados
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // N�mero m�ximo de tentativas
            maxRetryDelay: TimeSpan.FromSeconds(10), // Tempo m�ximo de espera entre as tentativas
            errorNumbersToAdd: null // Lista de erros adicionais para tratar como transit�rios
        )));


// Configura��o do Identity para autoriza��o
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    //informa qual contexto representa o banco de dados
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// Servi�os MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware para inicializar banco de dados e Identity
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //define o gerenciador de regras de autoriza��o
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    //define o gerenciador de usu�rios
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Cria��o de roles (Professor e Aluno)
    //se a role n�o existir, o gerenciador de roles a cria
    if (!await roleManager.RoleExistsAsync("Professor"))
    {
        await roleManager.CreateAsync(new IdentityRole("Professor"));
    }
    if (!await roleManager.RoleExistsAsync("Aluno"))
    {
        await roleManager.CreateAsync(new IdentityRole("Aluno"));
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();