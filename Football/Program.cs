using System;
using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data;

namespace P02_FootballBetting
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var context = new FootballBettingContext())
            {
                context.Database.Migrate();
            }
        }
    }
}