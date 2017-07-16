using ModernStore.Shared.Commands;
using System;

namespace ModernStore2.Domain.Commands
{
    public class RegisterOrderItemCommand:ICommand
    {
        public Guid Product { get; set; }
        public int Quantity { get; set; }
    }
}
