using System;
using PrintService.Infrastructure.Redis;

namespace PrintService.Domain.Dtos
{
    public class PrintAtTaskDto : IScoredValue
    {
        public Guid Id { get; set; }

        public DateTime PrintAt { get; set; }

        public string Message { get; set; }

        public double GetScore()
        {
            return PrintAt.ToOADate();
        }
    }
}
