﻿using Application.DTOs._Author_;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Authors.Queries.GetPaginatedAuthors;

public record GetPaginatedAuthorsCommand : IRequest<Result<PaginationList<AuthorDto>>>
{
    public int PageNo { get; init; }
    public int PageSize { get; init; }
}