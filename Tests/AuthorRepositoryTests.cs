
using Application.Interfaces.Repositories;
using Application.QueryParams;


using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Infrastructure.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Xunit.Abstractions;


namespace Tests;

public class AuthorServiceTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _context;
    private readonly IAuthorRepository _repository;

    public AuthorServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Уникальное имя БД на каждый тест
            .Options;

        _context = new ApplicationDbContext(options);


        _repository = new AuthorRepository(_context);
    }

    [Fact]
    public async Task AddAuthor_ShouldReturnAuthor()
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Fyodor",
            Surname = "Dostoevsky",
            Country = "Russia",
            BirthDate = new DateOnly(1821, 11, 11)
        };

        var result = await _repository.AddAsync(author);

        Assert.NotNull(result);
        Assert.Equal("Fyodor", result.Name);
    }

    [Fact]
    public async Task GetAuthorById_ShouldReturnCorrectAuthor()
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Anton",
            Surname = "Chekhov",
            Country = "Russia",
            BirthDate = new DateOnly(1860, 1, 29)
        };

        await _repository.AddAsync(author);
        var fetched = await _repository.GetByIdAsync(author.Id);

        Assert.NotNull(fetched);
        Assert.Equal("Anton", fetched?.Name);
    }

    [Fact]
    public async Task UpdateAuthor_ShouldChangeData()
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = "Leo",
            Surname = "Tolstoy",
            Country = "Russia",
            BirthDate = new DateOnly(1828, 9, 9)
        };

        await _repository.AddAsync(author);
        author.Name = "Lev";
        _repository.Update(author);

        var updated = await _repository.GetByIdAsync(author.Id);

        Assert.NotNull(updated);
        Assert.Equal("Lev", updated?.Name);
    }

   
    [Fact]
    public async Task GetPaginatedAuthors_ShouldReturnCorrectPage()
    {
        for (int i = 1; i <= 10; i++)
        {
            await _repository.AddAsync(new Author
            {
                Id = Guid.NewGuid(),
                Name = $"Author{i}",
                Surname = "Writer",
                Country = "Country",
                BirthDate = new DateOnly(1900 + i, 1, 1)
            });
        }

        await _context.SaveChangesAsync();
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
