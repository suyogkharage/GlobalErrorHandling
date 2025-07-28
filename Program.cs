using GlobalErrorHandling.CustomExceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = option =>
    { 
        option.ProblemDetails.Extensions.TryAdd("requestId", option.HttpContext.TraceIdentifier); 
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();    

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
