using Flunt.Notifications;
using System;

namespace Domain.Models.Base
{
    public abstract class BaseEntity : Notifiable
    {
        public BaseEntity()
        {

        }

        public long Id { get; private set; }
        public long? CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public long? ModifiedBy { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public abstract void Validate();
        public void SetCreatedBy(long? userId) => CreatedBy = userId;
        public void SetModifiedBy(long? userId) => ModifiedBy = userId;
    }
}
