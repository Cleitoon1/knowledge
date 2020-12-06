namespace Domain.Commands.Stats.GetStats {
    public class GetStatsResponse {
        public GetStatsResponse (int categoriesQuantity, int articlesQuantity, int usersQuantity) {
            this.CategoriesQuantity = categoriesQuantity;
            this.ArticlesQuantity = articlesQuantity;
            this.UsersQuantity = usersQuantity;
        }
        public int CategoriesQuantity { get; set; }
        public int ArticlesQuantity { get; set; }
        public int UsersQuantity { get; set; }
    }
}