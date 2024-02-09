var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

//var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
//var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
//var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
//var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidIssuer = issuer,
//            ValidateAudience = true,
//            ValidAudience = audience,
//            ValidateLifetime = true,
//            IssuerSigningKey = signingKey,
//            ValidateIssuerSigningKey = true

//        };
//    });


//var connectionString = builder.Configuration.GetConnectionString("UserDB");
//builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlite(connectionString));
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
 builder.Services.AddAuthentication().AddCookie("cookie");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
