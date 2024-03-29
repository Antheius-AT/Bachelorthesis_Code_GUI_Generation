﻿using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public abstract class InterfaceElementType<TModelType> : ISpecificationElementVisitable<TModelType> where TModelType : class
    {
        public InterfaceElementType(string stringRepresentation, Type dotnetTypeRepresentation, string binding, string? label)
        {
            StringRepresentation = stringRepresentation;
            DotnetTypeRepresentation = dotnetTypeRepresentation;
            Label = label;
            Binding = binding;
        }

        public Type DotnetTypeRepresentation { get; }
        public string StringRepresentation { get; }

        public string? Label { get; }

        public string Binding { get; }
        public abstract RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor);
    }
}
