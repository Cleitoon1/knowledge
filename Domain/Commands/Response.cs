using Flunt.Notifications;
using System.Collections.Generic;

namespace Domain.Commands
{
    public class Response
    {
        public Response(Notifiable notifiable)
        {
            this.Success = notifiable.Valid;
            this.Notifications = notifiable.Notifications;
        }

        public Response(Notifiable notifiable, object data)
        {
            this.Success = notifiable.Valid;
            this.Data = data;
            this.Notifications = notifiable.Notifications;
        }

        public IEnumerable<Notification> Notifications { get; }
        public bool Success { get; private set; }
        public object Data { get; private set; }
    }
}
