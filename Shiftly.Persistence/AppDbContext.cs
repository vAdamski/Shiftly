using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shiftly.Application.Common.Interfaces.Api;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;
using Shiftly.Application.Common.Interfaces.Persistence;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Enums;

namespace Shiftly.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
	private readonly IDateTime _dateTime;
	private readonly ICurrentUserService _userService;
	
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		
	}
	
	public AppDbContext(DbContextOptions<AppDbContext> options, IDateTime dateTime,
		ICurrentUserService userService) : base(options)
	{
		_dateTime = dateTime;
		_userService = userService;
	}
	
	
	public DatabaseFacade Database => base.Database;
	
	public DbSet<EventEntity> Events { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
	
	public void Dispose()
	{
		base.Dispose();
	}
	
	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		var result = base.SaveChangesAsync(cancellationToken);
		
		return result;
	}
}