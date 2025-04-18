﻿using Xunit;
using Microsoft.EntityFrameworkCore;

using Infrastructure.Repositories;
using Domain.Entities;
using Domain.Models.QueryParams;
using Infrastructure.Contexts;
using Xunit.Abstractions;


namespace Tests;

public class AuthorServiceTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _context;
    private readonly AuthorRepository _repository;
    private readonly CancellationToken _cancellationToken ;
    public AuthorServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

        _context = new ApplicationDbContext(options);


        _repository = new AuthorRepository(_context);
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public async Task AddAuthor_ShouldReturnAuthor()
    {
        var author = new Author(
            name: "Fyodor",
            surname: "Dostoevsky",
            country : "Russia",
            birthDate : new DateOnly(1821, 11, 11)
        );

        var result = await _repository.AddAsync(author, _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal("Fyodor", result.Name);
    }

    [Fact]
    public async Task GetAuthorById_ShouldReturnCorrectAuthor()
    {
        var author = new Author(
            name: "Anton",
            surname: "Chekhov",
            country: "Russia",
            birthDate: new DateOnly(1860, 1, 29)
        );

        await _repository.AddAsync(author, _cancellationToken);
        await _context.SaveChangesAsync(_cancellationToken);
        var fetched = await _repository.GetByIdAsync(author.Id, _cancellationToken);

        Assert.NotNull(fetched);
        Assert.Equal("Anton", fetched.Name);
    }

    [Fact]
    public async Task UpdateAuthor_ShouldChangeData()
    {
        var author = new Author (
            name : "Leo",
            surname : "Tolstoy",
            country : "Russia",
            birthDate : new DateOnly(1828, 9, 9)
        );

        await _repository.AddAsync(author, _cancellationToken);
        await _context.SaveChangesAsync(_cancellationToken);
        author.Name = "Lev";
        _repository.Update(author);
        await _context.SaveChangesAsync(_cancellationToken);
        var updated = await _repository.GetByIdAsync(author.Id, _cancellationToken);

        Assert.NotNull(updated);
        Assert.Equal("Lev", updated.Name);
    }

   
    [Fact]
    public async Task GetPaginatedAuthors_ShouldReturnCorrectPage()
    {
        for (int i = 1; i <= 10; i++)
        {
            await _repository.AddAsync(new Author(
                $"Author{i}",
                 "Writer",
                 new DateOnly(1900 + i, 1, 1),
                 "Country"
            ), _cancellationToken);
        }

        await _context.SaveChangesAsync(_cancellationToken);
        var filter = new AuthorQueryParams()
        {
            PageNo = 2,
            PageSize = 3
        };

        var page = await _repository.GetPaginatedCollectionAsync(filter);
   
        Assert.Contains(page.ToList(), a => a.Name == "Author4");
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
