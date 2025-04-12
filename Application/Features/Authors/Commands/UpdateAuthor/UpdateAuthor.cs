﻿using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands.UpdateAuthor;



public class UpdateAuthorHandler(IAuthorRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateAuthorCommand, Result>
{

    public async Task<Result> Handle(
        UpdateAuthorCommand request,
        CancellationToken cancellationToken
        )
    {
        var author = await repository.GetByIdAsync(request.Id, cancellationToken);
        author = request.Adapt<Author>();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}

