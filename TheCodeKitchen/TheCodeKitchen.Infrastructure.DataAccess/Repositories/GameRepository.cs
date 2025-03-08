using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;
using TheCodeKitchen.Core.Domain;
using TheCodeKitchen.Infrastructure.DataAccess.Abstractions;
using TheCodeKitchen.Infrastructure.DataAccess.Entities;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class GameRepository(DbContext context, IMapper mapper) : Repository<Game, GameModel>(context, mapper), IGameRepository
{
    
}