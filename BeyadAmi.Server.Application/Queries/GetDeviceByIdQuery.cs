namespace BeyadAmi.Server.Application.Queries
{
    public class GetDeviceByIdQuery
    {
        public int DeviceId { get; }
        public GetDeviceByIdQuery(int deviceId)
        {
            DeviceId = deviceId;
        }
    }
}