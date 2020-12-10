namespace Domain.Commands.Stats.GetStats {
    public class GetStatsResponse {
        public GetStatsResponse (int categories, int articles, int users) {
            this.Categories = categories;
            this.Articles = articles;
            this.Users = users;
        }
        public int Categories { get; set; }
        public int Articles { get; set; }
        public int Users { get; set; }
    }
}