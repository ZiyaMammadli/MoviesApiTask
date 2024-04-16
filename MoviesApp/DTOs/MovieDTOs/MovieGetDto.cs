namespace MoviesApp.DTOs.MovieDTOs
{
    public class MovieGetDto
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double SalePrice { get; set; }
    }
}
