﻿namespace UsersApp.Infrastructure.DAL;

internal interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}