namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public decimal HomeTeamBetRate { get; set; }
        public decimal AwayTeamBetRate { get; set; }
        public decimal DrawBetRate { get; set; }
        public DateTime DateTime { get; set; }
        public string Result { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public ICollection<PlayerStatistic> PlayersStatistics { get; set; } = new HashSet<PlayerStatistic>();
        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    }
}