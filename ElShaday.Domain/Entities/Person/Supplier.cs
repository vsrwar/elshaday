﻿using ElShaday.Domain.Entities.Person.Abstractions;

namespace ElShaday.Domain.Entities.Person;

public sealed class Supplier : LegalPerson
{
    public Supplier(string document, string corporateName, string fantasyName, Address address)
        : base(document, corporateName, fantasyName, address)
    {
    }
    
    // EF Constructor
    private Supplier()
    { }
}