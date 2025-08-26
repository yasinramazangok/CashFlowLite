
using CashFlowLite.Application.Concretes;
using CashFlowLite.Application.Interfaces;
using CashFlowLite.Application.Repositories;
using CashFlowLite.Application.Services;
using CashFlowLite.Domain.Concretes;
using CashFlowLite.Infrastructure.Concretes;
using CashFlowLite.Infrastructure.Contexts;
using CashFlowLite.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace CashFlowLite.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDbContext<CashFlowLiteDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IOnboardingService, OnboardingService>();

            builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
            builder.Services.AddScoped<IEventHandler<DepositMadeEvent>, DepositTransactionHandler>();
            builder.Services.AddScoped<IEventHandler<WithdrawMadeEvent>, WithdrawTransactionHandler>();



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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
