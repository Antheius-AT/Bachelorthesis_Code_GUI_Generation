﻿using GeneratorSharedComponents.Abstractions;
using GeneratorSharedComponents.MarkerTypes;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ContainerElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ContainerElementType(IEnumerable<InterfaceElementType<TModelType>> contentElements, string? label) : base("container", typeof(Container), label)
        {
            ContentElements = contentElements;
        }

        public IEnumerable<InterfaceElementType<TModelType>> ContentElements { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
