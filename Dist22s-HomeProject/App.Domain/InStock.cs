﻿using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class InStock : DomainEntityMetaId
{
    [Display(ResourceType = typeof(App.Recources.App.Domain.InStock), Name = nameof(Quantity))]
    public int Quantity { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
}