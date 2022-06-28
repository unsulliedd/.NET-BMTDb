using BMTDb.Data.Abstract;
using BMTDb.Data.Concrete.EFCore;
using BMTDb.Service.Abstract;
using BMTDb.Service.Concrete;
using BMTDb.WebUI.Identity;
using BMTDb.WebUI.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BMTDb.WebUI.Filters;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

builder.Services.AddControllersWithViews(                                    //MVC
    options => {options.Filters.Add(typeof(UserActivityFilter));}
    );    

builder.Services.AddDbContext<ApplicationContext>                           //Connection String
    (options => options.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB; 
        AttachDbFilename=C:\Users\berkk\Documents\Visual Studio 2022\Databases\BMTDb.MovieDb.mdf; 
        Database=BMTDb.MovieDb; Integrated Security=TRUE"));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {

    // password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;


    // SignIn
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath =  "/account/signin";
    options.LogoutPath = "/account/signout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(61);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".BMTDb.Account.Cookie"
    };
});

builder.Services.AddScoped<IMovieRepository, EfCoreMovieRepository>();      //Calls concrete version
builder.Services.AddScoped<IMovieService, MovieManager>();

builder.Services.AddScoped<IGenreRepository, EfCoreGenreRepository>();
builder.Services.AddScoped<IGenreService, GenreManager>();

builder.Services.AddScoped<IStudioRepository,EFCoreStudioRepository>();
builder.Services.AddScoped<IStudioService, StudioManager>();

builder.Services.AddScoped<IPersonRepository,EFCorePersonRepository>();
builder.Services.AddScoped<IPersonService, PersonManager>();

//Reads Appsettings.json
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
    new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"],
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:UserName"],
        builder.Configuration["EmailSender:Password"])
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

//localhost/u/profile
app.MapControllerRoute(
    name: "UserProfile",
    pattern: "u/profile/",
    defaults: new { controller = "User", action = "UserProfile" });

//localhost/u/profile/edit
app.MapControllerRoute(
    name: "UserProfileEdit",
    pattern: "u/profile/edit",
    defaults: new { controller = "User", action = "ProfileEdit" });

//localhost/u/movie/new
app.MapControllerRoute(
    name: "UserAddMovie",
    pattern: "u/movie/new",
    defaults: new { controller = "User", action = "UserAddMovie" });

//localhost/admin/dashboard
app.MapControllerRoute(
    name: "AdminDashboard",
    pattern: "admin/dashboard",
    defaults: new { controller = "Admin", action = "AdminDashboard" });

//localhost/admin/user/list
app.MapControllerRoute(
   name: "AdminUsers",
   pattern: "admin/user/list",
   defaults: new { controller = "Admin", action = "UserList" }
);

//localhost/admin/user/id
app.MapControllerRoute(
   name: "AdminUserEdit",
   pattern: "admin/user/{id?}",
   defaults: new { controller = "Admin", action = "UserEdit" }
);

//localhost/admin/roles
app.MapControllerRoute(
    name: "AdminRoles",
    pattern: "admin/role/list",
    defaults: new { controller = "Admin", action = "RoleList" }
);

//localhost/admin/roles/create
app.MapControllerRoute(
    name: "AdminRoleCreate",
    pattern: "admin/role/create",
    defaults: new { controller = "Admin", action = "RoleCreate" }
);

//localhost/admin/role/id
app.MapControllerRoute(
    name: "AdminRoleEdit",
    pattern: "admin/role/{id?}",
    defaults: new { controller = "Admin", action = "RoleEdit" }
);

//localhost/admin/persons
app.MapControllerRoute(
    name: "AdminPersonList",
    pattern: "admin/persons",
    defaults: new { controller = "Admin", action = "PersonList" });

app.MapControllerRoute(
    name: "AdminAddPerson",
    pattern: "admin/add-person",
    defaults: new { controller = "Admin", action = "AddPerson" });

app.MapControllerRoute(
    name: "AdminPersonEdit",
    pattern: "Admin/edit-person/{id?}",
    defaults: new { controller = "Admin", action = "EditPerson" });

//localhost/admin/movies
app.MapControllerRoute(
    name: "AdminMovieList",
    pattern: "admin/movies",
    defaults: new { controller = "Admin", action = "MovieList" });

app.MapControllerRoute(
    name: "AdminAddMovie",
    pattern: "admin/add-movie",
    defaults: new { controller = "Admin", action = "AddMovie" });

app.MapControllerRoute(
    name: "AdminMovieEdit",
    pattern: "Admin/edit-movie/{id?}",
    defaults: new { controller = "Admin", action = "EditMovie" });

//localhost/search
app.MapControllerRoute(
    name: "Search",
    pattern: "search",
    defaults: new { controller = "Movie", action = "Search" });

app.MapControllerRoute(
    name: "MovieStudio",
    pattern: "Movie/{studio?}",
    defaults: new { controller = "Movie", action = "Index" });

app.MapControllerRoute(
    name: "Movies",
    pattern: "Movies/{genre?}",
    defaults: new { controller = "Movie", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
