﻿namespace Domain.Entities.Abstraction;

public abstract class Entity
{
    public Guid Id { get; protected set; }
}