﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.DbModels;
using Server.ServerModels;
using Server.Services;
using DbModels.Repository.Interfaces;
using System;
using Server.Services.Interfaces;

var services = new ServiceCollection()
    .AddDbContext<QuizerineDbContext>(builder =>
    {
        builder.UseSqlite("Data Source=D:\\Mein progectos\\Quizerine\\Server\\quiz.db");
    })
    .AddScoped<IQuizService, QuizService>()
    .AddScoped<IQuizResultService, QuizResultService>()
    .AddScoped<ServerCore, ServerCore>()
    .AddSingleton<IServer, QuizServer>();

services
    .BuildServiceProvider()
    .GetService<IServer>()
    .Run();