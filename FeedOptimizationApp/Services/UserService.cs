using DataLibrary;
using DataLibrary.Models;
using FeedOptimizationApp.Services.Interfaces;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace FeedOptimizationApp.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly DeviceService _deviceService;

    public UserService(ApplicationDbContext context, DeviceService deviceService)
    {
        _context = context;
        _deviceService = deviceService;
    }

    public async Task<Result<UserEntity>> GetById(int id)
    {
        try
        {
            var user = await _context.Users
            .IgnoreQueryFilters()
            .AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

            if (user == null)
                throw new Exception($"Unable to return user with id {id}.");

            return await Result<UserEntity>.SuccessAsync(user);
        }
        catch (Exception ex)
        {
            return await Result<UserEntity>.FailAsync(new List<string> { ex.Message });
        }
    }

    public async Task<Result<int>> SaveAsync(UserEntity request)
    {
        try
        {
            var existingUser = await _context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.DeviceIdiom == request.DeviceIdiom);
            if (existingUser != null)
                throw new Exception("User already exists. Please edit existing entry.");

            var user = new UserEntity(
                request.Id,
                request.CountryEntity,
                request.LanguageEntity,
                request.SpeciesEntity,
                request.TermsAndConditions,
                DateTime.UtcNow,
                request.DeviceManufacturer,
                request.DeviceModel,
                request.DeviceName,
                request.DeviceVersionString,
                request.DevicePlatform,
                request.DeviceIdiom,
                request.DeviceType
                );

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(user.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateAsync(UserEntity request)
    {
        try
        {
            var user = await _context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.DeviceIdiom == request.DeviceIdiom);
            if (user == null)
                throw new Exception("User not found.");

            user.Set(
                request.CountryEntity,
                request.LanguageEntity,
                request.SpeciesEntity,
                request.TermsAndConditions,
                request.CreatedAt,
                request.DeviceManufacturer,
                request.DeviceModel,
                request.DeviceName,
                request.DeviceVersionString,
                request.DevicePlatform,
                request.DeviceIdiom,
                request.DeviceType
                );

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(user.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }
}