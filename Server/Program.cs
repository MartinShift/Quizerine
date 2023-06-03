using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.DbModels;
using Server.ServerModels;
using Server.Services;
using DbModels.Repository.Interfaces;
using System;

var services = new ServiceCollection()
    .AddDbContext<QuizerineDbContext>(builder =>
    {
        builder.UseSqlite("Data Source=C:\\Users\\kvvkv\\source\\repos\\Quizerine\\quiz.db");
    })
    .AddScoped<IQuizService, QuizService>()
    .AddScoped<ServerCore, ServerCore>()
    .AddSingleton<IServer, QuizServer>();

services
    .BuildServiceProvider()
    .GetService<IServer>()
    .Run();