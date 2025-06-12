using Plugin.LocalNotification;

namespace cursiv.Services
{
    public static class NotificationService
    {
        public static void ScheduleWaterReminder()
        {
            Device.StartTimer(TimeSpan.FromMinutes(30), () =>
            {
                var request = new NotificationRequest
                {
                    NotificationId = 1000,
                    Title = "Нагадування",
                    Description = "Випий 250 мл води 💧",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(1)
                    }
                };

                NotificationCenter.Current.Show(request);
                return true;
            });
        }
    }
}
