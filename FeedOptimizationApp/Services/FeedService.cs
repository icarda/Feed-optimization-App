using FeedOptimizationApp.Services.Interfaces;
using DataLibrary;
using FeedOptimizationApp.Shared.Wrapper;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedOptimizationApp.Services;

public class FeedService : IFeedService
{
    private readonly ApplicationDbContext _context;

    public FeedService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<FeedEntity>> GetById(string id)
    {
        try
        {
            var feed = await _context.Feeds
            .IgnoreQueryFilters()
            .AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

            if (feed == null)
                throw new Exception($"Unable to return eed with id {id}.");

            return await Result<FeedEntity>.SuccessAsync(feed);
        }
        catch (Exception ex)
        {
            return await Result<FeedEntity>.FailAsync(new List<string> { ex.Message });
        }
    }

    public async Task<Result<FeedEntity>> GetByName(string name)
    {
        try
        {
            var feed = await _context.Feeds
            .IgnoreQueryFilters()
            .AsNoTracking().FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
            if (feed == null)
                throw new Exception($"Unable to return feed with name {name}.");

            return await Result<FeedEntity>.SuccessAsync(feed);
        }
        catch (Exception ex)
        {
            return await Result<FeedEntity>.FailAsync(new List<string> { ex.Message });
        }
    }

    public async Task<Result<int>> SaveAsync(FeedEntity request)
    {
        try
        {
            var existingFeed = await _context.Feeds.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Name.ToLower() == request.Name.ToLower());
            if (existingFeed != null)
                throw new Exception("Feed already exists. Please edit existing entry.");
            var feed = new FeedEntity(
                Guid.NewGuid().ToString(),
                request.Name,
                request.DryMatterPercentage,
                request.MEMcalKg,
                request.MEMJKg,
                request.TDNPercentage,
                request.CPPercentage,
                request.DCPPercentage
            );
            await _context.Feeds.AddAsync(feed);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(feed.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateAsync(FeedEntity request)
    {
        try
        {
            var feed = await _context.Feeds.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == request.Id);
            if (feed == null)
                throw new Exception($"Feed with id {request.Id} not found.");

            feed.Set(
                request.Name,
                request.DryMatterPercentage,
                request.MEMcalKg,
                request.MEMJKg,
                request.TDNPercentage,
                request.CPPercentage,
                request.DCPPercentage
            );

            _context.Feeds.Update(feed);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(feed.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }
}