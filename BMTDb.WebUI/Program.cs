using BMTDb.Data.Abstract;
using BMTDb.Data.Concrete.EFCore;
using BMTDb.Service.Abstract;
using BMTDb.Service.Concrete;
using BMTDb.WebUI.Identity;
using BMTDb.WebUI.EmailServices;
using BMTDb.WebUI.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews(                                    
    options => {options.Filters.Add<UserActivityFilter>(); }
    );

builder.Services.AddDbContext<BMTDbContext>         //BMTDbContext Connection String
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddDbContext<ApplicationContext>   //ApplicationContext Connection String
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

//IdentityOptions Service
builder.Services.Configure<IdentityOptions>(options => {

    //password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    //SignIn
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
});

//Cookie
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

//HttpClient for TMDB API
builder.Services.AddHttpClient("Tmdb", client =>
{
    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("accept", "application/json");
});

//Inject Services
builder.Services.AddScoped<IUnitofWork, UnitofWork>();

builder.Services.AddScoped<IMovieService, MovieManager>();
builder.Services.AddScoped<IGenreService, GenreManager>();
builder.Services.AddScoped<IProductionCompanyService, ProductionCompanyManager>();
builder.Services.AddScoped<IPersonService, PersonManager>();
builder.Services.AddScoped<IWatchlistService, WatchlistManager>();
builder.Services.AddScoped<IFavouriteService, FavouriteManager>();

builder.Services.AddScoped<UserActivityFilter>();

//Reads Appsettings.json
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
{
    var host = builder.Configuration["EmailSender:Host"] ?? throw new InvalidOperationException("EmailSender:Host is missing.");
    var port = builder.Configuration.GetValue<int>("EmailSender:Port");
    var enableSSL = builder.Configuration.GetValue<bool>("EmailSender:EnableSSL");
    var username = builder.Configuration["EmailSender:UserName"] ?? throw new InvalidOperationException("EmailSender:UserName is missing.");
    var password = builder.Configuration["EmailSender:Password"] ?? throw new InvalidOperationException("EmailSender:Password is missing.");

    return new SmtpEmailSender(host, port, enableSSL, username, password);
});

// API keys
var logger = LoggerFactory.Create(config => config.AddConsole()).CreateLogger("Startup");

string? tmdbApiKey = builder.Configuration["ApiKeys:TmdbApiKey"];
string? omdbApiKey = builder.Configuration["ApiKeys:OmdbApiKey"];

if (string.IsNullOrEmpty(tmdbApiKey))
    logger.LogWarning("TMDB API key not configured. External movie data will be unavailable.");

if (string.IsNullOrEmpty(omdbApiKey))
    logger.LogWarning("OMDB API key not configured. External movie data will be unavailable.");

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

//localhost/u/favourite
app.MapControllerRoute(
    name: "Favourite",
    pattern: "u/favourite/",
    defaults: new { controller = "User", action = "Favourite" });

//localhost/u/watchlist
app.MapControllerRoute(
    name: "Watchlist",
    pattern: "u/watchlist/",
    defaults: new { controller = "User", action = "Watchlist" });

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
    pattern: "movie/{studio?}",
    defaults: new { controller = "Movie", action = "Index" });

app.MapControllerRoute(
    name: "Movies",
    pattern: "movies/{genre?}",
    defaults: new { controller = "Movie", action = "Index" });

app.MapControllerRoute(
    name: "MovieSort",
    pattern: "movies/sort/{sortOrder?}",
    defaults: new { controller = "Movie", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();