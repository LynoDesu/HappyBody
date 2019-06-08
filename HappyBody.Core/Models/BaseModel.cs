using System;
namespace HappyBody.Core.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
