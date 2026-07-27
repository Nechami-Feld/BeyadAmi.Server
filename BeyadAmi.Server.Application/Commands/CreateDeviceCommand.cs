using BeyadAmi.Server.Application.DTOs.Device;

namespace BeyadAmi.Server.Application.Commands
{
    public class CreateDeviceCommand
    {
        public CreateDeviceDto Device { get; }

        public CreateDeviceCommand(CreateDeviceDto device)
        {
            Device = device;
        }
    }
}