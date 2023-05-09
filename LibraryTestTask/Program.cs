using LibraryTestTask.Data;
using LibraryTestTask.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IReaderService, ReaderService>();

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
