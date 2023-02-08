using Context;
using Repositories.Customers;
using Repositories.Orders;
using Repositories.Products;
using Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var actionExecutingContext =
            actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext;
        if (actionContext.ModelState.ErrorCount > 0
            && actionExecutingContext?.ActionDescriptor.Parameters.Count ==
            actionContext.ActionDescriptor.Parameters.Count)
        {
            return new UnprocessableEntityObjectResult(actionContext.ModelState);
        }

        return new BadRequestObjectResult(actionContext.ModelState);
    };
});

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("ConsumerAPI", new OpenApiInfo()
    {
        Title = "ConsumerAPI",
        Version = "1",
        Description = "Through this API you can access all of our EShop requests",
        Contact = new OpenApiContact()
        {
            Email = "themtsil@gmail.com",
            Name = "Themis Tsiligkoudis",
            Url = new Uri("https://github.com/ttsiligkoudis")
        },
        License = new OpenApiLicense()
        {
            Name = "Common Development and Distribution License",
            Url = new Uri("https://opensource.org/licenses/CDDL-1.0")
        }
    });
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddMvc(setupAction =>
{
    setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
    setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    setupAction.Filters.Add(new ProducesDefaultResponseTypeAttribute());
    setupAction.ReturnHttpNotAcceptable = true;
    setupAction.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
    //setupAction.Filters.Add(new ConsumesAttribute("application/json"));
    setupAction.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    var jsonOutputFormatter = setupAction.OutputFormatters.OfType<SystemTextJsonOutputFormatter>()
        .FirstOrDefault();
    if (jsonOutputFormatter != null)
    {
        if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
        {
            jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
        }
    }
});

var connectionString = builder.Configuration["ConnectionStrings:EShop"];
builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(connectionString, b => b.MigrationsAssembly("EShopAPI")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

//app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(setupAction =>
    {
        setupAction.SwaggerEndpoint("ConsumerAPI/swagger.json", "ConsumerAPI");
        setupAction.EnableDeepLinking();
        setupAction.DisplayOperationId();
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
