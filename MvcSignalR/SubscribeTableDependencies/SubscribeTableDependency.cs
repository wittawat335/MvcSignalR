using MvcSignalR.SubscribeTableDependencies;
using MvcSignalR.Entities;
using MvcSignalR.Hubs;
using TableDependency.SqlClient;

namespace MvcSignalR.SubscribeTableDependencies
{
    public class SubscribeTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Notification> tableDependency;
        NotificationHub _notificationHub;

        public SubscribeTableDependency(NotificationHub notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public void NotiTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Notification>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Notification)} SqlTableDependency error: {e.Error.Message}");
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var notification = e.Entity;
                if (notification.MessageType == "All")
                {
                    await notificationHub.SendNotificationToAll(notification.Message);
                }
                else if (notification.MessageType == "Personal")
                {
                    await notificationHub.SendNotificationToClient(notification.Message, notification.Username);
                }
                else if (notification.MessageType == "Group")
                {
                    await notificationHub.SendNotificationToGroup(notification.Message, notification.Username);
                }
            }
        }
    }
}
